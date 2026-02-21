# CourtBooking API 🎾⚽🏸

REST API para gestión de reservas de canchas deportivas, desarrollada con **.NET 8** y **Clean Architecture**.

## 🚀 Tech Stack

- **Framework:** ASP.NET Core 8
- **ORM:** Entity Framework Core 8 + SQL Server
- **Autenticación:** JWT Bearer + Refresh Token
- **Arquitectura:** Clean Architecture + CQRS con MediatR
- **Testing:** xUnit + Moq + FluentAssertions (25 tests)
- **Documentación:** Swagger / OpenAPI

## 📁 Estructura del Proyecto

```
CourtBooking/
├── src/
│   ├── CourtBooking.Domain/          # Entidades, enums, interfaces
│   ├── CourtBooking.Application/     # Servicios, DTOs, CQRS (Commands/Queries)
│   ├── CourtBooking.Infrastructure/  # EF Core, repositorios, JWT, Email
│   └── CourtBooking.API/             # Controllers, Middleware, Program.cs
└── tests/
    └── CourtBooking.Tests/           # Unit tests (xUnit + Moq)
```

## ✨ Features

- ✅ **Autenticación JWT** con Access Token + Refresh Token
- ✅ **CQRS con MediatR** — Commands y Queries separados
- ✅ **Paginación** en todos los listados (`page`, `pageSize`)
- ✅ **Filtros avanzados** por deporte, superficie, precio, disponibilidad
- ✅ **Roles** — Admin y User con permisos diferenciados
- ✅ **Gestión de reservas** — crear, cancelar, reprogramar, confirmar
- ✅ **Soft delete** en todas las entidades
- ✅ **Global Exception Middleware**
- ✅ **25 unit tests** cubriendo BookingService, AuthService y CourtService

## 🏃 Cómo correr el proyecto

### Requisitos
- .NET 8 SDK
- SQL Server

### Setup

```bash
# Clonar
git clone https://github.com/Vladimir-Bulan/CourtBooking.git
cd CourtBooking

# Configurar conexión en src/CourtBooking.API/appsettings.json
# "DefaultConnection": "Server=...;Database=CourtBookingDb;..."

# Aplicar migraciones
dotnet ef database update --project src/CourtBooking.Infrastructure --startup-project src/CourtBooking.API

# Correr
dotnet run --project src/CourtBooking.API
```

Swagger disponible en: `http://localhost:5000`

### Tests

```bash
dotnet test
```

## 📋 Endpoints principales

### Auth
| Método | Endpoint | Descripción |
|--------|----------|-------------|
| POST | `/api/auth/register` | Registro de usuario |
| POST | `/api/auth/login` | Login → JWT + Refresh Token |
| POST | `/api/auth/refresh` | Renovar token |
| POST | `/api/auth/revoke` | Logout / invalidar token |

### Courts
| Método | Endpoint | Descripción |
|--------|----------|-------------|
| GET | `/api/courts?page=1&pageSize=10&sportType=Padel` | Listar con filtros |
| GET | `/api/courts/{id}` | Detalle de cancha |
| GET | `/api/courts/available?startTime=...&endTime=...` | Canchas disponibles |
| POST | `/api/courts` | Crear cancha (Admin) |
| PUT | `/api/courts/{id}` | Actualizar cancha (Admin) |
| DELETE | `/api/courts/{id}` | Eliminar cancha (Admin) |

### Bookings
| Método | Endpoint | Descripción |
|--------|----------|-------------|
| GET | `/api/bookings?page=1&pageSize=10` | Todas las reservas (Admin) |
| GET | `/api/bookings/my` | Mis reservas |
| GET | `/api/bookings/{id}` | Detalle de reserva |
| POST | `/api/bookings` | Crear reserva |
| PUT | `/api/bookings/{id}/reschedule` | Reprogramar |
| PUT | `/api/bookings/{id}/cancel` | Cancelar |
| PUT | `/api/bookings/{id}/confirm` | Confirmar (Admin) |

## 🏗️ Patrones y decisiones de diseño

- **Clean Architecture** — separación estricta de capas, dependencias apuntan hacia adentro
- **CQRS con MediatR** — Commands (escritura) y Queries (lectura) separados, handlers desacoplados
- **Repository Pattern** — abstracción del acceso a datos
- **Refresh Token Rotation** — cada refresh genera un nuevo token, el anterior se invalida
- **Soft Delete** — los registros nunca se borran físicamente

## 👤 Autor

**Vladimir Bulan** — [GitHub](https://github.com/Vladimir-Bulan)