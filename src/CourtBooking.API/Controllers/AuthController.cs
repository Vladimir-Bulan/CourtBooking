using CourtBooking.Application.DTOs.Auth;
using CourtBooking.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CourtBooking.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>Register a new user</summary>
    [HttpPost("register")]
    [ProducesResponseType(typeof(AuthResponse), 201)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        try
        {
            var result = await _authService.RegisterAsync(request);
            return CreatedAtAction(nameof(Register), result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>Login with email and password</summary>
    [HttpPost("login")]
    [ProducesResponseType(typeof(AuthResponse), 200)]
    [ProducesResponseType(401)]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        try
        {
            var result = await _authService.LoginAsync(request);
            return Ok(result);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }
}

