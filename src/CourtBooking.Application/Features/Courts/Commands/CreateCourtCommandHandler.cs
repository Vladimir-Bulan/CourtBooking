using CourtBooking.Application.DTOs.Courts;
using CourtBooking.Application.Interfaces;
using MediatR;

namespace CourtBooking.Application.Features.Courts.Commands;

public class CreateCourtCommandHandler : IRequestHandler<CreateCourtCommand, CourtDto>
{
    private readonly ICourtService _courtService;

    public CreateCourtCommandHandler(ICourtService courtService)
    {
        _courtService = courtService;
    }

    public async Task<CourtDto> Handle(CreateCourtCommand request, CancellationToken cancellationToken)
    {
        var createRequest = new CreateCourtRequest
        {
            Name = request.Name,
            Description = request.Description,
            SportType = request.SportType,
            Surface = request.Surface,
            HourlyRate = request.HourlyRate,
            Capacity = request.Capacity,
            OpeningTime = request.OpeningTime,
            ClosingTime = request.ClosingTime,
            ImageUrl = request.ImageUrl
        };

        return await _courtService.CreateAsync(createRequest);
    }
}
