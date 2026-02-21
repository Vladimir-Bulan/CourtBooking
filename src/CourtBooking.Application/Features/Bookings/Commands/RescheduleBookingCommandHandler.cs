using CourtBooking.Application.DTOs.Bookings;
using CourtBooking.Application.Interfaces;
using MediatR;

namespace CourtBooking.Application.Features.Bookings.Commands;

public class RescheduleBookingCommandHandler : IRequestHandler<RescheduleBookingCommand, BookingDto>
{
    private readonly IBookingService _bookingService;

    public RescheduleBookingCommandHandler(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    public async Task<BookingDto> Handle(RescheduleBookingCommand request, CancellationToken cancellationToken)
    {
        return await _bookingService.RescheduleAsync(
            request.BookingId,
            request.UserId,
            new RescheduleBookingRequest
            {
                NewStartTime = request.NewStartTime,
                NewEndTime = request.NewEndTime
            });
    }
}
