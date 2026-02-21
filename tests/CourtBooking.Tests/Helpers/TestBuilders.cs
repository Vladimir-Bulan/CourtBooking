using CourtBooking.Domain.Entities;
using CourtBooking.Domain.Enums;

namespace CourtBooking.Tests.Helpers;

public static class TestBuilders
{
    public static User BuildUser(
        Guid? id = null,
        string email = "test@test.com",
        string firstName = "John",
        string lastName = "Doe",
        UserRole role = UserRole.User,
        bool isActive = true)
    {
        return new User
        {
            Id = id ?? Guid.NewGuid(),
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Password123!"),
            Phone = "1234567890",
            Role = role,
            IsActive = isActive,
            CreatedAt = DateTime.UtcNow
        };
    }

    public static Court BuildCourt(
        Guid? id = null,
        string name = "Test Court",
        decimal hourlyRate = 1000,
        bool isAvailable = true,
        SportType sportType = SportType.Padel)
    {
        return new Court
        {
            Id = id ?? Guid.NewGuid(),
            Name = name,
            Description = "Test court description",
            SportType = sportType,
            Surface = CourtSurface.Synthetic,
            HourlyRate = hourlyRate,
            Capacity = 4,
            IsAvailable = isAvailable,
            OpeningTime = new TimeSpan(8, 0, 0),
            ClosingTime = new TimeSpan(22, 0, 0),
            CreatedAt = DateTime.UtcNow
        };
    }

    public static Booking BuildBooking(
        Guid? id = null,
        Guid? userId = null,
        Guid? courtId = null,
        DateTime? startTime = null,
        DateTime? endTime = null,
        BookingStatus status = BookingStatus.Confirmed,
        decimal totalPrice = 1000)
    {
        var start = startTime ?? DateTime.UtcNow.AddDays(1).Date.AddHours(10);
        var end = endTime ?? start.AddHours(1);

        return new Booking
        {
            Id = id ?? Guid.NewGuid(),
            UserId = userId ?? Guid.NewGuid(),
            CourtId = courtId ?? Guid.NewGuid(),
            StartTime = start,
            EndTime = end,
            Status = status,
            TotalPrice = totalPrice,
            CreatedAt = DateTime.UtcNow
        };
    }
}
