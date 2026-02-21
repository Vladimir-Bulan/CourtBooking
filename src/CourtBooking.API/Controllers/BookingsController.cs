using System.Security.Claims;
using CourtBooking.Application.DTOs.Bookings;
using CourtBooking.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourtBooking.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class BookingsController : ControllerBase
{
    private readonly IBookingService _bookingService;

    public BookingsController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    private Guid CurrentUserId =>
        Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    /// <summary>Get all bookings (Admin only)</summary>
    [HttpGet]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(IEnumerable<BookingDto>), 200)]
    public async Task<IActionResult> GetAll([FromQuery] BookingFilterRequest? filter)
    {
        var bookings = await _bookingService.GetAllAsync(filter);
        return Ok(bookings);
    }

    /// <summary>Get my bookings</summary>
    [HttpGet("my")]
    [ProducesResponseType(typeof(IEnumerable<BookingDto>), 200)]
    public async Task<IActionResult> GetMyBookings()
    {
        var bookings = await _bookingService.GetMyBookingsAsync(CurrentUserId);
        return Ok(bookings);
    }

    /// <summary>Get booking by ID</summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(BookingDto), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var booking = await _bookingService.GetByIdAsync(id);
        return booking is null ? NotFound() : Ok(booking);
    }

    /// <summary>Create a new booking</summary>
    [HttpPost]
    [ProducesResponseType(typeof(BookingDto), 201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(409)]
    public async Task<IActionResult> Create([FromBody] CreateBookingRequest request)
    {
        try
        {
            var booking = await _bookingService.CreateAsync(CurrentUserId, request);
            return CreatedAtAction(nameof(GetById), new { id = booking.Id }, booking);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    /// <summary>Reschedule a booking</summary>
    [HttpPut("{id:guid}/reschedule")]
    [ProducesResponseType(typeof(BookingDto), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(403)]
    public async Task<IActionResult> Reschedule(Guid id, [FromBody] RescheduleBookingRequest request)
    {
        try
        {
            var booking = await _bookingService.RescheduleAsync(id, CurrentUserId, request);
            return Ok(booking);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Forbid();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>Cancel a booking</summary>
    [HttpPut("{id:guid}/cancel")]
    [ProducesResponseType(typeof(BookingDto), 200)]
    [ProducesResponseType(403)]
    public async Task<IActionResult> Cancel(Guid id, [FromBody] CancelBookingRequest request)
    {
        try
        {
            var booking = await _bookingService.CancelAsync(id, CurrentUserId, request);
            return Ok(booking);
        }
        catch (UnauthorizedAccessException)
        {
            return Forbid();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>Confirm a booking (Admin only)</summary>
    [HttpPut("{id:guid}/confirm")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(BookingDto), 200)]
    public async Task<IActionResult> Confirm(Guid id)
    {
        try
        {
            var booking = await _bookingService.ConfirmAsync(id);
            return Ok(booking);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}

