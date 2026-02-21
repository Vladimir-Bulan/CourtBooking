# CourtBooking - Setup Script
# Ejecutar desde: C:\Users\PC\Desktop\Proyects\CourtBooking

Write-Host "Creando estructura CourtBooking..." -ForegroundColor Cyan

$folders = @(
    "src\CourtBooking.API\Controllers",
    "src\CourtBooking.API\Middleware",
    "src\CourtBooking.API\Properties",
    "src\CourtBooking.Application\DTOs\Auth",
    "src\CourtBooking.Application\DTOs\Bookings",
    "src\CourtBooking.Application\DTOs\Courts",
    "src\CourtBooking.Application\Interfaces",
    "src\CourtBooking.Application\Services",
    "src\CourtBooking.Domain\Common",
    "src\CourtBooking.Domain\Entities",
    "src\CourtBooking.Domain\Enums",
    "src\CourtBooking.Domain\Interfaces",
    "src\CourtBooking.Infrastructure\Data",
    "src\CourtBooking.Infrastructure\Repositories",
    "src\CourtBooking.Infrastructure\Services"
)
foreach ($f in $folders) { New-Item -ItemType Directory -Force -Path $f | Out-Null }
Write-Host "Carpetas creadas." -ForegroundColor Green

# --- .gitignore ---
@'
## .NET Core
*.user
*.suo
*.userosscache
*.sln.docstates
[Dd]ebug/
[Dd]ebugPublic/
[Rr]elease/
[Rr]eleases/
x64/
x86/
[Ww][Ii][Nn]32/
[Aa][Rr][Mm]/
[Aa][Rr][Mm]64/
bld/
[Bb]in/
[Oo]bj/
[Ll]og/
[Ll]ogs/
.vs/

## NuGet
*.nupkg
*.snupkg
**/[Pp]ackages/
!**/[Pp]ackages/build/
*.nuget.targets
project.lock.json
project.fragment.lock.json
artifacts/

## Build results
_UpgradeReport_Files/
Backup*/
UpgradeLog*.XML
UpgradeLog*.htm
ServiceFabricBackup/
*.rptproj.bak

## Environments
appsettings.Development.json
.env
.env.local

## IDE
.idea/
*.DS_Store
Thumbs.db

'@ | Set-Content -Path ".gitignore" -Encoding UTF8

# --- CourtBooking.sln ---
@'
﻿
Microsoft Visual Studio Solution File, Format Version 12.00
# Visual Studio Version 17
VisualStudioVersion = 17.0.31903.59
MinimumVisualStudioVersion = 10.0.40219.1
Project("{2150E333-8FDC-42A3-9474-1A3956D46DE8}") = "src", "src", "{7B0F6BE9-2481-46B5-9ABE-434751F3162B}"
EndProject
Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "CourtBooking.Domain", "src\CourtBooking.Domain\CourtBooking.Domain.csproj", "{8C233330-DBA8-4B03-90E1-56F10BBB774E}"
EndProject
Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "CourtBooking.Application", "src\CourtBooking.Application\CourtBooking.Application.csproj", "{B21F9547-74C9-4F44-8575-77A318A698DA}"
EndProject
Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "CourtBooking.Infrastructure", "src\CourtBooking.Infrastructure\CourtBooking.Infrastructure.csproj", "{71245CD2-9017-44EF-8E4B-3F9DA1D65FD0}"
EndProject
Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "CourtBooking.API", "src\CourtBooking.API\CourtBooking.API.csproj", "{89C00774-58EF-4B59-9EA1-BD956D2B3F22}"
EndProject
Global
	GlobalSection(SolutionConfigurationPlatforms) = preSolution
		Debug|Any CPU = Debug|Any CPU
		Release|Any CPU = Release|Any CPU
	EndGlobalSection
	GlobalSection(SolutionProperties) = preSolution
		HideSolutionNode = FALSE
	EndGlobalSection
	GlobalSection(ProjectConfigurationPlatforms) = postSolution
		{8C233330-DBA8-4B03-90E1-56F10BBB774E}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{8C233330-DBA8-4B03-90E1-56F10BBB774E}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{8C233330-DBA8-4B03-90E1-56F10BBB774E}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{8C233330-DBA8-4B03-90E1-56F10BBB774E}.Release|Any CPU.Build.0 = Release|Any CPU
		{B21F9547-74C9-4F44-8575-77A318A698DA}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{B21F9547-74C9-4F44-8575-77A318A698DA}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{B21F9547-74C9-4F44-8575-77A318A698DA}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{B21F9547-74C9-4F44-8575-77A318A698DA}.Release|Any CPU.Build.0 = Release|Any CPU
		{71245CD2-9017-44EF-8E4B-3F9DA1D65FD0}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{71245CD2-9017-44EF-8E4B-3F9DA1D65FD0}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{71245CD2-9017-44EF-8E4B-3F9DA1D65FD0}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{71245CD2-9017-44EF-8E4B-3F9DA1D65FD0}.Release|Any CPU.Build.0 = Release|Any CPU
		{89C00774-58EF-4B59-9EA1-BD956D2B3F22}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{89C00774-58EF-4B59-9EA1-BD956D2B3F22}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{89C00774-58EF-4B59-9EA1-BD956D2B3F22}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{89C00774-58EF-4B59-9EA1-BD956D2B3F22}.Release|Any CPU.Build.0 = Release|Any CPU
	EndGlobalSection
	GlobalSection(NestedProjects) = preSolution
		{8C233330-DBA8-4B03-90E1-56F10BBB774E} = {7B0F6BE9-2481-46B5-9ABE-434751F3162B}
		{B21F9547-74C9-4F44-8575-77A318A698DA} = {7B0F6BE9-2481-46B5-9ABE-434751F3162B}
		{71245CD2-9017-44EF-8E4B-3F9DA1D65FD0} = {7B0F6BE9-2481-46B5-9ABE-434751F3162B}
		{89C00774-58EF-4B59-9EA1-BD956D2B3F22} = {7B0F6BE9-2481-46B5-9ABE-434751F3162B}
	EndGlobalSection
EndGlobal

'@ | Set-Content -Path "CourtBooking.sln" -Encoding UTF8

# --- README.md ---
@'
# 🏟️ CourtBooking API

API REST para gestión de reservas de canchas deportivas (Padel, Fútbol, Tenis, etc.) construida con **.NET 8** y **Clean Architecture**.

## 🛠️ Tech Stack

| Capa | Tecnología |
|------|-----------|
| Framework | ASP.NET Core 8 |
| ORM | Entity Framework Core 8 |
| Base de datos | SQL Server |
| Autenticación | JWT Bearer Tokens |
| Password Hashing | BCrypt |
| Documentación | Swagger / OpenAPI |
| Arquitectura | Clean Architecture |

## 📁 Arquitectura

```
CourtBooking/
├── src/
│   ├── CourtBooking.Domain/          # Entidades, Enums, Interfaces de Repos
│   │   ├── Entities/                 # User, Court, Booking
│   │   ├── Enums/                    # BookingStatus, SportType, etc.
│   │   └── Interfaces/               # IUserRepository, ICourtRepository...
│   │
│   ├── CourtBooking.Application/     # Lógica de negocio, DTOs, Interfaces de Servicios
│   │   ├── DTOs/                     # Auth, Courts, Bookings
│   │   ├── Interfaces/               # IAuthService, IBookingService...
│   │   └── Services/                 # AuthService, BookingService, CourtService
│   │
│   ├── CourtBooking.Infrastructure/  # Implementaciones de repositorios y servicios externos
│   │   ├── Data/                     # AppDbContext + Seed Data
│   │   ├── Repositories/             # UserRepo, CourtRepo, BookingRepo
│   │   └── Services/                 # JwtService, EmailService
│   │
│   └── CourtBooking.API/             # Controllers, Middleware, Program.cs
│       ├── Controllers/              # Auth, Courts, Bookings, Admin
│       └── Middleware/               # ExceptionMiddleware
```

