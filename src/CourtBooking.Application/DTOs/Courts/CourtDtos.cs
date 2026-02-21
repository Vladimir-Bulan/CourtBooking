using System.ComponentModel.DataAnnotations;
using CourtBooking.Domain.Enums;

namespace CourtBooking.Application.DTOs.Courts;

public class CourtDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string SportType { get; set; } = string.Empty;
    public string Surface { get; set; } = string.Empty;
    public decimal HourlyRate { get; set; }
    public int Capacity { get; set; }
    public bool IsAvailable { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public string OpeningTime { get; set; } = string.Empty;
    public string ClosingTime { get; set; } = string.Empty;
}

public class CreateCourtRequest
{
    [Required] public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    [Required] public SportType SportType { get; set; }
    [Required] public CourtSurface Surface { get; set; }
    [Range(1, 10000)] public decimal HourlyRate { get; set; }
    [Range(1, 100)] public int Capacity { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public string OpeningTime { get; set; } = "08:00";
    public string ClosingTime { get; set; } = "22:00";
}

public class UpdateCourtRequest
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal? HourlyRate { get; set; }
    public int? Capacity { get; set; }
    public bool? IsAvailable { get; set; }
    public string? ImageUrl { get; set; }
    public string? OpeningTime { get; set; }
    public string? ClosingTime { get; set; }
}

public class CourtAvailabilityRequest
{
    [Required] public DateTime StartTime { get; set; }
    [Required] public DateTime EndTime { get; set; }
}

public class CourtFilterRequest
{
    public string? SportType { get; set; }
    public string? Surface { get; set; }
    public decimal? MaxHourlyRate { get; set; }
    public bool? IsAvailable { get; set; }
}
