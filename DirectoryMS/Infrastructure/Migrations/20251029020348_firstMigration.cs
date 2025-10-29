using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class firstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Doctor",
                columns: table => new
                {
                    DoctorId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LicenseNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Biography = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctor", x => x.DoctorId);
                });

            migrationBuilder.CreateTable(
                name: "Patient",
                columns: table => new
                {
                    PatientId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Dni = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Adress = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    HealthPlan = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MembershipNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patient", x => x.PatientId);
                });

            migrationBuilder.InsertData(
                table: "Doctor",
                columns: new[] { "DoctorId", "Biography", "FirstName", "LastName", "LicenseNumber", "UserId" },
                values: new object[,]
                {
                    { 1L, "Especialista en medicina general con 10 años de experiencia.", "Juan", "Pérez", "ABC123", 2L },
                    { 2L, "Cardióloga dedicada a la salud del corazón.", "María", "Gómez", "DEF456", 3L },
                    { 3L, "Pediatra apasionado por el cuidado infantil.", "Carlos", "López", "GHI789", 4L },
                    { 4L, "Dermatóloga especializada en tratamientos de la piel.", "Ana", "Martínez", "JKL012", 5L }
                });

            migrationBuilder.InsertData(
                table: "Patient",
                columns: new[] { "PatientId", "Adress", "DateOfBirth", "Dni", "HealthPlan", "LastName", "MembershipNumber", "Name", "UserId" },
                values: new object[,]
                {
                    { 1L, "Calle Falsa 123", new DateOnly(1985, 5, 20), 12345678, "Plan A", "Fernandez", "PA123456", "Luis", 6L },
                    { 2L, "Avenida Siempre Viva 742", new DateOnly(1990, 8, 15), 87654321, "Plan B", "Ramirez", "PB654321", "Sofia", 7L },
                    { 3L, "Boulevard Central 456", new DateOnly(1978, 12, 5), 11223344, "Plan C", "Gonzalez", "PC112233", "Mateo", 8L },
                    { 4L, "Calle del Sol 789", new DateOnly(2000, 3, 30), 44332211, "Plan A", "Vega", "PA445566", "Isabella", 9L }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Doctor");

            migrationBuilder.DropTable(
                name: "Patient");
        }
    }
}