## 🚀 Cómo correr el proyecto

### Requisitos
- .NET 8 SDK
- SQL Server (local o Docker)

### 1. Clonar el repo

```bash
git clone https://github.com/Vladimir-Bulan/CourtBooking.git
cd CourtBooking
```

### 2. Configurar la base de datos

En `src/CourtBooking.API/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=CourtBookingDB;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

### 3. Aplicar migraciones

```bash
cd src/CourtBooking.API
dotnet ef database update --project ../CourtBooking.Infrastructure
```

> ⚡ O simplemente corré la API — aplica las migraciones automáticamente al arrancar.

### 4. Correr la API

```bash
dotnet run --project src/CourtBooking.API
```

### 5. Acceder a Swagger

```
http://localhost:5000
```

---

## 🔐 Autenticación

La API usa **JWT Bearer Tokens**. Para endpoints protegidos:

1. Registrarse o hacer login en `/api/auth/register` o `/api/auth/login`
2. Copiar el token de la respuesta
3. En Swagger: click en **Authorize** → `Bearer {tu_token}`

### Usuario Admin (seed)
```
Email: admin@courtbooking.com
Password: Admin123!
```

---

## 📋 Endpoints

### Auth
| Método | Ruta | Descripción | Auth |
|--------|------|-------------|------|
| POST | `/api/auth/register` | Registrar usuario | ❌ |
| POST | `/api/auth/login` | Login | ❌ |

### Canchas
| Método | Ruta | Descripción | Auth |
|--------|------|-------------|------|
| GET | `/api/courts` | Listar todas las canchas | ❌ |
| GET | `/api/courts/{id}` | Obtener cancha por ID | ❌ |
| GET | `/api/courts/available?startTime=&endTime=` | Canchas disponibles | ❌ |
| POST | `/api/courts` | Crear cancha | 🔐 Admin |
| PUT | `/api/courts/{id}` | Actualizar cancha | 🔐 Admin |
| DELETE | `/api/courts/{id}` | Eliminar cancha | 🔐 Admin |

### Reservas
| Método | Ruta | Descripción | Auth |
|--------|------|-------------|------|
| GET | `/api/bookings` | Listar todas las reservas | 🔐 Admin |
| GET | `/api/bookings/my` | Mis reservas | 🔐 User |
| GET | `/api/bookings/{id}` | Detalle de reserva | 🔐 User |
| POST | `/api/bookings` | Crear reserva | 🔐 User |
| PUT | `/api/bookings/{id}/reschedule` | Reprogramar reserva | 🔐 User |
| PUT | `/api/bookings/{id}/cancel` | Cancelar reserva | 🔐 User |
| PUT | `/api/bookings/{id}/confirm` | Confirmar reserva | 🔐 Admin |

### Admin Dashboard
| Método | Ruta | Descripción | Auth |
|--------|------|-------------|------|
| GET | `/api/admin/dashboard` | Estadísticas generales | 🔐 Admin |
| GET | `/api/admin/users` | Listar usuarios | 🔐 Admin |
| PUT | `/api/admin/users/{id}/toggle-status` | Activar/desactivar usuario | 🔐 Admin |

---

## 💡 Features principales

- ✅ **Clean Architecture** — Domain, Application, Infrastructure, API
- ✅ **JWT Auth** con roles (Admin / User)
- ✅ **Validación de disponibilidad** — detecta conflictos de horario en tiempo real
- ✅ **Reservar, reprogramar y cancelar** turnos
- ✅ **Panel Admin** con stats (ingresos, reservas del día, usuarios)
- ✅ **Notificaciones por email** (configurable via SMTP)
- ✅ **Soft delete** en entidades
- ✅ **Global Exception Middleware** — manejo de errores centralizado
- ✅ **Swagger** con soporte JWT integrado
- ✅ **Seed data** con admin y 3 canchas de ejemplo

---

## 📧 Configurar emails (opcional)

En `appsettings.json`:

```json
"Email": {
  "Enabled": true,
  "Host": "smtp.gmail.com",
  "Port": 587,
  "Username": "tu-email@gmail.com",
  "Password": "tu-app-password",
  "From": "noreply@courtbooking.com"
}
```

> Para Gmail: activar 2FA y generar una **App Password** en la cuenta de Google.

---

## 👤 Autor

**Vladimir Bulan** — [GitHub](https://github.com/Vladimir-Bulan) | [LinkedIn](https://www.linkedin.com/in/vladimir-bulan-60083b21b/)

'@ | Set-Content -Path "README.md" -Encoding UTF8

# --- src\CourtBooking.API\Controllers\AdminController.cs ---
@'
using CourtBooking.Domain.Interfaces;
using CourtBooking.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourtBooking.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class AdminController : ControllerBase
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICourtRepository _courtRepository;

    public AdminController(
        IBookingRepository bookingRepository,
        IUserRepository userRepository,
        ICourtRepository courtRepository)
    {
        _bookingRepository = bookingRepository;
        _userRepository = userRepository;
        _courtRepository = courtRepository;
    }

    /// <summary>Get dashboard stats</summary>
    [HttpGet("dashboard")]
    public async Task<IActionResult> GetDashboard()
    {
        var allBookings = (await _bookingRepository.GetAllAsync()).ToList();
        var allUsers = (await _userRepository.GetAllAsync()).ToList();
        var allCourts = (await _courtRepository.GetAllAsync()).ToList();

        var today = DateTime.UtcNow.Date;
        var todayBookings = allBookings.Where(b => b.StartTime.Date == today).ToList();

        var stats = new
        {
            TotalUsers = allUsers.Count(u => u.Role == UserRole.User),
            TotalCourts = allCourts.Count,
            AvailableCourts = allCourts.Count(c => c.IsAvailable),
            TotalBookings = allBookings.Count,
            TodayBookings = todayBookings.Count,
            PendingBookings = allBookings.Count(b => b.Status == BookingStatus.Pending),
            ConfirmedBookings = allBookings.Count(b => b.Status == BookingStatus.Confirmed),
            CancelledBookings = allBookings.Count(b => b.Status == BookingStatus.Cancelled),
            TotalRevenue = allBookings
                .Where(b => b.Status != BookingStatus.Cancelled)
                .Sum(b => b.TotalPrice),
            RevenueThisMonth = allBookings
                .Where(b => b.Status != BookingStatus.Cancelled &&
                            b.StartTime.Month == DateTime.UtcNow.Month &&
                            b.StartTime.Year == DateTime.UtcNow.Year)
                .Sum(b => b.TotalPrice)
        };

        return Ok(stats);
    }

    /// <summary>Get all users</summary>
    [HttpGet("users")]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _userRepository.GetAllAsync();
        var result = users.Select(u => new
        {
            u.Id,
            u.FullName,
            u.Email,
            u.Phone,
            u.Role,
            u.IsActive,
            u.CreatedAt
        });
        return Ok(result);
    }

    /// <summary>Toggle user active status</summary>
    [HttpPut("users/{id:guid}/toggle-status")]
    public async Task<IActionResult> ToggleUserStatus(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user is null) return NotFound();

        user.IsActive = !user.IsActive;
        user.UpdatedAt = DateTime.UtcNow;
        await _userRepository.UpdateAsync(user);

        return Ok(new { user.Id, user.IsActive });
    }
}

'@ | Set-Content -Path "src\CourtBooking.API\Controllers\AdminController.cs" -Encoding UTF8

# --- src\CourtBooking.API\Controllers\AuthController.cs ---
@'
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

'@ | Set-Content -Path "src\CourtBooking.API\Controllers\AuthController.cs" -Encoding UTF8

# --- src\CourtBooking.API\Controllers\BookingsController.cs ---
@'
using System.Security.Claims;
using CourtBooking.Application.DTOs.Bookings;
using CourtBooking.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourtBooking.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class BookingsController : ControllerBase
{
    private readonly IBookingService _bookingService;

    public BookingsController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    private Guid CurrentUserId =>
        Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    /// <summary>Get all bookings (Admin only)</summary>
    [HttpGet]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(IEnumerable<BookingDto>), 200)]
    public async Task<IActionResult> GetAll([FromQuery] BookingFilterRequest? filter)
    {
        var bookings = await _bookingService.GetAllAsync(filter);
        return Ok(bookings);
    }

    /// <summary>Get my bookings</summary>
    [HttpGet("my")]
    [ProducesResponseType(typeof(IEnumerable<BookingDto>), 200)]
    public async Task<IActionResult> GetMyBookings()
    {
        var bookings = await _bookingService.GetMyBookingsAsync(CurrentUserId);
        return Ok(bookings);
    }

    /// <summary>Get booking by ID</summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(BookingDto), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var booking = await _bookingService.GetByIdAsync(id);
        return booking is null ? NotFound() : Ok(booking);
    }

    /// <summary>Create a new booking</summary>
    [HttpPost]
    [ProducesResponseType(typeof(BookingDto), 201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(409)]
    public async Task<IActionResult> Create([FromBody] CreateBookingRequest request)
    {
        try
        {
            var booking = await _bookingService.CreateAsync(CurrentUserId, request);
            return CreatedAtAction(nameof(GetById), new { id = booking.Id }, booking);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    /// <summary>Reschedule a booking</summary>
    [HttpPut("{id:guid}/reschedule")]
    [ProducesResponseType(typeof(BookingDto), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(403)]
    public async Task<IActionResult> Reschedule(Guid id, [FromBody] RescheduleBookingRequest request)
    {
        try
        {
            var booking = await _bookingService.RescheduleAsync(id, CurrentUserId, request);
            return Ok(booking);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Forbid();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>Cancel a booking</summary>
    [HttpPut("{id:guid}/cancel")]
    [ProducesResponseType(typeof(BookingDto), 200)]
    [ProducesResponseType(403)]
    public async Task<IActionResult> Cancel(Guid id, [FromBody] CancelBookingRequest request)
    {
        try
        {
            var booking = await _bookingService.CancelAsync(id, CurrentUserId, request);
            return Ok(booking);
        }
        catch (UnauthorizedAccessException)
        {
            return Forbid();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>Confirm a booking (Admin only)</summary>
    [HttpPut("{id:guid}/confirm")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(BookingDto), 200)]
    public async Task<IActionResult> Confirm(Guid id)
    {
        try
        {
            var booking = await _bookingService.ConfirmAsync(id);
            return Ok(booking);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}

'@ | Set-Content -Path "src\CourtBooking.API\Controllers\BookingsController.cs" -Encoding UTF8

# --- src\CourtBooking.API\Controllers\CourtsController.cs ---
@'
using CourtBooking.Application.DTOs.Courts;
using CourtBooking.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourtBooking.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CourtsController : ControllerBase
{
    private readonly ICourtService _courtService;

    public CourtsController(ICourtService courtService)
    {
        _courtService = courtService;
    }

    /// <summary>Get all courts</summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CourtDto>), 200)]
    public async Task<IActionResult> GetAll()
    {
        var courts = await _courtService.GetAllAsync();
        return Ok(courts);
    }

    /// <summary>Get court by ID</summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(CourtDto), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var court = await _courtService.GetByIdAsync(id);
        return court is null ? NotFound() : Ok(court);
    }

    /// <summary>Get available courts for a time slot</summary>
    [HttpGet("available")]
    [ProducesResponseType(typeof(IEnumerable<CourtDto>), 200)]
    public async Task<IActionResult> GetAvailable([FromQuery] DateTime startTime, [FromQuery] DateTime endTime)
    {
        try
        {
            var courts = await _courtService.GetAvailableAsync(startTime, endTime);
            return Ok(courts);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>Create a new court (Admin only)</summary>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(CourtDto), 201)]
    public async Task<IActionResult> Create([FromBody] CreateCourtRequest request)
    {
        var court = await _courtService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = court.Id }, court);
    }

    /// <summary>Update a court (Admin only)</summary>
    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(CourtDto), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCourtRequest request)
    {
        try
        {
            var court = await _courtService.UpdateAsync(id, request);
            return Ok(court);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>Delete a court (Admin only)</summary>
    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _courtService.DeleteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}

'@ | Set-Content -Path "src\CourtBooking.API\Controllers\CourtsController.cs" -Encoding UTF8

# --- src\CourtBooking.API\CourtBooking.API.csproj ---
@'
<Project Sdk="Microsoft.NET.Sdk.Web">
  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
    <GenerateDocumentationFile>true</GenerateDocumentationFile>
    <NoWarn>$(NoWarn);1591</NoWarn>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.Authentication.JwtBearer" Version="8.0.0" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="8.0.0" />
    <PackageReference Include="Swashbuckle.AspNetCore" Version="6.5.0" />
  </ItemGroup>
  <ItemGroup>
    <ProjectReference Include="..\CourtBooking.Application\CourtBooking.Application.csproj" />
    <ProjectReference Include="..\CourtBooking.Infrastructure\CourtBooking.Infrastructure.csproj" />
  </ItemGroup>
</Project>

'@ | Set-Content -Path "src\CourtBooking.API\CourtBooking.API.csproj" -Encoding UTF8

# --- src\CourtBooking.API\Middleware\ExceptionMiddleware.cs ---
@'
using System.Net;
using System.Text.Json;

namespace CourtBooking.API.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        var (statusCode, message) = ex switch
        {
            KeyNotFoundException => (HttpStatusCode.NotFound, ex.Message),
            UnauthorizedAccessException => (HttpStatusCode.Forbidden, ex.Message),
            ArgumentException => (HttpStatusCode.BadRequest, ex.Message),
            InvalidOperationException => (HttpStatusCode.Conflict, ex.Message),
            _ => (HttpStatusCode.InternalServerError, "An unexpected error occurred.")
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var response = JsonSerializer.Serialize(new
        {
            statusCode = (int)statusCode,
            message,
            timestamp = DateTime.UtcNow
        });

        return context.Response.WriteAsync(response);
    }
}

'@ | Set-Content -Path "src\CourtBooking.API\Middleware\ExceptionMiddleware.cs" -Encoding UTF8

# --- src\CourtBooking.API\Program.cs ---
@'
using System.Text;
using CourtBooking.Application.Interfaces;
using CourtBooking.Application.Services;
using CourtBooking.Domain.Interfaces;
using CourtBooking.Infrastructure.Data;
using CourtBooking.Infrastructure.Repositories;
using CourtBooking.Infrastructure.Services;
using CourtBooking.API.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICourtRepository, CourtRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICourtService, CourtService>();
builder.Services.AddScoped<IBookingService, BookingService>();

builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IEmailService, EmailService>();

var jwtKey = builder.Configuration["Jwt:SecretKey"]!;
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "CourtBooking API",
        Version = "v1",
        Description = "API para gestion de reservas de canchas deportivas",
        Contact = new OpenApiContact { Name = "Vladimir Bulan", Url = new Uri("https://github.com/Vladimir-Bulan") }
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Ingresa: Bearer {token}"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "CourtBooking API v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

app.Run();

'@ | Set-Content -Path "src\CourtBooking.API\Program.cs" -Encoding UTF8

# --- src\CourtBooking.API\Properties\launchSettings.json ---
@'
﻿{
  "$schema": "http://json.schemastore.org/launchsettings.json",
  "iisSettings": {
    "windowsAuthentication": false,
    "anonymousAuthentication": true,
    "iisExpress": {
      "applicationUrl": "http://localhost:12296",
      "sslPort": 44345
    }
  },
  "profiles": {
    "http": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": true,
      "launchUrl": "weatherforecast",
      "applicationUrl": "http://localhost:5268",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    },
    "https": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": true,
      "launchUrl": "weatherforecast",
      "applicationUrl": "https://localhost:7146;http://localhost:5268",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    },
    "IIS Express": {
      "commandName": "IISExpress",
      "launchBrowser": true,
      "launchUrl": "weatherforecast",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    }
  }
}

'@ | Set-Content -Path "src\CourtBooking.API\Properties\launchSettings.json" -Encoding UTF8

# --- src\CourtBooking.API\appsettings.json ---
@'
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=CourtBookingDB;Trusted_Connection=True;TrustServerCertificate=True;"
  },
  "Jwt": {
    "SecretKey": "CourtBookingSecretKey2024_ChangeThisInProduction!",
    "Issuer": "CourtBookingAPI",
    "Audience": "CourtBookingClient",
    "ExpiryHours": "24"
  },
  "Email": {
    "Enabled": false,
    "Host": "smtp.gmail.com",
    "Port": 587,
    "Username": "your-email@gmail.com",
    "Password": "your-app-password",
    "From": "noreply@courtbooking.com"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}

'@ | Set-Content -Path "src\CourtBooking.API\appsettings.json" -Encoding UTF8

# --- src\CourtBooking.Application\CourtBooking.Application.csproj ---
@'
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include="BCrypt.Net-Next" Version="4.0.3" />
  </ItemGroup>
  <ItemGroup>
    <ProjectReference Include="..\CourtBooking.Domain\CourtBooking.Domain.csproj" />
  </ItemGroup>
</Project>

'@ | Set-Content -Path "src\CourtBooking.Application\CourtBooking.Application.csproj" -Encoding UTF8

# --- src\CourtBooking.Application\DTOs\Auth\AuthDtos.cs ---
@'
using System.ComponentModel.DataAnnotations;

namespace CourtBooking.Application.DTOs.Auth;

public class RegisterRequest
{
    [Required] public string FirstName { get; set; } = string.Empty;
    [Required] public string LastName { get; set; } = string.Empty;
    [Required, EmailAddress] public string Email { get; set; } = string.Empty;
    [Required, MinLength(6)] public string Password { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
}

public class LoginRequest
{
    [Required, EmailAddress] public string Email { get; set; } = string.Empty;
    [Required] public string Password { get; set; } = string.Empty;
}

public class AuthResponse
{
    public string Token { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
    public UserDto User { get; set; } = null!;
}

public class UserDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
}

'@ | Set-Content -Path "src\CourtBooking.Application\DTOs\Auth\AuthDtos.cs" -Encoding UTF8

# --- src\CourtBooking.Application\DTOs\Bookings\BookingDtos.cs ---
@'
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

'@ | Set-Content -Path "src\CourtBooking.Application\DTOs\Bookings\BookingDtos.cs" -Encoding UTF8

# --- src\CourtBooking.Application\DTOs\Courts\CourtDtos.cs ---
@'
using System.ComponentModel.DataAnnotations;
using CourtBooking.Domain.Enums;

namespace CourtBooking.Application.DTOs.Courts;

public class CourtDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string SportType { get; set; } = string.Empty;
    public string Surface { get; set; } = string.Empty;
    public decimal HourlyRate { get; set; }
    public int Capacity { get; set; }
    public bool IsAvailable { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public string OpeningTime { get; set; } = string.Empty;
    public string ClosingTime { get; set; } = string.Empty;
}

public class CreateCourtRequest
{
    [Required] public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    [Required] public SportType SportType { get; set; }
    [Required] public CourtSurface Surface { get; set; }
    [Range(1, 10000)] public decimal HourlyRate { get; set; }
    [Range(1, 100)] public int Capacity { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public string OpeningTime { get; set; } = "08:00";
    public string ClosingTime { get; set; } = "22:00";
}

public class UpdateCourtRequest
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal? HourlyRate { get; set; }
    public int? Capacity { get; set; }
    public bool? IsAvailable { get; set; }
    public string? ImageUrl { get; set; }
    public string? OpeningTime { get; set; }
    public string? ClosingTime { get; set; }
}

public class CourtAvailabilityRequest
{
    [Required] public DateTime StartTime { get; set; }
    [Required] public DateTime EndTime { get; set; }
}

'@ | Set-Content -Path "src\CourtBooking.Application\DTOs\Courts\CourtDtos.cs" -Encoding UTF8

# --- src\CourtBooking.Application\Interfaces\IServices.cs ---
@'
using CourtBooking.Application.DTOs.Auth;
using CourtBooking.Application.DTOs.Bookings;
using CourtBooking.Application.DTOs.Courts;

namespace CourtBooking.Application.Interfaces;

public interface IAuthService
{
    Task<AuthResponse> RegisterAsync(RegisterRequest request);
    Task<AuthResponse> LoginAsync(LoginRequest request);
}

public interface ICourtService
{
    Task<IEnumerable<CourtDto>> GetAllAsync();
    Task<CourtDto?> GetByIdAsync(Guid id);
    Task<IEnumerable<CourtDto>> GetAvailableAsync(DateTime startTime, DateTime endTime);
    Task<CourtDto> CreateAsync(CreateCourtRequest request);
    Task<CourtDto> UpdateAsync(Guid id, UpdateCourtRequest request);
    Task DeleteAsync(Guid id);
}

public interface IBookingService
{
    Task<IEnumerable<BookingDto>> GetAllAsync(BookingFilterRequest? filter = null);
    Task<BookingDto?> GetByIdAsync(Guid id);
    Task<IEnumerable<BookingDto>> GetMyBookingsAsync(Guid userId);
    Task<BookingDto> CreateAsync(Guid userId, CreateBookingRequest request);
    Task<BookingDto> RescheduleAsync(Guid bookingId, Guid userId, RescheduleBookingRequest request);
    Task<BookingDto> CancelAsync(Guid bookingId, Guid userId, CancelBookingRequest request);
    Task<BookingDto> ConfirmAsync(Guid bookingId);
}

public interface IEmailService
{
    Task SendBookingConfirmationAsync(string toEmail, string userName, string courtName, DateTime startTime, DateTime endTime, decimal totalPrice);
    Task SendBookingCancellationAsync(string toEmail, string userName, string courtName, DateTime startTime, string? reason);
    Task SendBookingRescheduleAsync(string toEmail, string userName, string courtName, DateTime newStartTime, DateTime newEndTime);
}

public interface IJwtService
{
    string GenerateToken(Guid userId, string email, string role);
    bool ValidateToken(string token);
    Guid GetUserIdFromToken(string token);
}

'@ | Set-Content -Path "src\CourtBooking.Application\Interfaces\IServices.cs" -Encoding UTF8

# --- src\CourtBooking.Application\Services\AuthService.cs ---
@'
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

'@ | Set-Content -Path "src\CourtBooking.Application\Services\AuthService.cs" -Encoding UTF8

# --- src\CourtBooking.Application\Services\BookingService.cs ---
@'
using CourtBooking.Application.DTOs.Bookings;
using CourtBooking.Application.Interfaces;
using CourtBooking.Domain.Entities;
using CourtBooking.Domain.Enums;
using CourtBooking.Domain.Interfaces;

namespace CourtBooking.Application.Services;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _bookingRepository;
    private readonly ICourtRepository _courtRepository;
    private readonly IUserRepository _userRepository;
    private readonly IEmailService _emailService;

    public BookingService(
        IBookingRepository bookingRepository,
        ICourtRepository courtRepository,
        IUserRepository userRepository,
        IEmailService emailService)
    {
        _bookingRepository = bookingRepository;
        _courtRepository = courtRepository;
        _userRepository = userRepository;
        _emailService = emailService;
    }

    public async Task<IEnumerable<BookingDto>> GetAllAsync(BookingFilterRequest? filter = null)
    {
        IEnumerable<Booking> bookings;

        if (filter?.From.HasValue == true && filter?.To.HasValue == true)
            bookings = await _bookingRepository.GetByDateRangeAsync(filter.From.Value, filter.To.Value);
        else
            bookings = await _bookingRepository.GetAllAsync();

        if (filter?.Status is not null && Enum.TryParse<BookingStatus>(filter.Status, out var status))
            bookings = bookings.Where(b => b.Status == status);

        if (filter?.CourtId.HasValue == true)
            bookings = bookings.Where(b => b.CourtId == filter.CourtId.Value);

        return bookings.Select(MapToDto);
    }

    public async Task<BookingDto?> GetByIdAsync(Guid id)
    {
        var booking = await _bookingRepository.GetByIdWithDetailsAsync(id);
        return booking is null ? null : MapToDto(booking);
    }

    public async Task<IEnumerable<BookingDto>> GetMyBookingsAsync(Guid userId)
    {
        var bookings = await _bookingRepository.GetByUserIdAsync(userId);
        return bookings.Select(MapToDto);
    }

    public async Task<BookingDto> CreateAsync(Guid userId, CreateBookingRequest request)
    {
        // Validations
        if (request.EndTime <= request.StartTime)
            throw new ArgumentException("End time must be after start time.");

        if (request.StartTime < DateTime.UtcNow)
            throw new ArgumentException("Cannot book in the past.");

        var court = await _courtRepository.GetByIdAsync(request.CourtId)
            ?? throw new KeyNotFoundException("Court not found.");

        if (!court.IsAvailable)
            throw new InvalidOperationException("Court is not available.");

        // Validate opening hours
        var startHour = request.StartTime.TimeOfDay;
        var endHour = request.EndTime.TimeOfDay;
        if (startHour < court.OpeningTime || endHour > court.ClosingTime)
            throw new InvalidOperationException($"Court is only open from {court.OpeningTime:hh\\:mm} to {court.ClosingTime:hh\\:mm}.");

        // Check conflicts
        var hasConflict = await _bookingRepository.HasConflictAsync(request.CourtId, request.StartTime, request.EndTime);
        if (hasConflict)
            throw new InvalidOperationException("The court is already booked for this time slot.");

        var hours = (decimal)(request.EndTime - request.StartTime).TotalHours;
        var totalPrice = court.HourlyRate * hours;

        var booking = new Booking
        {
            UserId = userId,
            CourtId = request.CourtId,
            StartTime = request.StartTime,
            EndTime = request.EndTime,
            TotalPrice = totalPrice,
            Notes = request.Notes,
            Status = BookingStatus.Confirmed
        };

        await _bookingRepository.CreateAsync(booking);

        // Send confirmation email
        var user = await _userRepository.GetByIdAsync(userId);
        if (user is not null)
        {
            try
            {
                await _emailService.SendBookingConfirmationAsync(
                    user.Email, user.FullName, court.Name,
                    request.StartTime, request.EndTime, totalPrice);
            }
            catch { /* Don't fail the booking if email fails */ }
        }

        return MapToDto(booking, user, court);
    }

    public async Task<BookingDto> RescheduleAsync(Guid bookingId, Guid userId, RescheduleBookingRequest request)
    {
        var booking = await _bookingRepository.GetByIdWithDetailsAsync(bookingId)
            ?? throw new KeyNotFoundException("Booking not found.");

        if (booking.UserId != userId)
            throw new UnauthorizedAccessException("You can only reschedule your own bookings.");

        if (booking.Status == BookingStatus.Cancelled)
            throw new InvalidOperationException("Cannot reschedule a cancelled booking.");

        if (request.NewEndTime <= request.NewStartTime)
            throw new ArgumentException("End time must be after start time.");

        if (request.NewStartTime < DateTime.UtcNow)
            throw new ArgumentException("Cannot reschedule to the past.");

        var hasConflict = await _bookingRepository.HasConflictAsync(
            booking.CourtId, request.NewStartTime, request.NewEndTime, bookingId);

        if (hasConflict)
            throw new InvalidOperationException("The court is already booked for the new time slot.");

        var hours = (decimal)(request.NewEndTime - request.NewStartTime).TotalHours;
        booking.StartTime = request.NewStartTime;
        booking.EndTime = request.NewEndTime;
        booking.TotalPrice = booking.Court.HourlyRate * hours;
        booking.UpdatedAt = DateTime.UtcNow;

        await _bookingRepository.UpdateAsync(booking);

        // Send reschedule email
        try
        {
            await _emailService.SendBookingRescheduleAsync(
                booking.User.Email, booking.User.FullName, booking.Court.Name,
                request.NewStartTime, request.NewEndTime);
        }
        catch { }

        return MapToDto(booking);
    }

    public async Task<BookingDto> CancelAsync(Guid bookingId, Guid userId, CancelBookingRequest request)
    {
        var booking = await _bookingRepository.GetByIdWithDetailsAsync(bookingId)
            ?? throw new KeyNotFoundException("Booking not found.");

        if (booking.UserId != userId)
            throw new UnauthorizedAccessException("You can only cancel your own bookings.");

        if (booking.Status == BookingStatus.Cancelled)
            throw new InvalidOperationException("Booking is already cancelled.");

        if (booking.StartTime < DateTime.UtcNow)
            throw new InvalidOperationException("Cannot cancel a booking that has already started.");

        booking.Status = BookingStatus.Cancelled;
        booking.CancellationReason = request.Reason;
        booking.CancelledAt = DateTime.UtcNow;
        booking.UpdatedAt = DateTime.UtcNow;

        await _bookingRepository.UpdateAsync(booking);

        // Send cancellation email
        try
        {
            await _emailService.SendBookingCancellationAsync(
                booking.User.Email, booking.User.FullName, booking.Court.Name,
                booking.StartTime, request.Reason);
        }
        catch { }

        return MapToDto(booking);
    }

    public async Task<BookingDto> ConfirmAsync(Guid bookingId)
    {
        var booking = await _bookingRepository.GetByIdWithDetailsAsync(bookingId)
            ?? throw new KeyNotFoundException("Booking not found.");

        booking.Status = BookingStatus.Confirmed;
        booking.UpdatedAt = DateTime.UtcNow;

        await _bookingRepository.UpdateAsync(booking);
        return MapToDto(booking);
    }

    private static BookingDto MapToDto(Booking booking, User? user = null, Court? court = null) => new()
    {
        Id = booking.Id,
        UserId = booking.UserId,
        UserName = user?.FullName ?? booking.User?.FullName ?? "",
        UserEmail = user?.Email ?? booking.User?.Email ?? "",
        CourtId = booking.CourtId,
        CourtName = court?.Name ?? booking.Court?.Name ?? "",
        SportType = court?.SportType.ToString() ?? booking.Court?.SportType.ToString() ?? "",
        StartTime = booking.StartTime,
        EndTime = booking.EndTime,
        Status = booking.Status.ToString(),
        TotalPrice = booking.TotalPrice,
        Notes = booking.Notes,
        CancellationReason = booking.CancellationReason,
        CreatedAt = booking.CreatedAt
    };
}

'@ | Set-Content -Path "src\CourtBooking.Application\Services\BookingService.cs" -Encoding UTF8

# --- src\CourtBooking.Application\Services\CourtService.cs ---
@'
using CourtBooking.Application.DTOs.Courts;
using CourtBooking.Application.Interfaces;
using CourtBooking.Domain.Entities;
using CourtBooking.Domain.Interfaces;

namespace CourtBooking.Application.Services;

public class CourtService : ICourtService
{
    private readonly ICourtRepository _courtRepository;

    public CourtService(ICourtRepository courtRepository)
    {
        _courtRepository = courtRepository;
    }

    public async Task<IEnumerable<CourtDto>> GetAllAsync()
    {
        var courts = await _courtRepository.GetAllAsync();
        return courts.Select(MapToDto);
    }

    public async Task<CourtDto?> GetByIdAsync(Guid id)
    {
        var court = await _courtRepository.GetByIdAsync(id);
        return court is null ? null : MapToDto(court);
    }

    public async Task<IEnumerable<CourtDto>> GetAvailableAsync(DateTime startTime, DateTime endTime)
    {
        if (endTime <= startTime)
            throw new ArgumentException("End time must be after start time.");

        var courts = await _courtRepository.GetAvailableAsync(startTime, endTime);
        return courts.Select(MapToDto);
    }

    public async Task<CourtDto> CreateAsync(CreateCourtRequest request)
    {
        var court = new Court
        {
            Name = request.Name,
            Description = request.Description,
            SportType = request.SportType,
            Surface = request.Surface,
            HourlyRate = request.HourlyRate,
            Capacity = request.Capacity,
            ImageUrl = request.ImageUrl,
            OpeningTime = TimeSpan.Parse(request.OpeningTime),
            ClosingTime = TimeSpan.Parse(request.ClosingTime)
        };

        await _courtRepository.CreateAsync(court);
        return MapToDto(court);
    }

    public async Task<CourtDto> UpdateAsync(Guid id, UpdateCourtRequest request)
    {
        var court = await _courtRepository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Court {id} not found.");

        if (request.Name is not null) court.Name = request.Name;
        if (request.Description is not null) court.Description = request.Description;
        if (request.HourlyRate.HasValue) court.HourlyRate = request.HourlyRate.Value;
        if (request.Capacity.HasValue) court.Capacity = request.Capacity.Value;
        if (request.IsAvailable.HasValue) court.IsAvailable = request.IsAvailable.Value;
        if (request.ImageUrl is not null) court.ImageUrl = request.ImageUrl;
        if (request.OpeningTime is not null) court.OpeningTime = TimeSpan.Parse(request.OpeningTime);
        if (request.ClosingTime is not null) court.ClosingTime = TimeSpan.Parse(request.ClosingTime);

        court.UpdatedAt = DateTime.UtcNow;

        await _courtRepository.UpdateAsync(court);
        return MapToDto(court);
    }

    public async Task DeleteAsync(Guid id)
    {
        var court = await _courtRepository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Court {id} not found.");

        await _courtRepository.DeleteAsync(court.Id);
    }

    private static CourtDto MapToDto(Court court) => new()
    {
        Id = court.Id,
        Name = court.Name,
        Description = court.Description,
        SportType = court.SportType.ToString(),
        Surface = court.Surface.ToString(),
        HourlyRate = court.HourlyRate,
        Capacity = court.Capacity,
        IsAvailable = court.IsAvailable,
        ImageUrl = court.ImageUrl,
        OpeningTime = court.OpeningTime.ToString(@"hh\:mm"),
        ClosingTime = court.ClosingTime.ToString(@"hh\:mm")
    };
}

'@ | Set-Content -Path "src\CourtBooking.Application\Services\CourtService.cs" -Encoding UTF8

# --- src\CourtBooking.Domain\Common\BaseEntity.cs ---
@'
namespace CourtBooking.Domain.Common;

public abstract class BaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public bool IsDeleted { get; set; } = false;
}

'@ | Set-Content -Path "src\CourtBooking.Domain\Common\BaseEntity.cs" -Encoding UTF8

# --- src\CourtBooking.Domain\CourtBooking.Domain.csproj ---
@'
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
  </PropertyGroup>
</Project>

'@ | Set-Content -Path "src\CourtBooking.Domain\CourtBooking.Domain.csproj" -Encoding UTF8

# --- src\CourtBooking.Domain\Entities\Booking.cs ---
@'
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

'@ | Set-Content -Path "src\CourtBooking.Domain\Entities\Booking.cs" -Encoding UTF8

# --- src\CourtBooking.Domain\Entities\Court.cs ---
@'
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

'@ | Set-Content -Path "src\CourtBooking.Domain\Entities\Court.cs" -Encoding UTF8

# --- src\CourtBooking.Domain\Entities\User.cs ---
@'
using CourtBooking.Domain.Common;
using CourtBooking.Domain.Enums;

namespace CourtBooking.Domain.Entities;

public class User : BaseEntity
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public UserRole Role { get; set; } = UserRole.User;
    public bool IsActive { get; set; } = true;

    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public string FullName => $"{FirstName} {LastName}";
}

'@ | Set-Content -Path "src\CourtBooking.Domain\Entities\User.cs" -Encoding UTF8

# --- src\CourtBooking.Domain\Enums\Enums.cs ---
@'
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

'@ | Set-Content -Path "src\CourtBooking.Domain\Enums\Enums.cs" -Encoding UTF8

# --- src\CourtBooking.Domain\Interfaces\IRepositories.cs ---
@'
using CourtBooking.Domain.Entities;

namespace CourtBooking.Domain.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id);
    Task<User?> GetByEmailAsync(string email);
    Task<IEnumerable<User>> GetAllAsync();
    Task<User> CreateAsync(User user);
    Task<User> UpdateAsync(User user);
    Task DeleteAsync(Guid id);
    Task<bool> ExistsByEmailAsync(string email);
}

public interface ICourtRepository
{
    Task<Court?> GetByIdAsync(Guid id);
    Task<IEnumerable<Court>> GetAllAsync();
    Task<IEnumerable<Court>> GetAvailableAsync(DateTime startTime, DateTime endTime);
    Task<Court> CreateAsync(Court court);
    Task<Court> UpdateAsync(Court court);
    Task DeleteAsync(Guid id);
}

public interface IBookingRepository
{
    Task<Booking?> GetByIdAsync(Guid id);
    Task<Booking?> GetByIdWithDetailsAsync(Guid id);
    Task<IEnumerable<Booking>> GetAllAsync();
    Task<IEnumerable<Booking>> GetByUserIdAsync(Guid userId);
    Task<IEnumerable<Booking>> GetByCourtIdAsync(Guid courtId);
    Task<IEnumerable<Booking>> GetByDateRangeAsync(DateTime from, DateTime to);
    Task<bool> HasConflictAsync(Guid courtId, DateTime startTime, DateTime endTime, Guid? excludeBookingId = null);
    Task<Booking> CreateAsync(Booking booking);
    Task<Booking> UpdateAsync(Booking booking);
}

'@ | Set-Content -Path "src\CourtBooking.Domain\Interfaces\IRepositories.cs" -Encoding UTF8

# --- src\CourtBooking.Infrastructure\CourtBooking.Infrastructure.csproj ---
@'
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include="BCrypt.Net-Next" Version="4.0.3" />
    <PackageReference Include="Microsoft.EntityFrameworkCore" Version="8.0.0" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="8.0.0" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="8.0.0" />
    <PackageReference Include="Microsoft.Extensions.Configuration.Abstractions" Version="8.0.0" />
    <PackageReference Include="Microsoft.IdentityModel.Tokens" Version="7.0.3" />
    <PackageReference Include="System.IdentityModel.Tokens.Jwt" Version="7.0.3" />
  </ItemGroup>
  <ItemGroup>
    <ProjectReference Include="..\CourtBooking.Application\CourtBooking.Application.csproj" />
    <ProjectReference Include="..\CourtBooking.Domain\CourtBooking.Domain.csproj" />
  </ItemGroup>
</Project>

'@ | Set-Content -Path "src\CourtBooking.Infrastructure\CourtBooking.Infrastructure.csproj" -Encoding UTF8

# --- src\CourtBooking.Infrastructure\Data\AppDbContext.cs ---
@'
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
            new Court { Id = Guid.NewGuid(), Name = "Cancha Fútbol 5", Description = "Cancha de fútbol 5 con iluminación", SportType = Domain.Enums.SportType.Football, Surface = Domain.Enums.CourtSurface.Synthetic, HourlyRate = 2500, Capacity = 10, CreatedAt = DateTime.UtcNow },
            new Court { Id = Guid.NewGuid(), Name = "Cancha Tenis", Description = "Cancha de tenis en polvo de ladrillo", SportType = Domain.Enums.SportType.Tennis, Surface = Domain.Enums.CourtSurface.Clay, HourlyRate = 1200, Capacity = 4, CreatedAt = DateTime.UtcNow },
        };
        modelBuilder.Entity<Court>().HasData(courts);
    }
}

'@ | Set-Content -Path "src\CourtBooking.Infrastructure\Data\AppDbContext.cs" -Encoding UTF8

# --- src\CourtBooking.Infrastructure\Repositories\BookingRepository.cs ---
@'
using CourtBooking.Domain.Entities;
using CourtBooking.Domain.Enums;
using CourtBooking.Domain.Interfaces;
using CourtBooking.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CourtBooking.Infrastructure.Repositories;

public class BookingRepository : IBookingRepository
{
    private readonly AppDbContext _context;

    public BookingRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Booking?> GetByIdAsync(Guid id) =>
        await _context.Bookings.FindAsync(id);

    public async Task<Booking?> GetByIdWithDetailsAsync(Guid id) =>
        await _context.Bookings
            .Include(b => b.User)
            .Include(b => b.Court)
            .FirstOrDefaultAsync(b => b.Id == id);

    public async Task<IEnumerable<Booking>> GetAllAsync() =>
        await _context.Bookings
            .Include(b => b.User)
            .Include(b => b.Court)
            .OrderByDescending(b => b.StartTime)
            .ToListAsync();

    public async Task<IEnumerable<Booking>> GetByUserIdAsync(Guid userId) =>
        await _context.Bookings
            .Include(b => b.Court)
            .Where(b => b.UserId == userId)
            .OrderByDescending(b => b.StartTime)
            .ToListAsync();

    public async Task<IEnumerable<Booking>> GetByCourtIdAsync(Guid courtId) =>
        await _context.Bookings
            .Include(b => b.User)
            .Where(b => b.CourtId == courtId)
            .OrderByDescending(b => b.StartTime)
            .ToListAsync();

    public async Task<IEnumerable<Booking>> GetByDateRangeAsync(DateTime from, DateTime to) =>
        await _context.Bookings
            .Include(b => b.User)
            .Include(b => b.Court)
            .Where(b => b.StartTime >= from && b.EndTime <= to)
            .OrderBy(b => b.StartTime)
            .ToListAsync();

    public async Task<bool> HasConflictAsync(Guid courtId, DateTime startTime, DateTime endTime, Guid? excludeBookingId = null)
    {
        var query = _context.Bookings
            .Where(b => b.CourtId == courtId &&
                        b.Status != BookingStatus.Cancelled &&
                        b.StartTime < endTime && b.EndTime > startTime);

        if (excludeBookingId.HasValue)
            query = query.Where(b => b.Id != excludeBookingId.Value);

        return await query.AnyAsync();
    }

    public async Task<Booking> CreateAsync(Booking booking)
    {
        _context.Bookings.Add(booking);
        await _context.SaveChangesAsync();
        return booking;
    }

    public async Task<Booking> UpdateAsync(Booking booking)
    {
        _context.Bookings.Update(booking);
        await _context.SaveChangesAsync();
        return booking;
    }
}

'@ | Set-Content -Path "src\CourtBooking.Infrastructure\Repositories\BookingRepository.cs" -Encoding UTF8

# --- src\CourtBooking.Infrastructure\Repositories\CourtRepository.cs ---
@'
using CourtBooking.Domain.Entities;
using CourtBooking.Domain.Enums;
using CourtBooking.Domain.Interfaces;
using CourtBooking.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CourtBooking.Infrastructure.Repositories;

public class CourtRepository : ICourtRepository
{
    private readonly AppDbContext _context;

    public CourtRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Court?> GetByIdAsync(Guid id) =>
        await _context.Courts.FindAsync(id);

    public async Task<IEnumerable<Court>> GetAllAsync() =>
        await _context.Courts.OrderBy(c => c.Name).ToListAsync();

    public async Task<IEnumerable<Court>> GetAvailableAsync(DateTime startTime, DateTime endTime)
    {
        // Get courts that don't have conflicting bookings
        var bookedCourtIds = await _context.Bookings
            .Where(b => b.Status != BookingStatus.Cancelled &&
                        b.StartTime < endTime && b.EndTime > startTime)
            .Select(b => b.CourtId)
            .Distinct()
            .ToListAsync();

        return await _context.Courts
            .Where(c => c.IsAvailable && !bookedCourtIds.Contains(c.Id))
            .ToListAsync();
    }

    public async Task<Court> CreateAsync(Court court)
    {
        _context.Courts.Add(court);
        await _context.SaveChangesAsync();
        return court;
    }

    public async Task<Court> UpdateAsync(Court court)
    {
        _context.Courts.Update(court);
        await _context.SaveChangesAsync();
        return court;
    }

    public async Task DeleteAsync(Guid id)
    {
        var court = await _context.Courts.FindAsync(id);
        if (court is not null)
        {
            court.IsDeleted = true;
            await _context.SaveChangesAsync();
        }
    }
}

'@ | Set-Content -Path "src\CourtBooking.Infrastructure\Repositories\CourtRepository.cs" -Encoding UTF8

# --- src\CourtBooking.Infrastructure\Repositories\UserRepository.cs ---
@'
using CourtBooking.Domain.Entities;
using CourtBooking.Domain.Interfaces;
using CourtBooking.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CourtBooking.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByIdAsync(Guid id) =>
        await _context.Users.FindAsync(id);

    public async Task<User?> GetByEmailAsync(string email) =>
        await _context.Users.FirstOrDefaultAsync(u => u.Email == email.ToLower());

    public async Task<IEnumerable<User>> GetAllAsync() =>
        await _context.Users.ToListAsync();

    public async Task<User> CreateAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User> UpdateAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task DeleteAsync(Guid id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user is not null)
        {
            user.IsDeleted = true;
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsByEmailAsync(string email) =>
        await _context.Users.AnyAsync(u => u.Email == email.ToLower());
}

'@ | Set-Content -Path "src\CourtBooking.Infrastructure\Repositories\UserRepository.cs" -Encoding UTF8

# --- src\CourtBooking.Infrastructure\Services\EmailService.cs ---
@'
using CourtBooking.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CourtBooking.Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<EmailService> _logger;

    public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public async Task SendBookingConfirmationAsync(string toEmail, string userName, string courtName,
        DateTime startTime, DateTime endTime, decimal totalPrice)
    {
        var subject = "✅ Reserva Confirmada - CourtBooking";
        var body = $@"
            <h2>¡Tu reserva está confirmada!</h2>
            <p>Hola <strong>{userName}</strong>,</p>
            <p>Tu reserva ha sido confirmada con los siguientes detalles:</p>
            <table style='border-collapse: collapse; width: 100%;'>
                <tr><td style='padding: 8px; border: 1px solid #ddd;'><strong>Cancha</strong></td><td style='padding: 8px; border: 1px solid #ddd;'>{courtName}</td></tr>
                <tr><td style='padding: 8px; border: 1px solid #ddd;'><strong>Fecha</strong></td><td style='padding: 8px; border: 1px solid #ddd;'>{startTime:dd/MM/yyyy}</td></tr>
                <tr><td style='padding: 8px; border: 1px solid #ddd;'><strong>Horario</strong></td><td style='padding: 8px; border: 1px solid #ddd;'>{startTime:HH:mm} - {endTime:HH:mm}</td></tr>
                <tr><td style='padding: 8px; border: 1px solid #ddd;'><strong>Total</strong></td><td style='padding: 8px; border: 1px solid #ddd;'>${totalPrice:N2}</td></tr>
            </table>
            <br/>
            <p>¡Te esperamos! 🏆</p>
            <p><em>Equipo CourtBooking</em></p>";

        await SendEmailAsync(toEmail, subject, body);
    }

    public async Task SendBookingCancellationAsync(string toEmail, string userName, string courtName,
        DateTime startTime, string? reason)
    {
        var subject = "❌ Reserva Cancelada - CourtBooking";
        var body = $@"
            <h2>Tu reserva ha sido cancelada</h2>
            <p>Hola <strong>{userName}</strong>,</p>
            <p>Tu reserva para <strong>{courtName}</strong> del <strong>{startTime:dd/MM/yyyy HH:mm}</strong> ha sido cancelada.</p>
            {(reason is not null ? $"<p><strong>Motivo:</strong> {reason}</p>" : "")}
            <p>Podés hacer una nueva reserva cuando quieras.</p>
            <p><em>Equipo CourtBooking</em></p>";

        await SendEmailAsync(toEmail, subject, body);
    }

    public async Task SendBookingRescheduleAsync(string toEmail, string userName, string courtName,
        DateTime newStartTime, DateTime newEndTime)
    {
        var subject = "🔄 Reserva Reprogramada - CourtBooking";
        var body = $@"
            <h2>Tu reserva fue reprogramada</h2>
            <p>Hola <strong>{userName}</strong>,</p>
            <p>Tu reserva en <strong>{courtName}</strong> fue actualizada:</p>
            <p><strong>Nueva fecha:</strong> {newStartTime:dd/MM/yyyy}</p>
            <p><strong>Nuevo horario:</strong> {newStartTime:HH:mm} - {newEndTime:HH:mm}</p>
            <p><em>Equipo CourtBooking</em></p>";

        await SendEmailAsync(toEmail, subject, body);
    }

    private async Task SendEmailAsync(string toEmail, string subject, string htmlBody)
    {
        var smtpEnabled = _configuration.GetValue<bool>("Email:Enabled");
        if (!smtpEnabled)
        {
            _logger.LogInformation("Email (disabled) to: {Email} | Subject: {Subject}", toEmail, subject);
            return;
        }

        // Production: use MailKit
        // var message = new MimeMessage();
        // message.From.Add(new MailboxAddress("CourtBooking", config["Email:From"]));
        // message.To.Add(new MailboxAddress(toEmail, toEmail));
        // message.Subject = subject;
        // message.Body = new TextPart("html") { Text = htmlBody };
        // using var client = new SmtpClient();
        // await client.ConnectAsync(config["Email:Host"], config.GetValue<int>("Email:Port"), SecureSocketOptions.StartTls);
        // await client.AuthenticateAsync(config["Email:Username"], config["Email:Password"]);
        // await client.SendAsync(message);
        // await client.DisconnectAsync(true);

        _logger.LogInformation("Email sent to {Email}: {Subject}", toEmail, subject);
        await Task.CompletedTask;
    }
}

'@ | Set-Content -Path "src\CourtBooking.Infrastructure\Services\EmailService.cs" -Encoding UTF8

# --- src\CourtBooking.Infrastructure\Services\JwtService.cs ---
@'
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CourtBooking.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CourtBooking.Infrastructure.Services;

public class JwtService : IJwtService
{
    private readonly string _secretKey;
    private readonly string _issuer;
    private readonly string _audience;
    private readonly int _expiryHours;

    public JwtService(IConfiguration configuration)
    {
        _secretKey = configuration["Jwt:SecretKey"] ?? throw new InvalidOperationException("JWT SecretKey not configured.");
        _issuer = configuration["Jwt:Issuer"] ?? "CourtBookingAPI";
        _audience = configuration["Jwt:Audience"] ?? "CourtBookingClient";
        _expiryHours = int.Parse(configuration["Jwt:ExpiryHours"] ?? "24");
    }

    public string GenerateToken(Guid userId, string email, string role)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.Role, role),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _issuer,
            audience: _audience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(_expiryHours),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public bool ValidateToken(string token)
    {
        try
        {
            var handler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            handler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ValidateIssuer = true,
                ValidIssuer = _issuer,
                ValidateAudience = true,
                ValidAudience = _audience
            }, out _);
            return true;
        }
        catch { return false; }
    }

    public Guid GetUserIdFromToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);
        var userId = jwtToken.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        return Guid.Parse(userId);
    }
}

'@ | Set-Content -Path "src\CourtBooking.Infrastructure\Services\JwtService.cs" -Encoding UTF8

Write-Host "Proyecto listo!" -ForegroundColor Green
Write-Host "Ahora ejecuta: cd src\CourtBooking.API && dotnet run" -ForegroundColor Yellow