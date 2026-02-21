using CourtBooking.Domain.Entities;
using CourtBooking.Domain.Enums;
using CourtBooking.Domain.Interfaces;
using CourtBooking.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CourtBooking.Infrastructure.Repositories;

public class CourtRepository : ICourtRepository
{
    private readonly AppDbContext _context;

    public CourtRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Court?> GetByIdAsync(Guid id) =>
        await _context.Courts.FindAsync(id);

    public async Task<IEnumerable<Court>> GetAllAsync() =>
        await _context.Courts.OrderBy(c => c.Name).ToListAsync();

    public async Task<IEnumerable<Court>> GetAvailableAsync(DateTime startTime, DateTime endTime)
    {
        // Get courts that don't have conflicting bookings
        var bookedCourtIds = await _context.Bookings
            .Where(b => b.Status != BookingStatus.Cancelled &&
                        b.StartTime < endTime && b.EndTime > startTime)
            .Select(b => b.CourtId)
            .Distinct()
            .ToListAsync();

        return await _context.Courts
            .Where(c => c.IsAvailable && !bookedCourtIds.Contains(c.Id))
            .ToListAsync();
    }

    public async Task<Court> CreateAsync(Court court)
    {
        _context.Courts.Add(court);
        await _context.SaveChangesAsync();
        return court;
    }

    public async Task<Court> UpdateAsync(Court court)
    {
        _context.Courts.Update(court);
        await _context.SaveChangesAsync();
        return court;
    }

    public async Task DeleteAsync(Guid id)
    {
        var court = await _context.Courts.FindAsync(id);
        if (court is not null)
        {
            court.IsDeleted = true;
            await _context.SaveChangesAsync();
        }
    }
}

