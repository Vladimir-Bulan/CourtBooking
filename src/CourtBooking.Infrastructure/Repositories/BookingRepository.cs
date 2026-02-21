using CourtBooking.Domain.Entities;
using CourtBooking.Domain.Enums;
using CourtBooking.Domain.Interfaces;
using CourtBooking.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CourtBooking.Infrastructure.Repositories;

public class BookingRepository : IBookingRepository
{
    private readonly AppDbContext _context;

    public BookingRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Booking?> GetByIdAsync(Guid id) =>
        await _context.Bookings.FindAsync(id);

    public async Task<Booking?> GetByIdWithDetailsAsync(Guid id) =>
        await _context.Bookings
            .Include(b => b.User)
            .Include(b => b.Court)
            .FirstOrDefaultAsync(b => b.Id == id);

    public async Task<IEnumerable<Booking>> GetAllAsync() =>
        await _context.Bookings
            .Include(b => b.User)
            .Include(b => b.Court)
            .OrderByDescending(b => b.StartTime)
            .ToListAsync();

    public async Task<IEnumerable<Booking>> GetByUserIdAsync(Guid userId) =>
        await _context.Bookings
            .Include(b => b.Court)
            .Where(b => b.UserId == userId)
            .OrderByDescending(b => b.StartTime)
            .ToListAsync();

    public async Task<IEnumerable<Booking>> GetByCourtIdAsync(Guid courtId) =>
        await _context.Bookings
            .Include(b => b.User)
            .Where(b => b.CourtId == courtId)
            .OrderByDescending(b => b.StartTime)
            .ToListAsync();

    public async Task<IEnumerable<Booking>> GetByDateRangeAsync(DateTime from, DateTime to) =>
        await _context.Bookings
            .Include(b => b.User)
            .Include(b => b.Court)
            .Where(b => b.StartTime >= from && b.EndTime <= to)
            .OrderBy(b => b.StartTime)
            .ToListAsync();

    public async Task<bool> HasConflictAsync(Guid courtId, DateTime startTime, DateTime endTime, Guid? excludeBookingId = null)
    {
        var query = _context.Bookings
            .Where(b => b.CourtId == courtId &&
                        b.Status != BookingStatus.Cancelled &&
                        b.StartTime < endTime && b.EndTime > startTime);

        if (excludeBookingId.HasValue)
            query = query.Where(b => b.Id != excludeBookingId.Value);

        return await query.AnyAsync();
    }

    public async Task<Booking> CreateAsync(Booking booking)
    {
        _context.Bookings.Add(booking);
        await _context.SaveChangesAsync();
        return booking;
    }

    public async Task<Booking> UpdateAsync(Booking booking)
    {
        _context.Bookings.Update(booking);
        await _context.SaveChangesAsync();
        return booking;
    }
}

