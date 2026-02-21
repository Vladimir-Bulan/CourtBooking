using Xunit;
using CourtBooking.Application.DTOs.Common;
using CourtBooking.Application.DTOs.Courts;
using CourtBooking.Application.Services;
using CourtBooking.Domain.Enums;
using CourtBooking.Domain.Interfaces;
using CourtBooking.Tests.Helpers;
using FluentAssertions;
using Moq;

namespace CourtBooking.Tests.Services;

public class CourtServiceTests
{
    private readonly Mock<ICourtRepository> _courtRepoMock;
    private readonly CourtService _courtService;

    public CourtServiceTests()
    {
        _courtRepoMock = new Mock<ICourtRepository>();
        _courtService = new CourtService(_courtRepoMock.Object);
    }

    [Fact]
    public async Task GetAll_ReturnsMappedDtos()
    {
        // Arrange
        var courts = new[]
        {
            TestBuilders.BuildCourt(name: "Cancha 1"),
            TestBuilders.BuildCourt(name: "Cancha 2"),
            TestBuilders.BuildCourt(name: "Cancha 3"),
        };
        _courtRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(courts);

        // Act
        var result = await _courtService.GetAllAsync(new PaginationParams { Page = 1, PageSize = 10 });

        // Assert
        result.Should().NotBeNull();
        result.TotalCount.Should().Be(3);
        result.Data.Should().HaveCount(3);
        result.Data.Select(c => c.Name).Should().Contain(new[] { "Cancha 1", "Cancha 2", "Cancha 3" });
    }

    [Fact]
    public async Task GetAll_WithPagination_ReturnsCorrectPage()
    {
        // Arrange
        var courts = Enumerable.Range(1, 15)
            .Select(i => TestBuilders.BuildCourt(name: $"Cancha {i}"))
            .ToArray();
        _courtRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(courts);

        // Act
        var result = await _courtService.GetAllAsync(new PaginationParams { Page = 2, PageSize = 5 });

        // Assert
        result.TotalCount.Should().Be(15);
        result.Data.Should().HaveCount(5);
        result.Page.Should().Be(2);
        result.TotalPages.Should().Be(3);
        result.HasNextPage.Should().BeTrue();
        result.HasPreviousPage.Should().BeTrue();
    }

    [Fact]
    public async Task GetAll_WithSportTypeFilter_ReturnsFilteredCourts()
    {
        // Arrange
        var courts = new[]
        {
            TestBuilders.BuildCourt(name: "Padel 1", sportType: SportType.Padel),
            TestBuilders.BuildCourt(name: "Tenis 1", sportType: SportType.Tennis),
            TestBuilders.BuildCourt(name: "Padel 2", sportType: SportType.Padel),
        };
        _courtRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(courts);

        // Act
        var result = await _courtService.GetAllAsync(
            new PaginationParams(),
            new CourtFilterRequest { SportType = "Padel" });

        // Assert
        result.TotalCount.Should().Be(2);
        result.Data.All(c => c.SportType == "Padel").Should().BeTrue();
    }

    [Fact]
    public async Task GetById_ExistingCourt_ReturnsDto()
    {
        // Arrange
        var court = TestBuilders.BuildCourt(name: "Cancha Padel");
        _courtRepoMock.Setup(r => r.GetByIdAsync(court.Id)).ReturnsAsync(court);

        // Act
        var result = await _courtService.GetByIdAsync(court.Id);

        // Assert
        result.Should().NotBeNull();
        result!.Name.Should().Be("Cancha Padel");
        result.HourlyRate.Should().Be(court.HourlyRate);
    }

    [Fact]
    public async Task GetById_NonExistentCourt_ReturnsNull()
    {
        // Arrange
        _courtRepoMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Domain.Entities.Court?)null);

        // Act
        var result = await _courtService.GetByIdAsync(Guid.NewGuid());

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task Create_WithValidData_ReturnsMappedDto()
    {
        // Arrange
        var request = new CreateCourtRequest
        {
            Name = "Cancha Tenis",
            Description = "Cancha de tenis",
            SportType = SportType.Tennis,
            Surface = CourtSurface.Clay,
            HourlyRate = 1200,
            Capacity = 4,
            OpeningTime = "08:00",
            ClosingTime = "22:00"
        };

        _courtRepoMock.Setup(r => r.CreateAsync(It.IsAny<Domain.Entities.Court>()))
            .ReturnsAsync((Domain.Entities.Court c) => c);

        // Act
        var result = await _courtService.CreateAsync(request);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be("Cancha Tenis");
        result.HourlyRate.Should().Be(1200);
        result.SportType.Should().Be("Tennis");
        result.IsAvailable.Should().BeTrue();
    }

    [Fact]
    public async Task GetAvailable_WithInvalidTimeRange_ThrowsArgumentException()
    {
        // Arrange
        var startTime = DateTime.UtcNow.AddHours(2);
        var endTime = DateTime.UtcNow.AddHours(1);

        // Act
        var act = async () => await _courtService.GetAvailableAsync(startTime, endTime);

        // Assert
        await act.Should().ThrowAsync<ArgumentException>()
            .WithMessage("End time must be after start time.");
    }

    [Fact]
    public async Task Update_NonExistentCourt_ThrowsKeyNotFoundException()
    {
        // Arrange
        _courtRepoMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Domain.Entities.Court?)null);

        // Act
        var act = async () => await _courtService.UpdateAsync(Guid.NewGuid(), new UpdateCourtRequest());

        // Assert
        await act.Should().ThrowAsync<KeyNotFoundException>();
    }

    [Fact]
    public async Task Update_ExistingCourt_UpdatesOnlyProvidedFields()
    {
        // Arrange
        var court = TestBuilders.BuildCourt(name: "Nombre Viejo", hourlyRate: 1000);
        _courtRepoMock.Setup(r => r.GetByIdAsync(court.Id)).ReturnsAsync(court);
        _courtRepoMock.Setup(r => r.UpdateAsync(It.IsAny<Domain.Entities.Court>()))
            .ReturnsAsync((Domain.Entities.Court c) => c);

        var request = new UpdateCourtRequest { Name = "Nombre Nuevo" };

        // Act
        var result = await _courtService.UpdateAsync(court.Id, request);

        // Assert
        result.Name.Should().Be("Nombre Nuevo");
        result.HourlyRate.Should().Be(1000);
    }
}