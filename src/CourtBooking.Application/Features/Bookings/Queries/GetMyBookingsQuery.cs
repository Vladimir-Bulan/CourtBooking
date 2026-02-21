using CourtBooking.Application.DTOs.Bookings;
using CourtBooking.Application.DTOs.Common;
using MediatR;

namespace CourtBooking.Application.Features.Bookings.Queries;

public record GetMyBookingsQuery(Guid UserId, PaginationParams Pagination) : IRequest<PagedResult<BookingDto>>;
