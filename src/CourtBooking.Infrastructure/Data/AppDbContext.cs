using CourtBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CourtBooking.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Court> Courts => Set<Court>();
    public DbSet<Booking> Bookings => Set<Booking>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // User
        modelBuilder.Entity<User>(e =>
        {
            e.HasKey(u => u.Id);
            e.HasIndex(u => u.Email).IsUnique();
            e.Property(u => u.Email).HasMaxLength(255).IsRequired();
            e.Property(u => u.FirstName).HasMaxLength(100).IsRequired();
            e.Property(u => u.LastName).HasMaxLength(100).IsRequired();
            e.Property(u => u.PasswordHash).IsRequired();
            e.Property(u => u.Phone).HasMaxLength(20);
            e.HasQueryFilter(u => !u.IsDeleted);
        });

        // Court
        modelBuilder.Entity<Court>(e =>
        {
            e.HasKey(c => c.Id);
            e.Property(c => c.Name).HasMaxLength(150).IsRequired();
            e.Property(c => c.HourlyRate).HasColumnType("decimal(10,2)");
            e.HasQueryFilter(c => !c.IsDeleted);
        });

        // Booking
        modelBuilder.Entity<Booking>(e =>
        {
            e.HasKey(b => b.Id);
            e.Property(b => b.TotalPrice).HasColumnType("decimal(10,2)");

            e.HasOne(b => b.User)
                .WithMany(u => u.Bookings)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            e.HasOne(b => b.Court)
                .WithMany(c => c.Bookings)
                .HasForeignKey(b => b.CourtId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Seed admin user
        SeedData(modelBuilder);
    }

    private static void SeedData(ModelBuilder modelBuilder)
    {
        var adminId = Guid.Parse("00000000-0000-0000-0000-000000000001");
        modelBuilder.Entity<User>().HasData(new User
        {
            Id = adminId,
            FirstName = "Admin",
            LastName = "CourtBooking",
            Email = "admin@courtbooking.com",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
            Phone = "000000000",
            Role = Domain.Enums.UserRole.Admin,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        });

        var courts = new[]
        {
            new Court { Id = Guid.NewGuid(), Name = "Cancha Padel 1", Description = "Cancha de padel cubierta", SportType = Domain.Enums.SportType.Padel, Surface = Domain.Enums.CourtSurface.Synthetic, HourlyRate = 1500, Capacity = 4, CreatedAt = DateTime.UtcNow },
            new Court { Id = Guid.NewGuid(), Name = "Cancha FÃºtbol 5", Description = "Cancha de fÃºtbol 5 con iluminaciÃ³n", SportType = Domain.Enums.SportType.Football, Surface = Domain.Enums.CourtSurface.Synthetic, HourlyRate = 2500, Capacity = 10, CreatedAt = DateTime.UtcNow },
            new Court { Id = Guid.NewGuid(), Name = "Cancha Tenis", Description = "Cancha de tenis en polvo de ladrillo", SportType = Domain.Enums.SportType.Tennis, Surface = Domain.Enums.CourtSurface.Clay, HourlyRate = 1200, Capacity = 4, CreatedAt = DateTime.UtcNow },
        };
        modelBuilder.Entity<Court>().HasData(courts);
    }
}

