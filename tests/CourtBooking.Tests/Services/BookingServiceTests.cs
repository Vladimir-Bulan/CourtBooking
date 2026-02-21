using CourtBooking.Application.DTOs.Bookings;
using CourtBooking.Application.Interfaces;
using CourtBooking.Application.Services;
using CourtBooking.Domain.Entities;
using CourtBooking.Domain.Enums;
using CourtBooking.Domain.Interfaces;
using Moq;
using Xunit;

namespace CourtBooking.Tests;

public class BookingServiceTests
{
    // ── Mocks ──────────────────────────────────────────────────────────────
    private readonly Mock<IBookingRepository> _bookingRepo = new();
    private readonly Mock<ICourtRepository> _courtRepo = new();
    private readonly Mock<IUserRepository> _userRepo = new();
    private readonly Mock<IEmailService> _emailSvc = new();
    private readonly BookingService _sut;

    // ── Helpers ────────────────────────────────────────────────────────────
    private static readonly Guid UserId = Guid.NewGuid();
    private static readonly Guid CourtId = Guid.NewGuid();
    private static readonly Guid BookingId = Guid.NewGuid();

    private static Court DefaultCourt() => new()
    {
        Id = CourtId,
        Name = "Cancha 1",
        HourlyRate = 100m,
        IsAvailable = true,
        OpeningTime = new TimeSpan(8, 0, 0),
        ClosingTime = new TimeSpan(22, 0, 0),
        SportType = SportType.Padel,
        Surface = CourtSurface.Synthetic
    };

    private static User DefaultUser() => new()
    {
        Id = UserId,
        FirstName = "Juan",
        LastName = "Perez",
        Email = "juan@test.com"
    };

    private static Booking DefaultBooking(DateTime? start = null, DateTime? end = null) => new()
    {
        Id = BookingId,
        UserId = UserId,
        CourtId = CourtId,
        Court = DefaultCourt(),
        User = DefaultUser(),
        StartTime = start ?? DateTime.UtcNow.AddDays(1).Date.AddHours(10),
        EndTime = end ?? DateTime.UtcNow.AddDays(1).Date.AddHours(12),
        Status = BookingStatus.Confirmed,
        TotalPrice = 200m
    };

    public BookingServiceTests()
    {
        // El email nunca debe romper el flujo
        _emailSvc.Setup(e => e.SendBookingConfirmationAsync(
            It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
            It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<decimal>()))
            .Returns(Task.CompletedTask);

        _emailSvc.Setup(e => e.SendBookingCancellationAsync(
            It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
            It.IsAny<DateTime>(), It.IsAny<string?>()))
            .Returns(Task.CompletedTask);

        _emailSvc.Setup(e => e.SendBookingRescheduleAsync(
            It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
            It.IsAny<DateTime>(), It.IsAny<DateTime>()))
            .Returns(Task.CompletedTask);

        _sut = new BookingService(_bookingRepo.Object, _courtRepo.Object,
                                  _userRepo.Object, _emailSvc.Object);
    }

    // ══════════════════════════════════════════════════════════════════════
    //  CREATE
    // ══════════════════════════════════════════════════════════════════════

