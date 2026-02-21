using CourtBooking.Domain.Common;
using CourtBooking.Domain.Enums;

namespace CourtBooking.Domain.Entities;

public class Court : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public SportType SportType { get; set; }
    public CourtSurface Surface { get; set; }
    public decimal HourlyRate { get; set; }
    public int Capacity { get; set; }
    public bool IsAvailable { get; set; } = true;
    public string ImageUrl { get; set; } = string.Empty;

    // Opening hours (stored as TimeOnly)
    public TimeSpan OpeningTime { get; set; } = new TimeSpan(8, 0, 0);
    public TimeSpan ClosingTime { get; set; } = new TimeSpan(22, 0, 0);

    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}

