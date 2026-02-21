using CourtBooking.Application.DTOs.Bookings;
using CourtBooking.Application.Interfaces;
using MediatR;

namespace CourtBooking.Application.Features.Bookings.Commands;

public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, BookingDto>
{
    private readonly IBookingService _bookingService;

    public CreateBookingCommandHandler(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    public async Task<BookingDto> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
    {
        var createRequest = new CreateBookingRequest
        {
            CourtId = request.CourtId,
            StartTime = request.StartTime,
            EndTime = request.EndTime,
            Notes = request.Notes
        };

        return await _bookingService.CreateAsync(request.UserId, createRequest);
    }
}