    [Fact]
    public async Task CreateAsync_ValidRequest_ReturnsBookingDto()
    {
        // Arrange
        var start = DateTime.UtcNow.AddDays(1).Date.AddHours(10);
        var end = start.AddHours(2);

        var request = new CreateBookingRequest
        {
            CourtId = CourtId,
            StartTime = start,
            EndTime = end
        };

        var court = DefaultCourt();
        var user = DefaultUser();

        _courtRepo.Setup(r => r.GetByIdAsync(CourtId)).ReturnsAsync(court);
        _bookingRepo.Setup(r => r.HasConflictAsync(CourtId, start, end, null)).ReturnsAsync(false);
        _bookingRepo.Setup(r => r.CreateAsync(It.IsAny<Booking>()))
                    .ReturnsAsync((Booking b) => b);
        _userRepo.Setup(r => r.GetByIdAsync(UserId)).ReturnsAsync(user);

        // Act
        var result = await _sut.CreateAsync(UserId, request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Confirmed", result.Status);
        Assert.Equal(200m, result.TotalPrice); // 2 horas × $100
        _bookingRepo.Verify(r => r.CreateAsync(It.IsAny<Booking>()), Times.Once);
    }

    [Fact]
    public async Task CreateAsync_EndTimeBeforeStartTime_ThrowsArgumentException()
    {
        var start = DateTime.UtcNow.AddDays(1).AddHours(12);
        var end = DateTime.UtcNow.AddDays(1).AddHours(10); // end < start

        var request = new CreateBookingRequest
        {
            CourtId = CourtId,
            StartTime = start,
            EndTime = end
        };

        await Assert.ThrowsAsync<ArgumentException>(() =>
            _sut.CreateAsync(UserId, request));
    }

    [Fact]
    public async Task CreateAsync_StartTimeInPast_ThrowsArgumentException()
    {
        var request = new CreateBookingRequest
        {
            CourtId = CourtId,
            StartTime = DateTime.UtcNow.AddHours(-1), // pasado
            EndTime = DateTime.UtcNow.AddHours(1)
        };

        await Assert.ThrowsAsync<ArgumentException>(() =>
            _sut.CreateAsync(UserId, request));
    }

    [Fact]
    public async Task CreateAsync_CourtNotFound_ThrowsKeyNotFoundException()
    {
        var start = DateTime.UtcNow.AddDays(1).AddHours(10);
        var end = start.AddHours(2);

        _courtRepo.Setup(r => r.GetByIdAsync(CourtId)).ReturnsAsync((Court?)null);

        var request = new CreateBookingRequest
        {
            CourtId = CourtId,
            StartTime = start,
            EndTime = end
        };

        await Assert.ThrowsAsync<KeyNotFoundException>(() =>
            _sut.CreateAsync(UserId, request));
    }

    [Fact]
    public async Task CreateAsync_CourtNotAvailable_ThrowsInvalidOperationException()
    {
        var start = DateTime.UtcNow.AddDays(1).AddHours(10);
        var end = start.AddHours(2);

        var court = DefaultCourt();
        court.IsAvailable = false;

        _courtRepo.Setup(r => r.GetByIdAsync(CourtId)).ReturnsAsync(court);

        var request = new CreateBookingRequest
        {
            CourtId = CourtId,
            StartTime = start,
            EndTime = end
        };

        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            _sut.CreateAsync(UserId, request));
    }

