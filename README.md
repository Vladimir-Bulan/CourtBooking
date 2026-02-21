# ðŸŸï¸ CourtBooking API

API REST para gestiÃ³n de reservas de canchas deportivas (Padel, FÃºtbol, Tenis, etc.) construida con **.NET 8** y **Clean Architecture**.

## ðŸ› ï¸ Tech Stack

| Capa | TecnologÃ­a |
|------|-----------|
| Framework | ASP.NET Core 8 |
| ORM | Entity Framework Core 8 |
| Base de datos | SQL Server |
| AutenticaciÃ³n | JWT Bearer Tokens |
| Password Hashing | BCrypt |
| DocumentaciÃ³n | Swagger / OpenAPI |
| Arquitectura | Clean Architecture |

## ðŸ“ Arquitectura

```
CourtBooking/
â”œâ”€â”€ src/
â”‚   â”œâ”€â”€ CourtBooking.Domain/          # Entidades, Enums, Interfaces de Repos
â”‚   â”‚   â”œâ”€â”€ Entities/                 # User, Court, Booking
â”‚   â”‚   â”œâ”€â”€ Enums/                    # BookingStatus, SportType, etc.
â”‚   â”‚   â””â”€â”€ Interfaces/               # IUserRepository, ICourtRepository...
â”‚   â”‚
â”‚   â”œâ”€â”€ CourtBooking.Application/     # LÃ³gica de negocio, DTOs, Interfaces de Servicios
â”‚   â”‚   â”œâ”€â”€ DTOs/                     # Auth, Courts, Bookings
â”‚   â”‚   â”œâ”€â”€ Interfaces/               # IAuthService, IBookingService...
â”‚   â”‚   â””â”€â”€ Services/                 # AuthService, BookingService, CourtService
â”‚   â”‚
â”‚   â”œâ”€â”€ CourtBooking.Infrastructure/  # Implementaciones de repositorios y servicios externos
â”‚   â”‚   â”œâ”€â”€ Data/                     # AppDbContext + Seed Data
â”‚   â”‚   â”œâ”€â”€ Repositories/             # UserRepo, CourtRepo, BookingRepo
â”‚   â”‚   â””â”€â”€ Services/                 # JwtService, EmailService
â”‚   â”‚
â”‚   â””â”€â”€ CourtBooking.API/             # Controllers, Middleware, Program.cs
â”‚       â”œâ”€â”€ Controllers/              # Auth, Courts, Bookings, Admin
â”‚       â””â”€â”€ Middleware/               # ExceptionMiddleware
```

## ðŸš€ CÃ³mo correr el proyecto

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

> âš¡ O simplemente corrÃ© la API â€” aplica las migraciones automÃ¡ticamente al arrancar.

### 4. Correr la API

```bash
dotnet run --project src/CourtBooking.API
```

### 5. Acceder a Swagger

```
http://localhost:5000
```

---

## ðŸ” AutenticaciÃ³n

La API usa **JWT Bearer Tokens**. Para endpoints protegidos:

1. Registrarse o hacer login en `/api/auth/register` o `/api/auth/login`
2. Copiar el token de la respuesta
3. En Swagger: click en **Authorize** â†’ `Bearer {tu_token}`

### Usuario Admin (seed)
```
Email: admin@courtbooking.com
Password: Admin123!
```

---

## ðŸ“‹ Endpoints

### Auth
| MÃ©todo | Ruta | DescripciÃ³n | Auth |
|--------|------|-------------|------|
| POST | `/api/auth/register` | Registrar usuario | âŒ |
| POST | `/api/auth/login` | Login | âŒ |

### Canchas
| MÃ©todo | Ruta | DescripciÃ³n | Auth |
|--------|------|-------------|------|
| GET | `/api/courts` | Listar todas las canchas | âŒ |
| GET | `/api/courts/{id}` | Obtener cancha por ID | âŒ |
| GET | `/api/courts/available?startTime=&endTime=` | Canchas disponibles | âŒ |
| POST | `/api/courts` | Crear cancha | ðŸ” Admin |
| PUT | `/api/courts/{id}` | Actualizar cancha | ðŸ” Admin |
| DELETE | `/api/courts/{id}` | Eliminar cancha | ðŸ” Admin |

### Reservas
| MÃ©todo | Ruta | DescripciÃ³n | Auth |
|--------|------|-------------|------|
| GET | `/api/bookings` | Listar todas las reservas | ðŸ” Admin |
| GET | `/api/bookings/my` | Mis reservas | ðŸ” User |
| GET | `/api/bookings/{id}` | Detalle de reserva | ðŸ” User |
| POST | `/api/bookings` | Crear reserva | ðŸ” User |
| PUT | `/api/bookings/{id}/reschedule` | Reprogramar reserva | ðŸ” User |
| PUT | `/api/bookings/{id}/cancel` | Cancelar reserva | ðŸ” User |
| PUT | `/api/bookings/{id}/confirm` | Confirmar reserva | ðŸ” Admin |

### Admin Dashboard
| MÃ©todo | Ruta | DescripciÃ³n | Auth |
|--------|------|-------------|------|
| GET | `/api/admin/dashboard` | EstadÃ­sticas generales | ðŸ” Admin |
| GET | `/api/admin/users` | Listar usuarios | ðŸ” Admin |
| PUT | `/api/admin/users/{id}/toggle-status` | Activar/desactivar usuario | ðŸ” Admin |

---

## ðŸ’¡ Features principales

- âœ… **Clean Architecture** â€” Domain, Application, Infrastructure, API
- âœ… **JWT Auth** con roles (Admin / User)
- âœ… **ValidaciÃ³n de disponibilidad** â€” detecta conflictos de horario en tiempo real
- âœ… **Reservar, reprogramar y cancelar** turnos
- âœ… **Panel Admin** con stats (ingresos, reservas del dÃ­a, usuarios)
- âœ… **Notificaciones por email** (configurable via SMTP)
- âœ… **Soft delete** en entidades
- âœ… **Global Exception Middleware** â€” manejo de errores centralizado
- âœ… **Swagger** con soporte JWT integrado
- âœ… **Seed data** con admin y 3 canchas de ejemplo

---

## ðŸ“§ Configurar emails (opcional)

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

## ðŸ‘¤ Autor

**Vladimir Bulan** â€” [GitHub](https://github.com/Vladimir-Bulan) | [LinkedIn](https://www.linkedin.com/in/vladimir-bulan-60083b21b/)

