using CourtBooking.Domain.Entities;

namespace CourtBooking.Domain.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id);
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByRefreshTokenAsync(string refreshToken);
    Task<IEnumerable<User>> GetAllAsync();
    Task<User> CreateAsync(User user);
    Task<User> UpdateAsync(User user);
    Task DeleteAsync(Guid id);
    Task<bool> ExistsByEmailAsync(string email);
}

public interface ICourtRepository
{
    Task<Court?> GetByIdAsync(Guid id);
    Task<IEnumerable<Court>> GetAllAsync();
    Task<IEnumerable<Court>> GetAvailableAsync(DateTime startTime, DateTime endTime);
    Task<Court> CreateAsync(Court court);
    Task<Court> UpdateAsync(Court court);
    Task DeleteAsync(Guid id);
}

public interface IBookingRepository
{
    Task<Booking?> GetByIdAsync(Guid id);
    Task<Booking?> GetByIdWithDetailsAsync(Guid id);
    Task<IEnumerable<Booking>> GetAllAsync();
    Task<IEnumerable<Booking>> GetByUserIdAsync(Guid userId);
    Task<IEnumerable<Booking>> GetByCourtIdAsync(Guid courtId);
    Task<IEnumerable<Booking>> GetByDateRangeAsync(DateTime from, DateTime to);
    Task<bool> HasConflictAsync(Guid courtId, DateTime startTime, DateTime endTime, Guid? excludeBookingId = null);
    Task<Booking> CreateAsync(Booking booking);
    Task<Booking> UpdateAsync(Booking booking);
}
