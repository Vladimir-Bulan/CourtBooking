using CourtBooking.Domain.Interfaces;
using CourtBooking.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourtBooking.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class AdminController : ControllerBase
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICourtRepository _courtRepository;

    public AdminController(
        IBookingRepository bookingRepository,
        IUserRepository userRepository,
        ICourtRepository courtRepository)
    {
        _bookingRepository = bookingRepository;
        _userRepository = userRepository;
        _courtRepository = courtRepository;
    }

    /// <summary>Get dashboard stats</summary>
    [HttpGet("dashboard")]
    public async Task<IActionResult> GetDashboard()
    {
        var allBookings = (await _bookingRepository.GetAllAsync()).ToList();
        var allUsers = (await _userRepository.GetAllAsync()).ToList();
        var allCourts = (await _courtRepository.GetAllAsync()).ToList();

        var today = DateTime.UtcNow.Date;
        var todayBookings = allBookings.Where(b => b.StartTime.Date == today).ToList();

        var stats = new
        {
            TotalUsers = allUsers.Count(u => u.Role == UserRole.User),
            TotalCourts = allCourts.Count,
            AvailableCourts = allCourts.Count(c => c.IsAvailable),
            TotalBookings = allBookings.Count,
            TodayBookings = todayBookings.Count,
            PendingBookings = allBookings.Count(b => b.Status == BookingStatus.Pending),
            ConfirmedBookings = allBookings.Count(b => b.Status == BookingStatus.Confirmed),
            CancelledBookings = allBookings.Count(b => b.Status == BookingStatus.Cancelled),
            TotalRevenue = allBookings
                .Where(b => b.Status != BookingStatus.Cancelled)
                .Sum(b => b.TotalPrice),
            RevenueThisMonth = allBookings
                .Where(b => b.Status != BookingStatus.Cancelled &&
                            b.StartTime.Month == DateTime.UtcNow.Month &&
                            b.StartTime.Year == DateTime.UtcNow.Year)
                .Sum(b => b.TotalPrice)
        };

        return Ok(stats);
    }

    /// <summary>Get all users</summary>
    [HttpGet("users")]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _userRepository.GetAllAsync();
        var result = users.Select(u => new
        {
            u.Id,
            u.FullName,
            u.Email,
            u.Phone,
            u.Role,
            u.IsActive,
            u.CreatedAt
        });
        return Ok(result);
    }

    /// <summary>Toggle user active status</summary>
    [HttpPut("users/{id:guid}/toggle-status")]
    public async Task<IActionResult> ToggleUserStatus(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user is null) return NotFound();

        user.IsActive = !user.IsActive;
        user.UpdatedAt = DateTime.UtcNow;
        await _userRepository.UpdateAsync(user);

        return Ok(new { user.Id, user.IsActive });
    }
}

