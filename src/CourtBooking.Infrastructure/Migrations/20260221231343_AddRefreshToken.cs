using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CourtBooking.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRefreshToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Courts",
                keyColumn: "Id",
                keyValue: new Guid("0d311e3a-44c3-4b1e-9743-93fd3707b3cc"));

            migrationBuilder.DeleteData(
                table: "Courts",
                keyColumn: "Id",
                keyValue: new Guid("1d2900e3-d78e-4fcc-881d-51f68fc27e16"));

            migrationBuilder.DeleteData(
                table: "Courts",
                keyColumn: "Id",
                keyValue: new Guid("aad6272f-8bd0-417f-b55f-f84b425967f0"));

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiresAt",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Courts",
                columns: new[] { "Id", "Capacity", "ClosingTime", "CreatedAt", "Description", "HourlyRate", "ImageUrl", "IsAvailable", "IsDeleted", "Name", "OpeningTime", "SportType", "Surface", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("1f3f949b-2506-4a5f-ab24-7f44947ac0b5"), 4, new TimeSpan(0, 22, 0, 0, 0), new DateTime(2026, 2, 21, 23, 13, 43, 634, DateTimeKind.Utc).AddTicks(7881), "Cancha de tenis en polvo de ladrillo", 1200m, "", true, false, "Cancha Tenis", new TimeSpan(0, 8, 0, 0, 0), 3, 2, null },
                    { new Guid("5f8893a6-aa08-4da6-b348-18278475f3fb"), 10, new TimeSpan(0, 22, 0, 0, 0), new DateTime(2026, 2, 21, 23, 13, 43, 634, DateTimeKind.Utc).AddTicks(7878), "Cancha de fÃºtbol 5 con iluminaciÃ³n", 2500m, "", true, false, "Cancha FÃºtbol 5", new TimeSpan(0, 8, 0, 0, 0), 1, 4, null },
                    { new Guid("ffeb45e7-aca6-4e06-b926-8843f64c3a62"), 4, new TimeSpan(0, 22, 0, 0, 0), new DateTime(2026, 2, 21, 23, 13, 43, 634, DateTimeKind.Utc).AddTicks(7868), "Cancha de padel cubierta", 1500m, "", true, false, "Cancha Padel 1", new TimeSpan(0, 8, 0, 0, 0), 2, 4, null }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "CreatedAt", "PasswordHash", "RefreshToken", "RefreshTokenExpiresAt" },
                values: new object[] { new DateTime(2026, 2, 21, 23, 13, 43, 634, DateTimeKind.Utc).AddTicks(7487), "$2a$11$Pj7riUM/Ob1LpXntY2STRelalBGR1YTWGZMaQfwZv8UpFSasz3tfG", null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Courts",
                keyColumn: "Id",
                keyValue: new Guid("1f3f949b-2506-4a5f-ab24-7f44947ac0b5"));

            migrationBuilder.DeleteData(
                table: "Courts",
                keyColumn: "Id",
                keyValue: new Guid("5f8893a6-aa08-4da6-b348-18278475f3fb"));

            migrationBuilder.DeleteData(
                table: "Courts",
                keyColumn: "Id",
                keyValue: new Guid("ffeb45e7-aca6-4e06-b926-8843f64c3a62"));

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiresAt",
                table: "Users");

            migrationBuilder.InsertData(
                table: "Courts",
                columns: new[] { "Id", "Capacity", "ClosingTime", "CreatedAt", "Description", "HourlyRate", "ImageUrl", "IsAvailable", "IsDeleted", "Name", "OpeningTime", "SportType", "Surface", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("0d311e3a-44c3-4b1e-9743-93fd3707b3cc"), 4, new TimeSpan(0, 22, 0, 0, 0), new DateTime(2026, 2, 21, 21, 59, 17, 292, DateTimeKind.Utc).AddTicks(4150), "Cancha de tenis en polvo de ladrillo", 1200m, "", true, false, "Cancha Tenis", new TimeSpan(0, 8, 0, 0, 0), 3, 2, null },
                    { new Guid("1d2900e3-d78e-4fcc-881d-51f68fc27e16"), 4, new TimeSpan(0, 22, 0, 0, 0), new DateTime(2026, 2, 21, 21, 59, 17, 292, DateTimeKind.Utc).AddTicks(4133), "Cancha de padel cubierta", 1500m, "", true, false, "Cancha Padel 1", new TimeSpan(0, 8, 0, 0, 0), 2, 4, null },
                    { new Guid("aad6272f-8bd0-417f-b55f-f84b425967f0"), 10, new TimeSpan(0, 22, 0, 0, 0), new DateTime(2026, 2, 21, 21, 59, 17, 292, DateTimeKind.Utc).AddTicks(4136), "Cancha de fÃºtbol 5 con iluminaciÃ³n", 2500m, "", true, false, "Cancha FÃºtbol 5", new TimeSpan(0, 8, 0, 0, 0), 1, 4, null }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 2, 21, 21, 59, 17, 292, DateTimeKind.Utc).AddTicks(3630), "$2a$11$je3donAc0hXzAa7r525aneRMHkhsYwKkNqhHxWp9FEOoFDUQkA6ua" });
        }
    }
}
