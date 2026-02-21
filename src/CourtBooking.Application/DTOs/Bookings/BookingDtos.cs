using System.ComponentModel.DataAnnotations;

namespace CourtBooking.Application.DTOs.Bookings;

public class BookingDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string UserEmail { get; set; } = string.Empty;
    public Guid CourtId { get; set; }
    public string CourtName { get; set; } = string.Empty;
    public string SportType { get; set; } = string.Empty;
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string Status { get; set; } = string.Empty;
    public decimal TotalPrice { get; set; }
    public string? Notes { get; set; }
    public string? CancellationReason { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CreateBookingRequest
{
    [Required] public Guid CourtId { get; set; }
    [Required] public DateTime StartTime { get; set; }
    [Required] public DateTime EndTime { get; set; }
    public string? Notes { get; set; }
}

public class RescheduleBookingRequest
{
    [Required] public DateTime NewStartTime { get; set; }
    [Required] public DateTime NewEndTime { get; set; }
}

public class CancelBookingRequest
{
    public string? Reason { get; set; }
}

public class BookingFilterRequest
{
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
    public string? Status { get; set; }
    public Guid? CourtId { get; set; }
}

