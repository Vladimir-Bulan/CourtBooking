using CourtBooking.Application.DTOs.Common;
using CourtBooking.Application.DTOs.Courts;
using CourtBooking.Application.Features.Courts.Commands;
using CourtBooking.Application.Features.Courts.Queries;
using CourtBooking.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourtBooking.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CourtsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ICourtService _courtService;

    public CourtsController(IMediator mediator, ICourtService courtService)
    {
        _mediator = mediator;
        _courtService = courtService;
    }

    /// <summary>Get all courts with pagination and optional filters</summary>
    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<CourtDto>), 200)]
    public async Task<IActionResult> GetAll(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? sportType = null,
        [FromQuery] string? surface = null,
        [FromQuery] decimal? maxHourlyRate = null,
        [FromQuery] bool? isAvailable = null)
    {
        var query = new GetAllCourtsQuery(
            new PaginationParams { Page = page, PageSize = pageSize },
            new CourtFilterRequest { SportType = sportType, Surface = surface, MaxHourlyRate = maxHourlyRate, IsAvailable = isAvailable });

        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>Get court by ID</summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(CourtDto), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var court = await _courtService.GetByIdAsync(id);
        return court is null ? NotFound() : Ok(court);
    }

    /// <summary>Get available courts for a time slot</summary>
    [HttpGet("available")]
    [ProducesResponseType(typeof(IEnumerable<CourtDto>), 200)]
    public async Task<IActionResult> GetAvailable([FromQuery] DateTime startTime, [FromQuery] DateTime endTime)
    {
        try
        {
            var courts = await _courtService.GetAvailableAsync(startTime, endTime);
            return Ok(courts);
        }
        catch (ArgumentException ex) { return BadRequest(new { message = ex.Message }); }
    }

    /// <summary>Create a new court (Admin only)</summary>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(CourtDto), 201)]
    public async Task<IActionResult> Create([FromBody] CreateCourtRequest request)
    {
        var command = new CreateCourtCommand(
            request.Name, request.Description, request.SportType,
            request.Surface, request.HourlyRate, request.Capacity,
            request.OpeningTime, request.ClosingTime, request.ImageUrl);

        var court = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = court.Id }, court);
    }

    /// <summary>Update a court (Admin only)</summary>
    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(CourtDto), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCourtRequest request)
    {
        try
        {
            var court = await _courtService.UpdateAsync(id, request);
            return Ok(court);
        }
        catch (KeyNotFoundException) { return NotFound(); }
    }

    /// <summary>Delete a court (Admin only)</summary>
    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _courtService.DeleteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException) { return NotFound(); }
    }
}
