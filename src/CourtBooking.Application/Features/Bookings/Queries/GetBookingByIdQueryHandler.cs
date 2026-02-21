using CourtBooking.Application.DTOs.Bookings;
using CourtBooking.Application.Interfaces;
using MediatR;

namespace CourtBooking.Application.Features.Bookings.Queries;

public class GetBookingByIdQueryHandler : IRequestHandler<GetBookingByIdQuery, BookingDto?>
{
    private readonly IBookingService _bookingService;

    public GetBookingByIdQueryHandler(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    public async Task<BookingDto?> Handle(GetBookingByIdQuery request, CancellationToken cancellationToken)
    {
        return await _bookingService.GetByIdAsync(request.BookingId);
    }
}