    [Fact]
    public async Task CreateAsync_OutsideOpeningHours_ThrowsInvalidOperationException()
    {
        // Cancha cierra a las 22:00, pedimos 23:00 - 23:30 (mismo día)
        var start = DateTime.UtcNow.AddDays(1).Date.AddHours(23);
        var end = DateTime.UtcNow.AddDays(1).Date.AddHours(23).AddMinutes(30);

        _courtRepo.Setup(r => r.GetByIdAsync(CourtId)).ReturnsAsync(DefaultCourt());

        var request = new CreateBookingRequest
        {
            CourtId = CourtId,
            StartTime = start,
            EndTime = end
        };

        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            _sut.CreateAsync(UserId, request));
    }

    [Fact]
    public async Task CreateAsync_ConflictExists_ThrowsInvalidOperationException()
    {
        var start = DateTime.UtcNow.AddDays(1).Date.AddHours(10);
        var end = start.AddHours(2);

        _courtRepo.Setup(r => r.GetByIdAsync(CourtId)).ReturnsAsync(DefaultCourt());
        _bookingRepo.Setup(r => r.HasConflictAsync(CourtId, start, end, null)).ReturnsAsync(true);

        var request = new CreateBookingRequest
        {
            CourtId = CourtId,
            StartTime = start,
            EndTime = end
        };

        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            _sut.CreateAsync(UserId, request));
    }

    [Fact]
    public async Task CreateAsync_EmailFails_StillReturnsBooking()
    {
        var start = DateTime.UtcNow.AddDays(1).Date.AddHours(10);
        var end = start.AddHours(2);

        _courtRepo.Setup(r => r.GetByIdAsync(CourtId)).ReturnsAsync(DefaultCourt());
        _bookingRepo.Setup(r => r.HasConflictAsync(CourtId, start, end, null)).ReturnsAsync(false);
        _bookingRepo.Setup(r => r.CreateAsync(It.IsAny<Booking>()))
                    .ReturnsAsync((Booking b) => b);
        _userRepo.Setup(r => r.GetByIdAsync(UserId)).ReturnsAsync(DefaultUser());

        // El servicio de email lanza excepción
        _emailSvc.Setup(e => e.SendBookingConfirmationAsync(
            It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
            It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<decimal>()))
            .ThrowsAsync(new Exception("SMTP error"));

        var request = new CreateBookingRequest
        {
            CourtId = CourtId,
            StartTime = start,
            EndTime = end
        };

        // No debe lanzar excepción aunque el email falle
        var result = await _sut.CreateAsync(UserId, request);
        Assert.NotNull(result);
    }

    // ══════════════════════════════════════════════════════════════════════
    //  CANCEL
    // ══════════════════════════════════════════════════════════════════════

    [Fact]
    public async Task CancelAsync_ValidRequest_ReturnsCancelledBooking()
    {
        var booking = DefaultBooking();
        var cancelRequest = new CancelBookingRequest { Reason = "Lluvia" };

        _bookingRepo.Setup(r => r.GetByIdWithDetailsAsync(BookingId)).ReturnsAsync(booking);
        _bookingRepo.Setup(r => r.UpdateAsync(It.IsAny<Booking>()))
                    .ReturnsAsync((Booking b) => b);

        var result = await _sut.CancelAsync(BookingId, UserId, cancelRequest);

        Assert.Equal("Cancelled", result.Status);
        Assert.Equal("Lluvia", result.CancellationReason);
    }

    [Fact]
    public async Task CancelAsync_BookingNotFound_ThrowsKeyNotFoundException()
    {
        _bookingRepo.Setup(r => r.GetByIdWithDetailsAsync(BookingId)).ReturnsAsync((Booking?)null);

        await Assert.ThrowsAsync<KeyNotFoundException>(() =>
            _sut.CancelAsync(BookingId, UserId, new CancelBookingRequest()));
    }

    [Fact]
    public async Task CancelAsync_DifferentUser_ThrowsUnauthorizedAccessException()
    {
        var booking = DefaultBooking();
        var otherUserId = Guid.NewGuid();

        _bookingRepo.Setup(r => r.GetByIdWithDetailsAsync(BookingId)).ReturnsAsync(booking);

        await Assert.ThrowsAsync<UnauthorizedAccessException>(() =>
            _sut.CancelAsync(BookingId, otherUserId, new CancelBookingRequest()));
    }

    [Fact]
    public async Task CancelAsync_AlreadyCancelled_ThrowsInvalidOperationException()
    {
        var booking = DefaultBooking();
        booking.Status = BookingStatus.Cancelled;

        _bookingRepo.Setup(r => r.GetByIdWithDetailsAsync(BookingId)).ReturnsAsync(booking);

        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            _sut.CancelAsync(BookingId, UserId, new CancelBookingRequest()));
    }

    [Fact]
    public async Task CancelAsync_BookingAlreadyStarted_ThrowsInvalidOperationException()
    {
        // StartTime en el pasado
        var booking = DefaultBooking(
            start: DateTime.UtcNow.AddHours(-1),
            end: DateTime.UtcNow.AddHours(1));

        _bookingRepo.Setup(r => r.GetByIdWithDetailsAsync(BookingId)).ReturnsAsync(booking);

        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            _sut.CancelAsync(BookingId, UserId, new CancelBookingRequest()));
    }

    // ══════════════════════════════════════════════════════════════════════
    //  RESCHEDULE
    // ══════════════════════════════════════════════════════════════════════

    [Fact]
    public async Task RescheduleAsync_ValidRequest_ReturnsUpdatedBooking()
    {
        var booking = DefaultBooking();
        var newStart = DateTime.UtcNow.AddDays(2).Date.AddHours(14);
        var newEnd = newStart.AddHours(2);

        var request = new RescheduleBookingRequest
        {
            NewStartTime = newStart,
            NewEndTime = newEnd
        };

        _bookingRepo.Setup(r => r.GetByIdWithDetailsAsync(BookingId)).ReturnsAsync(booking);
        _bookingRepo.Setup(r => r.HasConflictAsync(CourtId, newStart, newEnd, BookingId)).ReturnsAsync(false);
        _bookingRepo.Setup(r => r.UpdateAsync(It.IsAny<Booking>()))
                    .ReturnsAsync((Booking b) => b);

        var result = await _sut.RescheduleAsync(BookingId, UserId, request);

        Assert.Equal(newStart, result.StartTime);
        Assert.Equal(newEnd, result.EndTime);
    }

    [Fact]
    public async Task RescheduleAsync_CancelledBooking_ThrowsInvalidOperationException()
    {
        var booking = DefaultBooking();
        booking.Status = BookingStatus.Cancelled;

        _bookingRepo.Setup(r => r.GetByIdWithDetailsAsync(BookingId)).ReturnsAsync(booking);

        var request = new RescheduleBookingRequest
        {
            NewStartTime = DateTime.UtcNow.AddDays(2),
            NewEndTime = DateTime.UtcNow.AddDays(2).AddHours(2)
        };

        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            _sut.RescheduleAsync(BookingId, UserId, request));
    }

    [Fact]
    public async Task RescheduleAsync_DifferentUser_ThrowsUnauthorizedAccessException()
    {
        var booking = DefaultBooking();
        var otherUserId = Guid.NewGuid();

        _bookingRepo.Setup(r => r.GetByIdWithDetailsAsync(BookingId)).ReturnsAsync(booking);

        var request = new RescheduleBookingRequest
        {
            NewStartTime = DateTime.UtcNow.AddDays(2),
            NewEndTime = DateTime.UtcNow.AddDays(2).AddHours(2)
        };

        await Assert.ThrowsAsync<UnauthorizedAccessException>(() =>
            _sut.RescheduleAsync(BookingId, otherUserId, request));
    }

    [Fact]
    public async Task RescheduleAsync_NewEndTimeBeforeStartTime_ThrowsArgumentException()
    {
        var booking = DefaultBooking();
        _bookingRepo.Setup(r => r.GetByIdWithDetailsAsync(BookingId)).ReturnsAsync(booking);

        var request = new RescheduleBookingRequest
        {
            NewStartTime = DateTime.UtcNow.AddDays(2).AddHours(12),
            NewEndTime = DateTime.UtcNow.AddDays(2).AddHours(10) // end < start
        };

        await Assert.ThrowsAsync<ArgumentException>(() =>
            _sut.RescheduleAsync(BookingId, UserId, request));
    }

    [Fact]
    public async Task RescheduleAsync_NewSlotHasConflict_ThrowsInvalidOperationException()
    {
        var booking = DefaultBooking();
        var newStart = DateTime.UtcNow.AddDays(2).Date.AddHours(14);
        var newEnd = newStart.AddHours(2);

        _bookingRepo.Setup(r => r.GetByIdWithDetailsAsync(BookingId)).ReturnsAsync(booking);
        _bookingRepo.Setup(r => r.HasConflictAsync(CourtId, newStart, newEnd, BookingId)).ReturnsAsync(true);

        var request = new RescheduleBookingRequest
        {
            NewStartTime = newStart,
            NewEndTime = newEnd
        };

        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            _sut.RescheduleAsync(BookingId, UserId, request));
    }

    // ══════════════════════════════════════════════════════════════════════
    //  GET
    // ══════════════════════════════════════════════════════════════════════

    [Fact]
    public async Task GetByIdAsync_ExistingId_ReturnsDto()
    {
        var booking = DefaultBooking();
        _bookingRepo.Setup(r => r.GetByIdWithDetailsAsync(BookingId)).ReturnsAsync(booking);

        var result = await _sut.GetByIdAsync(BookingId);

        Assert.NotNull(result);
        Assert.Equal(BookingId, result.Id);
    }

    [Fact]
    public async Task GetByIdAsync_NonExistingId_ReturnsNull()
    {
        _bookingRepo.Setup(r => r.GetByIdWithDetailsAsync(It.IsAny<Guid>())).ReturnsAsync((Booking?)null);

        var result = await _sut.GetByIdAsync(Guid.NewGuid());

        Assert.Null(result);
    }

    [Fact]
    public async Task GetMyBookingsAsync_ReturnsOnlyUserBookings()
    {
        var bookings = new List<Booking> { DefaultBooking(), DefaultBooking() };
        _bookingRepo.Setup(r => r.GetByUserIdAsync(UserId)).ReturnsAsync(bookings);

        var result = await _sut.GetMyBookingsAsync(UserId);

        Assert.Equal(2, result.Count());
        Assert.All(result, b => Assert.Equal(UserId, b.UserId));
    }
}