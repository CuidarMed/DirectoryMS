using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Persistence.Configuratios
{
    public class DoctorConfig : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.HasData(
                new Doctor { DoctorId = 1, FirstName = "Juan", LastName = "Pérez", LicenseNumber = "ABC123", Biography = "Especialista en cardiología con 10 años de experiencia." },
                new Doctor { DoctorId = 2, FirstName = "María", LastName = "Gómez", LicenseNumber = "DEF456", Biography = "Dermatóloga dedicada al cuidado de la piel." },
                new Doctor { DoctorId = 3, FirstName = "Carlos", LastName = "López", LicenseNumber = "GHI789", Biography = "Pediatra comprometido con la salud infantil." },
                new Doctor { DoctorId = 4, FirstName = "Ana", LastName = "Martínez", LicenseNumber = "JKL012", Biography = "Ginecóloga especializada en salud femenina." },
                new Doctor { DoctorId = 5, FirstName = "Luis", LastName = "Rodríguez", LicenseNumber = "MNO345", Biography = "Ortopedista con amplia experiencia en lesiones deportivas." }
            );
        }
    }
}
