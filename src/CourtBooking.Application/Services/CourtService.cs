using CourtBooking.Application.DTOs.Courts;
using CourtBooking.Application.Interfaces;
using CourtBooking.Domain.Entities;
using CourtBooking.Domain.Interfaces;

namespace CourtBooking.Application.Services;

public class CourtService : ICourtService
{
    private readonly ICourtRepository _courtRepository;

    public CourtService(ICourtRepository courtRepository)
    {
        _courtRepository = courtRepository;
    }

    public async Task<IEnumerable<CourtDto>> GetAllAsync()
    {
        var courts = await _courtRepository.GetAllAsync();
        return courts.Select(MapToDto);
    }

    public async Task<CourtDto?> GetByIdAsync(Guid id)
    {
        var court = await _courtRepository.GetByIdAsync(id);
        return court is null ? null : MapToDto(court);
    }

    public async Task<IEnumerable<CourtDto>> GetAvailableAsync(DateTime startTime, DateTime endTime)
    {
        if (endTime <= startTime)
            throw new ArgumentException("End time must be after start time.");

        var courts = await _courtRepository.GetAvailableAsync(startTime, endTime);
        return courts.Select(MapToDto);
    }

    public async Task<CourtDto> CreateAsync(CreateCourtRequest request)
    {
        var court = new Court
        {
            Name = request.Name,
            Description = request.Description,
            SportType = request.SportType,
            Surface = request.Surface,
            HourlyRate = request.HourlyRate,
            Capacity = request.Capacity,
            ImageUrl = request.ImageUrl,
            OpeningTime = TimeSpan.Parse(request.OpeningTime),
            ClosingTime = TimeSpan.Parse(request.ClosingTime)
        };

        await _courtRepository.CreateAsync(court);
        return MapToDto(court);
    }

    public async Task<CourtDto> UpdateAsync(Guid id, UpdateCourtRequest request)
    {
        var court = await _courtRepository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Court {id} not found.");

        if (request.Name is not null) court.Name = request.Name;
        if (request.Description is not null) court.Description = request.Description;
        if (request.HourlyRate.HasValue) court.HourlyRate = request.HourlyRate.Value;
        if (request.Capacity.HasValue) court.Capacity = request.Capacity.Value;
        if (request.IsAvailable.HasValue) court.IsAvailable = request.IsAvailable.Value;
        if (request.ImageUrl is not null) court.ImageUrl = request.ImageUrl;
        if (request.OpeningTime is not null) court.OpeningTime = TimeSpan.Parse(request.OpeningTime);
        if (request.ClosingTime is not null) court.ClosingTime = TimeSpan.Parse(request.ClosingTime);

        court.UpdatedAt = DateTime.UtcNow;

        await _courtRepository.UpdateAsync(court);
        return MapToDto(court);
    }

    public async Task DeleteAsync(Guid id)
    {
        var court = await _courtRepository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Court {id} not found.");

        await _courtRepository.DeleteAsync(court.Id);
    }

    private static CourtDto MapToDto(Court court) => new()
    {
        Id = court.Id,
        Name = court.Name,
        Description = court.Description,
        SportType = court.SportType.ToString(),
        Surface = court.Surface.ToString(),
        HourlyRate = court.HourlyRate,
        Capacity = court.Capacity,
        IsAvailable = court.IsAvailable,
        ImageUrl = court.ImageUrl,
        OpeningTime = court.OpeningTime.ToString(@"hh\:mm"),
        ClosingTime = court.ClosingTime.ToString(@"hh\:mm")
    };
}

