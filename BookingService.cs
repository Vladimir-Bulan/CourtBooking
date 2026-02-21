using CourtBooking.Application.DTOs.Bookings;
using CourtBooking.Application.Interfaces;
using CourtBooking.Domain.Entities;
using CourtBooking.Domain.Enums;
using CourtBooking.Domain.Interfaces;

namespace CourtBooking.Application.Services;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _bookingRepository;
    private readonly ICourtRepository _courtRepository;
    private readonly IUserRepository _userRepository;
    private readonly IEmailService _emailService;

    public BookingService(
        IBookingRepository bookingRepository,
        ICourtRepository courtRepository,
        IUserRepository userRepository,
        IEmailService emailService)
    {
        _bookingRepository = bookingRepository;
        _courtRepository = courtRepository;
        _userRepository = userRepository;
        _emailService = emailService;
    }

    public async Task<IEnumerable<BookingDto>> GetAllAsync(BookingFilterRequest? filter = null)
    {
        IEnumerable<Booking> bookings;

        if (filter?.From.HasValue == true && filter?.To.HasValue == true)
            bookings = await _bookingRepository.GetByDateRangeAsync(filter.From.Value, filter.To.Value);
        else
            bookings = await _bookingRepository.GetAllAsync();

        if (filter?.Status is not null && Enum.TryParse<BookingStatus>(filter.Status, out var status))
            bookings = bookings.Where(b => b.Status == status);

        if (filter?.CourtId.HasValue == true)
            bookings = bookings.Where(b => b.CourtId == filter.CourtId.Value);

        return bookings.Select(MapToDto);
    }

    public async Task<BookingDto?> GetByIdAsync(Guid id)
    {
        var booking = await _bookingRepository.GetByIdWithDetailsAsync(id);
        return booking is null ? null : MapToDto(booking);
    }

    public async Task<IEnumerable<BookingDto>> GetMyBookingsAsync(Guid userId)
    {
        var bookings = await _bookingRepository.GetByUserIdAsync(userId);
        return bookings.Select(MapToDto);
    }

    public async Task<BookingDto> CreateAsync(Guid userId, CreateBookingRequest request)
    {
        // Validations
        if (request.EndTime <= request.StartTime)
            throw new ArgumentException("End time must be after start time.");

        if (request.StartTime < DateTime.UtcNow)
            throw new ArgumentException("Cannot book in the past.");

        var court = await _courtRepository.GetByIdAsync(request.CourtId)
            ?? throw new KeyNotFoundException("Court not found.");

        if (!court.IsAvailable)
            throw new InvalidOperationException("Court is not available.");

        // Validate opening hours
        var startHour = request.StartTime.TimeOfDay;
        var endHour = request.EndTime.TimeOfDay;
        if (startHour < court.OpeningTime || endHour > court.ClosingTime)
            throw new InvalidOperationException($"Court is only open from {court.OpeningTime:hh\\:mm} to {court.ClosingTime:hh\\:mm}.");

        // Check conflicts
        var hasConflict = await _bookingRepository.HasConflictAsync(request.CourtId, request.StartTime, request.EndTime);
        if (hasConflict)
            throw new InvalidOperationException("The court is already booked for this time slot.");

        var hours = (decimal)(request.EndTime - request.StartTime).TotalHours;
        var totalPrice = court.HourlyRate * hours;

        var booking = new Booking
        {
            UserId = userId,
            CourtId = request.CourtId,
            StartTime = request.StartTime,
            EndTime = request.EndTime,
            TotalPrice = totalPrice,
            Notes = request.Notes,
            Status = BookingStatus.Confirmed
        };

        await _bookingRepository.CreateAsync(booking);

        // Send confirmation email
        var user = await _userRepository.GetByIdAsync(userId);
        if (user is not null)
        {
            try
            {
                await _emailService.SendBookingConfirmationAsync(
                    user.Email, user.FullName, court.Name,
                    request.StartTime, request.EndTime, totalPrice);
            }
            catch { /* Don't fail the booking if email fails */ }
        }

        return MapToDto(booking, user, court);
    }

    public async Task<BookingDto> RescheduleAsync(Guid bookingId, Guid userId, RescheduleBookingRequest request)
    {
        var booking = await _bookingRepository.GetByIdWithDetailsAsync(bookingId)
            ?? throw new KeyNotFoundException("Booking not found.");

        if (booking.UserId != userId)
            throw new UnauthorizedAccessException("You can only reschedule your own bookings.");

        if (booking.Status == BookingStatus.Cancelled)
            throw new InvalidOperationException("Cannot reschedule a cancelled booking.");

        if (request.NewEndTime <= request.NewStartTime)
            throw new ArgumentException("End time must be after start time.");

        if (request.NewStartTime < DateTime.UtcNow)
            throw new ArgumentException("Cannot reschedule to the past.");

        var hasConflict = await _bookingRepository.HasConflictAsync(
            booking.CourtId, request.NewStartTime, request.NewEndTime, bookingId);

        if (hasConflict)
            throw new InvalidOperationException("The court is already booked for the new time slot.");

        var hours = (decimal)(request.NewEndTime - request.NewStartTime).TotalHours;
        booking.StartTime = request.NewStartTime;
        booking.EndTime = request.NewEndTime;
        booking.TotalPrice = booking.Court.HourlyRate * hours;
        booking.UpdatedAt = DateTime.UtcNow;

        await _bookingRepository.UpdateAsync(booking);

        // Send reschedule email
        try
        {
            await _emailService.SendBookingRescheduleAsync(
                booking.User.Email, booking.User.FullName, booking.Court.Name,
                request.NewStartTime, request.NewEndTime);
        }
        catch { }

        return MapToDto(booking);
    }

    public async Task<BookingDto> CancelAsync(Guid bookingId, Guid userId, CancelBookingRequest request)
    {
        var booking = await _bookingRepository.GetByIdWithDetailsAsync(bookingId)
            ?? throw new KeyNotFoundException("Booking not found.");

        if (booking.UserId != userId)
            throw new UnauthorizedAccessException("You can only cancel your own bookings.");

        if (booking.Status == BookingStatus.Cancelled)
            throw new InvalidOperationException("Booking is already cancelled.");

        if (booking.StartTime < DateTime.UtcNow)
            throw new InvalidOperationException("Cannot cancel a booking that has already started.");

        booking.Status = BookingStatus.Cancelled;
        booking.CancellationReason = request.Reason;
        booking.CancelledAt = DateTime.UtcNow;
        booking.UpdatedAt = DateTime.UtcNow;

        await _bookingRepository.UpdateAsync(booking);

        // Send cancellation email
        try
        {
            await _emailService.SendBookingCancellationAsync(
                booking.User.Email, booking.User.FullName, booking.Court.Name,
                booking.StartTime, request.Reason);
        }
        catch { }

        return MapToDto(booking);
    }

    public async Task<BookingDto> ConfirmAsync(Guid bookingId)
    {
        var booking = await _bookingRepository.GetByIdWithDetailsAsync(bookingId)
            ?? throw new KeyNotFoundException("Booking not found.");

        booking.Status = BookingStatus.Confirmed;
        booking.UpdatedAt = DateTime.UtcNow;

        await _bookingRepository.UpdateAsync(booking);
        return MapToDto(booking);
    }

    private static BookingDto MapToDto(Booking booking, User? user = null, Court? court = null) => new()
    {
        Id = booking.Id,
        UserId = booking.UserId,
        UserName = user?.FullName ?? booking.User?.FullName ?? "",
        UserEmail = user?.Email ?? booking.User?.Email ?? "",
        CourtId = booking.CourtId,
        CourtName = court?.Name ?? booking.Court?.Name ?? "",
        SportType = court?.SportType.ToString() ?? booking.Court?.SportType.ToString() ?? "",
        StartTime = booking.StartTime,
        EndTime = booking.EndTime,
        Status = booking.Status.ToString(),
        TotalPrice = booking.TotalPrice,
        Notes = booking.Notes,
        CancellationReason = booking.CancellationReason,
        CreatedAt = booking.CreatedAt
    };
}
