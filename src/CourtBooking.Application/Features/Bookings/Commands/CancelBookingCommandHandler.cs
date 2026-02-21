using CourtBooking.Application.DTOs.Bookings;
using CourtBooking.Application.Interfaces;
using MediatR;

namespace CourtBooking.Application.Features.Bookings.Commands;

public class CancelBookingCommandHandler : IRequestHandler<CancelBookingCommand, BookingDto>
{
    private readonly IBookingService _bookingService;

    public CancelBookingCommandHandler(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    public async Task<BookingDto> Handle(CancelBookingCommand request, CancellationToken cancellationToken)
    {
        return await _bookingService.CancelAsync(
            request.BookingId,
            request.UserId,
            new CancelBookingRequest { Reason = request.Reason });
    }
}
