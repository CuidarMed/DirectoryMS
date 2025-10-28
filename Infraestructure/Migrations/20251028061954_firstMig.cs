using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class firstMig : Migration
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
                    Biography = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
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
                    MembershipNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patient", x => x.PatientId);
                });

            migrationBuilder.CreateTable(
                name: "Specialty",
                columns: table => new
                {
                    SpecialtyId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specialty", x => x.SpecialtyId);
                });

            migrationBuilder.CreateTable(
                name: "DoctorSpecialty",
                columns: table => new
                {
                    DoctorId = table.Column<long>(type: "bigint", nullable: false),
                    SpecialtyId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorSpecialty", x => new { x.DoctorId, x.SpecialtyId });
                    table.ForeignKey(
                        name: "FK_DoctorSpecialty_Doctor_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctor",
                        principalColumn: "DoctorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorSpecialty_Specialty_SpecialtyId",
                        column: x => x.SpecialtyId,
                        principalTable: "Specialty",
                        principalColumn: "SpecialtyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Doctor",
                columns: new[] { "DoctorId", "Biography", "FirstName", "LastName", "LicenseNumber" },
                values: new object[,]
                {
                    { 1L, "Especialista en cardiología con 10 años de experiencia.", "Juan", "Pérez", "ABC123" },
                    { 2L, "Dermatóloga dedicada al cuidado de la piel.", "María", "Gómez", "DEF456" },
                    { 3L, "Pediatra comprometido con la salud infantil.", "Carlos", "López", "GHI789" },
                    { 4L, "Ginecóloga especializada en salud femenina.", "Ana", "Martínez", "JKL012" },
                    { 5L, "Ortopedista con amplia experiencia en lesiones deportivas.", "Luis", "Rodríguez", "MNO345" }
                });

            migrationBuilder.InsertData(
                table: "Patient",
                columns: new[] { "PatientId", "Adress", "DateOfBirth", "Dni", "HealthPlan", "LastName", "MembershipNumber", "Name" },
                values: new object[,]
                {
                    { 1L, "Calle Falsa 123", new DateOnly(1985, 5, 20), 12345678, "Plan A", "García", "A12345", "Pedro" },
                    { 2L, "Avenida Siempre Viva 742", new DateOnly(1990, 8, 15), 87654321, "Plan B", "López", "B67890", "María" },
                    { 3L, "Boulevard Central 456", new DateOnly(1978, 12, 5), 11223344, "Plan C", "Martínez", "C11223", "Juan" },
                    { 4L, "Calle del Sol 789", new DateOnly(2000, 3, 30), 44332211, "Plan A", "Sánchez", "A44556", "Ana" },
                    { 5L, "Avenida de la Luna 321", new DateOnly(1995, 7, 10), 55667788, "Plan B", "Fernández", "B77889", "Luis" }
                });

            migrationBuilder.InsertData(
                table: "Specialty",
                columns: new[] { "SpecialtyId", "Description", "Name" },
                values: new object[,]
                {
                    { 1L, "Especialista enfocado en la salud cardiovascuilar.", "Cardiology" },
                    { 2L, "Especialista en el cuidado de la piel.", "Dermatology" },
                    { 3L, "Especialista en la salud infantil.", "Pediatrics" },
                    { 4L, "Especialista en la salud femenina.", "Gynecology" },
                    { 5L, "Especialista en el sistema musculoesquelético.", "Orthopedics" }
                });

            migrationBuilder.InsertData(
                table: "DoctorSpecialty",
                columns: new[] { "DoctorId", "SpecialtyId" },
                values: new object[,]
                {
                    { 1L, 1L },
                    { 1L, 3L },
                    { 2L, 2L },
                    { 3L, 3L },
                    { 4L, 4L },
                    { 5L, 5L }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DoctorSpecialty_SpecialtyId",
                table: "DoctorSpecialty",
                column: "SpecialtyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoctorSpecialty");

            migrationBuilder.DropTable(
                name: "Patient");

            migrationBuilder.DropTable(
                name: "Doctor");

            migrationBuilder.DropTable(
                name: "Specialty");
        }
    }
}
