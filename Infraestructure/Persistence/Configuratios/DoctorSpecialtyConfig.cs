using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Persistence.Configuratios
{
    public class DoctorSpecialtyConfig : IEntityTypeConfiguration<DoctorSpecialty>
    {
        public void Configure(EntityTypeBuilder<DoctorSpecialty> builder)
        {
            builder.HasData(
                new DoctorSpecialty {DoctorId = 1, SpecialtyId = 1 },
                new DoctorSpecialty {DoctorId = 2, SpecialtyId = 2 },
                new DoctorSpecialty {DoctorId = 3, SpecialtyId = 3 },
                new DoctorSpecialty {DoctorId = 4, SpecialtyId = 4 },
                new DoctorSpecialty {DoctorId = 5, SpecialtyId = 5 },
                new DoctorSpecialty {DoctorId = 1, SpecialtyId = 3 }
            );
        }
    }
}
