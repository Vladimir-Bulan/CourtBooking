using CourtBooking.Application.DTOs.Bookings;
using CourtBooking.Application.DTOs.Common;
using CourtBooking.Application.Interfaces;
using MediatR;

namespace CourtBooking.Application.Features.Bookings.Queries;

public class GetMyBookingsQueryHandler : IRequestHandler<GetMyBookingsQuery, PagedResult<BookingDto>>
{
    private readonly IBookingService _bookingService;

    public GetMyBookingsQueryHandler(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    public async Task<PagedResult<BookingDto>> Handle(GetMyBookingsQuery request, CancellationToken cancellationToken)
    {
        return await _bookingService.GetMyBookingsAsync(request.UserId, request.Pagination);
    }
}
