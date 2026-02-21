using CourtBooking.Application.DTOs.Common;
using CourtBooking.Application.DTOs.Courts;
using MediatR;

namespace CourtBooking.Application.Features.Courts.Queries;

public record GetAllCourtsQuery(PaginationParams Pagination, CourtFilterRequest? Filter = null) 
    : IRequest<PagedResult<CourtDto>>;
