using CourtBooking.Application.DTOs.Bookings;
using MediatR;

namespace CourtBooking.Application.Features.Bookings.Queries;

public record GetBookingByIdQuery(Guid BookingId) : IRequest<BookingDto?>;
