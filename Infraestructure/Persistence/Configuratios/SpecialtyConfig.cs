using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Persistence.Configuratios
{
    public class SpecialtyConfig : IEntityTypeConfiguration<Specialty>
    {
        public void Configure(EntityTypeBuilder<Specialty> builder)
        {
            builder.HasData(
                new Specialty { SpecialtyId = 1, Name = "Cardiology", Description = "Especialista enfocado en la salud cardiovascuilar." },
                new Specialty { SpecialtyId = 2, Name = "Dermatology", Description = "Especialista en el cuidado de la piel." },
                new Specialty { SpecialtyId = 3, Name = "Pediatrics", Description = "Especialista en la salud infantil." },
                new Specialty { SpecialtyId = 4, Name = "Gynecology", Description = "Especialista en la salud femenina." },
                new Specialty { SpecialtyId = 5, Name = "Orthopedics", Description = "Especialista en el sistema musculoesquelético." }
                );


        } 
    }
}
