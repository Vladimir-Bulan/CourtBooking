using System.Security.Claims;
using CourtBooking.Application.DTOs.Bookings;
using CourtBooking.Application.DTOs.Common;
using CourtBooking.Application.Features.Bookings.Commands;
using CourtBooking.Application.Features.Bookings.Queries;
using CourtBooking.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourtBooking.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class BookingsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IBookingService _bookingService;

    public BookingsController(IMediator mediator, IBookingService bookingService)
    {
        _mediator = mediator;
        _bookingService = bookingService;
    }

    private Guid CurrentUserId =>
        Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    /// <summary>Get all bookings with pagination (Admin only)</summary>
    [HttpGet]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(PagedResult<BookingDto>), 200)]
    public async Task<IActionResult> GetAll(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? status = null,
        [FromQuery] Guid? courtId = null,
        [FromQuery] DateTime? from = null,
        [FromQuery] DateTime? to = null)
    {
        var pagination = new PaginationParams { Page = page, PageSize = pageSize };
        var filter = new BookingFilterRequest { Status = status, CourtId = courtId, From = from, To = to };
        var bookings = await _bookingService.GetAllAsync(filter, pagination);
        return Ok(bookings);
    }

    /// <summary>Get my bookings with pagination</summary>
    [HttpGet("my")]
    [ProducesResponseType(typeof(PagedResult<BookingDto>), 200)]
    public async Task<IActionResult> GetMyBookings([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var query = new GetMyBookingsQuery(CurrentUserId, new PaginationParams { Page = page, PageSize = pageSize });
        var bookings = await _mediator.Send(query);
        return Ok(bookings);
    }

    /// <summary>Get booking by ID</summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(BookingDto), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var booking = await _mediator.Send(new GetBookingByIdQuery(id));
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
            var command = new CreateBookingCommand(
                CurrentUserId,
                request.CourtId,
                request.StartTime,
                request.EndTime,
                request.Notes);

            var booking = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = booking.Id }, booking);
        }
        catch (ArgumentException ex) { return BadRequest(new { message = ex.Message }); }
        catch (InvalidOperationException ex) { return Conflict(new { message = ex.Message }); }
        catch (KeyNotFoundException ex) { return NotFound(new { message = ex.Message }); }
    }

    /// <summary>Reschedule a booking</summary>
    [HttpPut("{id:guid}/reschedule")]
    [ProducesResponseType(typeof(BookingDto), 200)]
    public async Task<IActionResult> Reschedule(Guid id, [FromBody] RescheduleBookingRequest request)
    {
        try
        {
            var command = new RescheduleBookingCommand(id, CurrentUserId, request.NewStartTime, request.NewEndTime);
            var booking = await _mediator.Send(command);
            return Ok(booking);
        }
        catch (UnauthorizedAccessException) { return Forbid(); }
        catch (InvalidOperationException ex) { return BadRequest(new { message = ex.Message }); }
        catch (KeyNotFoundException) { return NotFound(); }
    }

    /// <summary>Cancel a booking</summary>
    [HttpPut("{id:guid}/cancel")]
    [ProducesResponseType(typeof(BookingDto), 200)]
    public async Task<IActionResult> Cancel(Guid id, [FromBody] CancelBookingRequest request)
    {
        try
        {
            var command = new CancelBookingCommand(id, CurrentUserId, request.Reason);
            var booking = await _mediator.Send(command);
            return Ok(booking);
        }
        catch (UnauthorizedAccessException) { return Forbid(); }
        catch (InvalidOperationException ex) { return BadRequest(new { message = ex.Message }); }
        catch (KeyNotFoundException) { return NotFound(); }
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
        catch (KeyNotFoundException) { return NotFound(); }
    }
}
