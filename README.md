# CourtBooking API

API REST para gestion de reservas de canchas deportivas (Padel, Futbol, Tenis, etc.) construida con **.NET 8** y **Clean Architecture**.

## Tech Stack

| Capa | Tecnologia |
|------|-----------|
| Framework | ASP.NET Core 8 |
| ORM | Entity Framework Core 8 |
| Base de datos | SQL Server |
| Autenticacion | JWT Bearer Tokens |
| Password Hashing | BCrypt |
| Documentacion | Swagger / OpenAPI |
| Arquitectura | Clean Architecture |

## Arquitectura

```
CourtBooking/
├── src/
│   ├── CourtBooking.Domain/          # Entidades, Enums, Interfaces de Repos
│   │   ├── Entities/                 # User, Court, Booking
│   │   ├── Enums/                    # BookingStatus, SportType, etc.
│   │   └── Interfaces/               # IUserRepository, ICourtRepository...
│   │
│   ├── CourtBooking.Application/     # Logica de negocio, DTOs, Interfaces de Servicios
│   │   ├── DTOs/                     # Auth, Courts, Bookings
│   │   ├── Interfaces/               # IAuthService, IBookingService...
│   │   └── Services/                 # AuthService, BookingService, CourtService
│   │
│   ├── CourtBooking.Infrastructure/  # Implementaciones de repos y servicios externos
│   │   ├── Data/                     # AppDbContext + Seed Data
│   │   ├── Repositories/             # UserRepo, CourtRepo, BookingRepo
│   │   └── Services/                 # JwtService, EmailService
│   │
│   └── CourtBooking.API/             # Controllers, Middleware, Program.cs
│       ├── Controllers/              # Auth, Courts, Bookings, Admin
│       └── Middleware/               # ExceptionMiddleware
```

## Como correr el proyecto

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

### 3. Correr la API

```bash
cd src/CourtBooking.API
dotnet run
```

> La API aplica las migraciones automaticamente al arrancar y crea la base de datos.

### 4. Acceder a Swagger

```
http://localhost:5000
```

---

## Autenticacion

La API usa **JWT Bearer Tokens**. Para endpoints protegidos:

1. Login en `POST /api/auth/login`
2. Copiar el `token` de la respuesta
3. En Swagger: click en **Authorize** → `Bearer {tu_token}`

### Usuario Admin (seed)
```
Email: admin@courtbooking.com
Password: Admin123!
```

---

## Endpoints

### Auth
| Metodo | Ruta | Descripcion | Auth |
|--------|------|-------------|------|
| POST | `/api/auth/register` | Registrar usuario | Publico |
| POST | `/api/auth/login` | Login | Publico |

### Canchas
| Metodo | Ruta | Descripcion | Auth |
|--------|------|-------------|------|
| GET | `/api/courts` | Listar todas las canchas | Publico |
| GET | `/api/courts/{id}` | Obtener cancha por ID | Publico |
| GET | `/api/courts/available?startTime=&endTime=` | Canchas disponibles | Publico |
| POST | `/api/courts` | Crear cancha | Admin |
| PUT | `/api/courts/{id}` | Actualizar cancha | Admin |
| DELETE | `/api/courts/{id}` | Eliminar cancha | Admin |

### Reservas
| Metodo | Ruta | Descripcion | Auth |
|--------|------|-------------|------|
| GET | `/api/bookings` | Listar todas las reservas | Admin |
| GET | `/api/bookings/my` | Mis reservas | User |
| GET | `/api/bookings/{id}` | Detalle de reserva | User |
| POST | `/api/bookings` | Crear reserva | User |
| PUT | `/api/bookings/{id}/reschedule` | Reprogramar reserva | User |
| PUT | `/api/bookings/{id}/cancel` | Cancelar reserva | User |
| PUT | `/api/bookings/{id}/confirm` | Confirmar reserva | Admin |

### Admin Dashboard
| Metodo | Ruta | Descripcion | Auth |
|--------|------|-------------|------|
| GET | `/api/admin/dashboard` | Estadisticas generales | Admin |
| GET | `/api/admin/users` | Listar usuarios | Admin |
| PUT | `/api/admin/users/{id}/toggle-status` | Activar/desactivar usuario | Admin |

---

## Features

- **Clean Architecture** — Domain, Application, Infrastructure, API
- **JWT Auth** con roles (Admin / User)
- **Validacion de disponibilidad** — detecta conflictos de horario en tiempo real
- **Reservar, reprogramar y cancelar** turnos
- **Panel Admin** con stats (ingresos, reservas del dia, usuarios)
- **Notificaciones por email** configurables via SMTP
- **Soft delete** en entidades
- **Global Exception Middleware** — manejo de errores centralizado
- **Swagger** con soporte JWT integrado
- **Seed data** con admin y 3 canchas de ejemplo

---

## Configurar emails (opcional)

En `appsettings.json` cambiar `Enabled` a `true` y completar los datos SMTP:

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

Para Gmail: activar 2FA y generar una **App Password** en la configuracion de cuenta de Google.

---

## Autor

**Vladimir Bulan** — [GitHub](https://github.com/Vladimir-Bulan) | [LinkedIn](https://www.linkedin.com/in/vladimir-bulan-60083b21b/)