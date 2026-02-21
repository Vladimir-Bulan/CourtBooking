using Xunit;
using CourtBooking.Application.DTOs.Bookings;
using CourtBooking.Application.DTOs.Common;
using CourtBooking.Application.Interfaces;
using CourtBooking.Application.Services;
using CourtBooking.Domain.Enums;
using CourtBooking.Domain.Interfaces;
using CourtBooking.Tests.Helpers;
using FluentAssertions;
using Moq;

namespace CourtBooking.Tests.Services;

public class BookingServiceTests
{
    private readonly Mock<IBookingRepository> _bookingRepoMock;
    private readonly Mock<ICourtRepository> _courtRepoMock;
    private readonly Mock<IUserRepository> _userRepoMock;
    private readonly Mock<IEmailService> _emailServiceMock;
    private readonly BookingService _bookingService;

    public BookingServiceTests()
    {
        _bookingRepoMock = new Mock<IBookingRepository>();
        _courtRepoMock = new Mock<ICourtRepository>();
        _userRepoMock = new Mock<IUserRepository>();
        _emailServiceMock = new Mock<IEmailService>();

        _bookingService = new BookingService(
            _bookingRepoMock.Object,
            _courtRepoMock.Object,
            _userRepoMock.Object,
            _emailServiceMock.Object);
    }

    // ===================== CREATE BOOKING =====================

    [Fact]
    public async Task CreateBooking_WithValidData_ReturnsBookingDto()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var court = TestBuilders.BuildCourt(hourlyRate: 1000);
        var user = TestBuilders.BuildUser(id: userId);

        var startTime = DateTime.UtcNow.AddDays(1).Date.AddHours(10);
        var endTime = startTime.AddHours(2);

        var request = new CreateBookingRequest
        {
            CourtId = court.Id,
            StartTime = startTime,
            EndTime = endTime
        };

        _courtRepoMock.Setup(r => r.GetByIdAsync(court.Id)).ReturnsAsync(court);
        _bookingRepoMock.Setup(r => r.HasConflictAsync(court.Id, startTime, endTime, null)).ReturnsAsync(false);
        _bookingRepoMock.Setup(r => r.CreateAsync(It.IsAny<Domain.Entities.Booking>()))
            .ReturnsAsync((Domain.Entities.Booking b) => b);
        _userRepoMock.Setup(r => r.GetByIdAsync(userId)).ReturnsAsync(user);
        _emailServiceMock.Setup(e => e.SendBookingConfirmationAsync(
            It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
            It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<decimal>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _bookingService.CreateAsync(userId, request);

        // Assert
        result.Should().NotBeNull();
        result.CourtId.Should().Be(court.Id);
        result.UserId.Should().Be(userId);
        result.TotalPrice.Should().Be(2000); // 2 horas * $1000
        result.Status.Should().Be("Confirmed");
    }

    [Fact]
    public async Task CreateBooking_WithPastDate_ThrowsArgumentException()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var request = new CreateBookingRequest
        {
            CourtId = Guid.NewGuid(),
            StartTime = DateTime.UtcNow.AddHours(-2),
            EndTime = DateTime.UtcNow.AddHours(-1)
        };

        // Act
        var act = async () => await _bookingService.CreateAsync(userId, request);

        // Assert
        await act.Should().ThrowAsync<ArgumentException>()
            .WithMessage("Cannot book in the past.");
    }

    [Fact]
    public async Task CreateBooking_WithEndBeforeStart_ThrowsArgumentException()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var request = new CreateBookingRequest
        {
            CourtId = Guid.NewGuid(),
            StartTime = DateTime.UtcNow.AddDays(1).AddHours(2),
            EndTime = DateTime.UtcNow.AddDays(1).AddHours(1)
        };

        // Act
        var act = async () => await _bookingService.CreateAsync(userId, request);

