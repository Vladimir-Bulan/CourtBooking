using CourtBooking.Application.DTOs.Common;
using CourtBooking.Application.DTOs.Courts;
using CourtBooking.Application.Interfaces;
using MediatR;

namespace CourtBooking.Application.Features.Courts.Queries;

public class GetAllCourtsQueryHandler : IRequestHandler<GetAllCourtsQuery, PagedResult<CourtDto>>
{
    private readonly ICourtService _courtService;

    public GetAllCourtsQueryHandler(ICourtService courtService)
    {
        _courtService = courtService;
    }

    public async Task<PagedResult<CourtDto>> Handle(GetAllCourtsQuery request, CancellationToken cancellationToken)
    {
        return await _courtService.GetAllAsync(request.Pagination, request.Filter);
    }
}
