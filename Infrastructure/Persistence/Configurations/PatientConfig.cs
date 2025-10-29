using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class PatientConfig : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.HasData(
                new Patient { PatientId = 1, Dni = 12345678, Name = "Luis", LastName = "Fernandez", Adress = "Calle Falsa 123", DateOfBirth = new DateOnly(1985, 5, 20), HealthPlan = "Plan A", MembershipNumber = "PA123456", UserId = 6 },
                new Patient { PatientId = 2, Dni = 87654321, Name = "Sofia", LastName = "Ramirez", Adress = "Avenida Siempre Viva 742", DateOfBirth = new DateOnly(1990, 8, 15), HealthPlan = "Plan B", MembershipNumber = "PB654321", UserId = 7 },
                new Patient { PatientId = 3, Dni = 11223344, Name = "Mateo", LastName = "Gonzalez", Adress = "Boulevard Central 456", DateOfBirth = new DateOnly(1978, 12, 5), HealthPlan = "Plan C", MembershipNumber = "PC112233", UserId = 8 },
                new Patient { PatientId = 4, Dni = 44332211, Name = "Isabella", LastName = "Vega", Adress = "Calle del Sol 789", DateOfBirth = new DateOnly(2000, 3, 30), HealthPlan = "Plan A", MembershipNumber = "PA445566", UserId = 9 }
            );
        }
    }
}
