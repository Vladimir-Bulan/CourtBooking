using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CourtBooking.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Courts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SportType = table.Column<int>(type: "int", nullable: false),
                    Surface = table.Column<int>(type: "int", nullable: false),
                    HourlyRate = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OpeningTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    ClosingTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourtId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CancellationReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CancelledAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_Courts_CourtId",
                        column: x => x.CourtId,
                        principalTable: "Courts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bookings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Courts",
                columns: new[] { "Id", "Capacity", "ClosingTime", "CreatedAt", "Description", "HourlyRate", "ImageUrl", "IsAvailable", "IsDeleted", "Name", "OpeningTime", "SportType", "Surface", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("0d311e3a-44c3-4b1e-9743-93fd3707b3cc"), 4, new TimeSpan(0, 22, 0, 0, 0), new DateTime(2026, 2, 21, 21, 59, 17, 292, DateTimeKind.Utc).AddTicks(4150), "Cancha de tenis en polvo de ladrillo", 1200m, "", true, false, "Cancha Tenis", new TimeSpan(0, 8, 0, 0, 0), 3, 2, null },
                    { new Guid("1d2900e3-d78e-4fcc-881d-51f68fc27e16"), 4, new TimeSpan(0, 22, 0, 0, 0), new DateTime(2026, 2, 21, 21, 59, 17, 292, DateTimeKind.Utc).AddTicks(4133), "Cancha de padel cubierta", 1500m, "", true, false, "Cancha Padel 1", new TimeSpan(0, 8, 0, 0, 0), 2, 4, null },
                    { new Guid("aad6272f-8bd0-417f-b55f-f84b425967f0"), 10, new TimeSpan(0, 22, 0, 0, 0), new DateTime(2026, 2, 21, 21, 59, 17, 292, DateTimeKind.Utc).AddTicks(4136), "Cancha de fÃºtbol 5 con iluminaciÃ³n", 2500m, "", true, false, "Cancha FÃºtbol 5", new TimeSpan(0, 8, 0, 0, 0), 1, 4, null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "FirstName", "IsActive", "IsDeleted", "LastName", "PasswordHash", "Phone", "Role", "UpdatedAt" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000001"), new DateTime(2026, 2, 21, 21, 59, 17, 292, DateTimeKind.Utc).AddTicks(3630), "admin@courtbooking.com", "Admin", true, false, "CourtBooking", "$2a$11$je3donAc0hXzAa7r525aneRMHkhsYwKkNqhHxWp9FEOoFDUQkA6ua", "000000000", 2, null });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_CourtId",
                table: "Bookings",
                column: "CourtId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_UserId",
                table: "Bookings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Courts");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
