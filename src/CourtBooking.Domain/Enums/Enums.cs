namespace CourtBooking.Domain.Enums;

public enum UserRole
{
    User = 1,
    Admin = 2
}

public enum BookingStatus
{
    Pending = 1,
    Confirmed = 2,
    Cancelled = 3,
    Completed = 4
}

public enum CourtSurface
{
    Grass = 1,
    Clay = 2,
    Hard = 3,
    Synthetic = 4
}

public enum SportType
{
    Football = 1,
    Padel = 2,
    Tennis = 3,
    Basketball = 4,
    Volleyball = 5
}

