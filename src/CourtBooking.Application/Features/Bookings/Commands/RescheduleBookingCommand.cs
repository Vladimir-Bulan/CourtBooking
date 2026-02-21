using CourtBooking.Application.DTOs.Bookings;
using MediatR;

namespace CourtBooking.Application.Features.Bookings.Commands;

public record RescheduleBookingCommand(
    Guid BookingId,
    Guid UserId,
    DateTime NewStartTime,
    DateTime NewEndTime
) : IRequest<BookingDto>;
