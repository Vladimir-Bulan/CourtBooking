using CourtBooking.Application.DTOs.Auth;
using CourtBooking.Application.DTOs.Bookings;
using CourtBooking.Application.DTOs.Common;
using CourtBooking.Application.DTOs.Courts;

namespace CourtBooking.Application.Interfaces;

public interface IAuthService
{
    Task<AuthResponse> RegisterAsync(RegisterRequest request);
    Task<AuthResponse> LoginAsync(LoginRequest request);
}

public interface ICourtService
{
    Task<PagedResult<CourtDto>> GetAllAsync(PaginationParams pagination, CourtFilterRequest? filter = null);
    Task<CourtDto?> GetByIdAsync(Guid id);
    Task<IEnumerable<CourtDto>> GetAvailableAsync(DateTime startTime, DateTime endTime);
    Task<CourtDto> CreateAsync(CreateCourtRequest request);
    Task<CourtDto> UpdateAsync(Guid id, UpdateCourtRequest request);
    Task DeleteAsync(Guid id);
}

public interface IBookingService
{
    Task<PagedResult<BookingDto>> GetAllAsync(BookingFilterRequest? filter = null, PaginationParams? pagination = null);
    Task<BookingDto?> GetByIdAsync(Guid id);
    Task<PagedResult<BookingDto>> GetMyBookingsAsync(Guid userId, PaginationParams pagination);
    Task<BookingDto> CreateAsync(Guid userId, CreateBookingRequest request);
    Task<BookingDto> RescheduleAsync(Guid bookingId, Guid userId, RescheduleBookingRequest request);
    Task<BookingDto> CancelAsync(Guid bookingId, Guid userId, CancelBookingRequest request);
    Task<BookingDto> ConfirmAsync(Guid bookingId);
}

public interface IEmailService
{
    Task SendBookingConfirmationAsync(string toEmail, string userName, string courtName, DateTime startTime, DateTime endTime, decimal totalPrice);
    Task SendBookingCancellationAsync(string toEmail, string userName, string courtName, DateTime startTime, string? reason);
    Task SendBookingRescheduleAsync(string toEmail, string userName, string courtName, DateTime newStartTime, DateTime newEndTime);
}

public interface IJwtService
{
    string GenerateToken(Guid userId, string email, string role);
    bool ValidateToken(string token);
    Guid GetUserIdFromToken(string token);
}
