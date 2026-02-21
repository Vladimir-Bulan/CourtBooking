using CourtBooking.Application.DTOs.Auth;
using CourtBooking.Application.Interfaces;
using CourtBooking.Application.Services;
using CourtBooking.Domain.Interfaces;
using CourtBooking.Tests.Helpers;
using FluentAssertions;
using Moq;

namespace CourtBooking.Tests.Services;

public class AuthServiceTests
{
    private readonly Mock<IUserRepository> _userRepoMock;
    private readonly Mock<IJwtService> _jwtServiceMock;
    private readonly AuthService _authService;

    public AuthServiceTests()
    {
        _userRepoMock = new Mock<IUserRepository>();
        _jwtServiceMock = new Mock<IJwtService>();
        _authService = new AuthService(_userRepoMock.Object, _jwtServiceMock.Object);
    }

    // ===================== REGISTER =====================

    [Fact]
    public async Task Register_WithValidData_ReturnsAuthResponse()
    {
        // Arrange
        var request = new RegisterRequest
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john@test.com",
            Password = "Password123!",
            Phone = "1234567890"
        };

        _userRepoMock.Setup(r => r.ExistsByEmailAsync(request.Email)).ReturnsAsync(false);
        _userRepoMock.Setup(r => r.CreateAsync(It.IsAny<Domain.Entities.User>()))
            .ReturnsAsync((Domain.Entities.User u) => u);
        _jwtServiceMock.Setup(j => j.GenerateToken(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>()))
            .Returns("fake-jwt-token");

        // Act
        var result = await _authService.RegisterAsync(request);

        // Assert
        result.Should().NotBeNull();
        result.Token.Should().Be("fake-jwt-token");
        result.User.Email.Should().Be(request.Email);
        result.User.FullName.Should().Be("John Doe");
        result.User.Role.Should().Be("User");
    }

    [Fact]
    public async Task Register_WithExistingEmail_ThrowsInvalidOperationException()
    {
        // Arrange
        var request = new RegisterRequest
        {
            Email = "existing@test.com",
            Password = "Password123!",
            FirstName = "John",
            LastName = "Doe"
        };

        _userRepoMock.Setup(r => r.ExistsByEmailAsync(request.Email)).ReturnsAsync(true);

        // Act
        var act = async () => await _authService.RegisterAsync(request);

        // Assert
        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("Email already registered.");
    }

    // ===================== LOGIN =====================

    [Fact]
    public async Task Login_WithValidCredentials_ReturnsAuthResponse()
    {
        // Arrange
        var user = TestBuilders.BuildUser(email: "john@test.com");
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword("Password123!");

        var request = new LoginRequest { Email = "john@test.com", Password = "Password123!" };

        _userRepoMock.Setup(r => r.GetByEmailAsync(request.Email)).ReturnsAsync(user);
        _jwtServiceMock.Setup(j => j.GenerateToken(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>()))
            .Returns("fake-jwt-token");

        // Act
        var result = await _authService.LoginAsync(request);

        // Assert
        result.Should().NotBeNull();
        result.Token.Should().Be("fake-jwt-token");
        result.User.Email.Should().Be(user.Email);
    }

    [Fact]
    public async Task Login_WithNonExistentEmail_ThrowsUnauthorizedException()
    {
        // Arrange
        var request = new LoginRequest { Email = "noexiste@test.com", Password = "Password123!" };

        _userRepoMock.Setup(r => r.GetByEmailAsync(request.Email)).ReturnsAsync((Domain.Entities.User?)null);

        // Act
        var act = async () => await _authService.LoginAsync(request);

        // Assert
        await act.Should().ThrowAsync<UnauthorizedAccessException>()
            .WithMessage("Invalid credentials.");
    }

    [Fact]
    public async Task Login_WithWrongPassword_ThrowsUnauthorizedException()
    {
        // Arrange
        var user = TestBuilders.BuildUser(email: "john@test.com");
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword("CorrectPassword123!");

        var request = new LoginRequest { Email = "john@test.com", Password = "WrongPassword!" };

        _userRepoMock.Setup(r => r.GetByEmailAsync(request.Email)).ReturnsAsync(user);

        // Act
        var act = async () => await _authService.LoginAsync(request);

        // Assert
        await act.Should().ThrowAsync<UnauthorizedAccessException>()
            .WithMessage("Invalid credentials.");
    }

    [Fact]
    public async Task Login_WithInactiveUser_ThrowsUnauthorizedException()
    {
        // Arrange
        var user = TestBuilders.BuildUser(email: "john@test.com", isActive: false);
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword("Password123!");

        var request = new LoginRequest { Email = "john@test.com", Password = "Password123!" };

        _userRepoMock.Setup(r => r.GetByEmailAsync(request.Email)).ReturnsAsync(user);

        // Act
        var act = async () => await _authService.LoginAsync(request);

        // Assert
        await act.Should().ThrowAsync<UnauthorizedAccessException>()
            .WithMessage("Account is disabled.");
    }
}
