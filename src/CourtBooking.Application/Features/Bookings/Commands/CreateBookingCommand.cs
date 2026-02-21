using CourtBooking.Application.DTOs.Bookings;
using MediatR;

namespace CourtBooking.Application.Features.Bookings.Commands;

public record CreateBookingCommand(
    Guid UserId,
    Guid CourtId,
    DateTime StartTime,
    DateTime EndTime,
    string? Notes
) : IRequest<BookingDto>;
