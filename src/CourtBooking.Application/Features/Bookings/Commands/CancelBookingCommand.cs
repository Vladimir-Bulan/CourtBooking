using CourtBooking.Application.DTOs.Bookings;
using MediatR;

namespace CourtBooking.Application.Features.Bookings.Commands;

public record CancelBookingCommand(
    Guid BookingId,
    Guid UserId,
    string? Reason
) : IRequest<BookingDto>;
