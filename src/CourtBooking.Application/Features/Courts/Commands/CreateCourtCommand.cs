using CourtBooking.Application.DTOs.Courts;
using CourtBooking.Domain.Enums;
using MediatR;

namespace CourtBooking.Application.Features.Courts.Commands;

public record CreateCourtCommand(
    string Name,
    string Description,
    SportType SportType,
    CourtSurface Surface,
    decimal HourlyRate,
    int Capacity,
    string OpeningTime,
    string ClosingTime,
    string? ImageUrl = null
) : IRequest<CourtDto>;
