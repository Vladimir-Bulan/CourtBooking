using CourtBooking.Application.DTOs.Auth;
using CourtBooking.Application.Interfaces;
using CourtBooking.Domain.Entities;
using CourtBooking.Domain.Interfaces;

namespace CourtBooking.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtService _jwtService;

    public AuthService(IUserRepository userRepository, IJwtService jwtService)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
    }

    public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
    {
        if (await _userRepository.ExistsByEmailAsync(request.Email))
            throw new InvalidOperationException("Email already registered.");

        var user = new User
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email.ToLower(),
            Phone = request.Phone,
            PasswordHash = BCryptHash(request.Password)
        };

        await _userRepository.CreateAsync(user);

        var token = _jwtService.GenerateToken(user.Id, user.Email, user.Role.ToString());

        return new AuthResponse
        {
            Token = token,
            ExpiresAt = DateTime.UtcNow.AddHours(24),
            User = MapToUserDto(user)
        };
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email.ToLower())
            ?? throw new UnauthorizedAccessException("Invalid credentials.");

        if (!BCryptVerify(request.Password, user.PasswordHash))
            throw new UnauthorizedAccessException("Invalid credentials.");

        if (!user.IsActive)
            throw new UnauthorizedAccessException("Account is disabled.");

        var token = _jwtService.GenerateToken(user.Id, user.Email, user.Role.ToString());

        return new AuthResponse
        {
            Token = token,
            ExpiresAt = DateTime.UtcNow.AddHours(24),
            User = MapToUserDto(user)
        };
    }

    private static string BCryptHash(string password) =>
        BCrypt.Net.BCrypt.HashPassword(password);

    private static bool BCryptVerify(string password, string hash) =>
        BCrypt.Net.BCrypt.Verify(password, hash);

    private static UserDto MapToUserDto(User user) => new()
    {
        Id = user.Id,
        FullName = user.FullName,
        Email = user.Email,
        Phone = user.Phone,
        Role = user.Role.ToString()
    };
}