        // Assert
        await act.Should().ThrowAsync<ArgumentException>()
            .WithMessage("End time must be after start time.");
    }

    [Fact]
    public async Task CreateBooking_WithUnavailableCourt_ThrowsInvalidOperationException()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var court = TestBuilders.BuildCourt(isAvailable: false);

        var startTime = DateTime.UtcNow.AddDays(1).Date.AddHours(10);
        var request = new CreateBookingRequest
        {
            CourtId = court.Id,
            StartTime = startTime,
            EndTime = startTime.AddHours(1)
        };

        _courtRepoMock.Setup(r => r.GetByIdAsync(court.Id)).ReturnsAsync(court);

        // Act
        var act = async () => await _bookingService.CreateAsync(userId, request);

        // Assert
        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("Court is not available.");
    }

    [Fact]
    public async Task CreateBooking_WithConflict_ThrowsInvalidOperationException()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var court = TestBuilders.BuildCourt();

        var startTime = DateTime.UtcNow.AddDays(1).Date.AddHours(10);
        var endTime = startTime.AddHours(1);

        var request = new CreateBookingRequest
        {
            CourtId = court.Id,
            StartTime = startTime,
            EndTime = endTime
        };

        _courtRepoMock.Setup(r => r.GetByIdAsync(court.Id)).ReturnsAsync(court);
        _bookingRepoMock.Setup(r => r.HasConflictAsync(court.Id, startTime, endTime, null)).ReturnsAsync(true);

        // Act
        var act = async () => await _bookingService.CreateAsync(userId, request);

        // Assert
        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("The court is already booked for this time slot.");
    }

    [Fact]
    public async Task CreateBooking_WithNonExistentCourt_ThrowsKeyNotFoundException()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var request = new CreateBookingRequest
        {
            CourtId = Guid.NewGuid(),
            StartTime = DateTime.UtcNow.AddDays(1).AddHours(10),
            EndTime = DateTime.UtcNow.AddDays(1).AddHours(11)
        };

        _courtRepoMock.Setup(r => r.GetByIdAsync(request.CourtId)).ReturnsAsync((Domain.Entities.Court?)null);

        // Act
        var act = async () => await _bookingService.CreateAsync(userId, request);

        // Assert
        await act.Should().ThrowAsync<KeyNotFoundException>();
    }

    // ===================== CANCEL BOOKING =====================

    [Fact]
    public async Task CancelBooking_WithValidData_ReturnsUpdatedBooking()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var user = TestBuilders.BuildUser(id: userId);
        var court = TestBuilders.BuildCourt();
        var booking = TestBuilders.BuildBooking(
            userId: userId,
            courtId: court.Id,
            startTime: DateTime.UtcNow.AddDays(1).AddHours(10),
            status: BookingStatus.Confirmed);

        booking.User = user;
        booking.Court = court;

        var request = new CancelBookingRequest { Reason = "No puedo ir" };

        _bookingRepoMock.Setup(r => r.GetByIdWithDetailsAsync(booking.Id)).ReturnsAsync(booking);
        _bookingRepoMock.Setup(r => r.UpdateAsync(It.IsAny<Domain.Entities.Booking>()))
            .ReturnsAsync((Domain.Entities.Booking b) => b);
        _emailServiceMock.Setup(e => e.SendBookingCancellationAsync(
            It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
            It.IsAny<DateTime>(), It.IsAny<string?>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _bookingService.CancelAsync(booking.Id, userId, request);

        // Assert
        result.Status.Should().Be("Cancelled");
        result.CancellationReason.Should().Be("No puedo ir");
    }

    [Fact]
    public async Task CancelBooking_ByDifferentUser_ThrowsUnauthorizedException()
    {
        // Arrange
        var ownerUserId = Guid.NewGuid();
        var otherUserId = Guid.NewGuid();
        var booking = TestBuilders.BuildBooking(userId: ownerUserId);

        _bookingRepoMock.Setup(r => r.GetByIdWithDetailsAsync(booking.Id)).ReturnsAsync(booking);

        // Act
        var act = async () => await _bookingService.CancelAsync(booking.Id, otherUserId, new CancelBookingRequest());

        // Assert
        await act.Should().ThrowAsync<UnauthorizedAccessException>();
    }

    [Fact]
    public async Task CancelBooking_AlreadyCancelled_ThrowsInvalidOperationException()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var booking = TestBuilders.BuildBooking(userId: userId, status: BookingStatus.Cancelled);

        _bookingRepoMock.Setup(r => r.GetByIdWithDetailsAsync(booking.Id)).ReturnsAsync(booking);

        // Act
        var act = async () => await _bookingService.CancelAsync(booking.Id, userId, new CancelBookingRequest());

        // Assert
        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("Booking is already cancelled.");
    }

    // ===================== RESCHEDULE =====================

    [Fact]
    public async Task RescheduleBooking_WithValidData_UpdatesTimesAndPrice()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var user = TestBuilders.BuildUser(id: userId);
        var court = TestBuilders.BuildCourt(hourlyRate: 1500);
        var booking = TestBuilders.BuildBooking(userId: userId, courtId: court.Id);
        booking.User = user;
        booking.Court = court;

        var newStart = DateTime.UtcNow.AddDays(2).Date.AddHours(14);
        var newEnd = newStart.AddHours(2);

        var request = new RescheduleBookingRequest
        {
            NewStartTime = newStart,
            NewEndTime = newEnd
        };

        _bookingRepoMock.Setup(r => r.GetByIdWithDetailsAsync(booking.Id)).ReturnsAsync(booking);
        _bookingRepoMock.Setup(r => r.HasConflictAsync(court.Id, newStart, newEnd, booking.Id)).ReturnsAsync(false);
        _bookingRepoMock.Setup(r => r.UpdateAsync(It.IsAny<Domain.Entities.Booking>()))
            .ReturnsAsync((Domain.Entities.Booking b) => b);
        _emailServiceMock.Setup(e => e.SendBookingRescheduleAsync(
            It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
            It.IsAny<DateTime>(), It.IsAny<DateTime>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _bookingService.RescheduleAsync(booking.Id, userId, request);

        // Assert
        result.StartTime.Should().Be(newStart);
        result.EndTime.Should().Be(newEnd);
        result.TotalPrice.Should().Be(3000); // 2 horas * $1500
    }
}
