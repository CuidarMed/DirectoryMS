using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class DoctorConfig : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.HasData(
                new Doctor { DoctorId = 1, FirstName = "Juan", LastName = "Pérez", LicenseNumber = "ABC123", Biography = "Especialista en medicina general con 10 años de experiencia.", UserId = 2 },
                new Doctor { DoctorId = 2, FirstName = "María", LastName = "Gómez", LicenseNumber = "DEF456", Biography = "Cardióloga dedicada a la salud del corazón.", UserId = 3 },
                new Doctor { DoctorId = 3, FirstName = "Carlos", LastName = "López", LicenseNumber = "GHI789", Biography = "Pediatra apasionado por el cuidado infantil.", UserId = 4 },
                new Doctor { DoctorId = 4, FirstName = "Ana", LastName = "Martínez", LicenseNumber = "JKL012", Biography = "Dermatóloga especializada en tratamientos de la piel.", UserId = 5 }
            );
        }

    }
}
