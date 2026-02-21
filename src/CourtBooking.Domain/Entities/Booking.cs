using CourtBooking.Domain.Common;
using CourtBooking.Domain.Enums;

namespace CourtBooking.Domain.Entities;

public class Booking : BaseEntity
{
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    public Guid CourtId { get; set; }
    public Court Court { get; set; } = null!;

    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public BookingStatus Status { get; set; } = BookingStatus.Pending;
    public decimal TotalPrice { get; set; }
    public string? Notes { get; set; }
    public string? CancellationReason { get; set; }
    public DateTime? CancelledAt { get; set; }

    public int DurationInHours => (int)(EndTime - StartTime).TotalHours;
}

