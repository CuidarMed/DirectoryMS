using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Persistence.Configuratios
{
    public class PatientConfig : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.HasData(
                new Patient{PatientId = 1,Dni = 12345678,Name = "Pedro",LastName = "García",Adress = "Calle Falsa 123",DateOfBirth = new DateOnly(1985, 5, 20),HealthPlan = "Plan A",MembershipNumber = "A12345"},
                new Patient{PatientId = 2,Dni = 87654321,Name = "María",LastName = "López",Adress = "Avenida Siempre Viva 742",DateOfBirth = new DateOnly(1990, 8, 15),HealthPlan = "Plan B",MembershipNumber = "B67890"},
                new Patient{PatientId = 3,Dni = 11223344,Name = "Juan",LastName = "Martínez",Adress = "Boulevard Central 456",DateOfBirth = new DateOnly(1978, 12, 5),HealthPlan = "Plan C",MembershipNumber = "C11223"},
                new Patient{PatientId = 4,Dni = 44332211,Name = "Ana",LastName = "Sánchez",Adress = "Calle del Sol 789",DateOfBirth = new DateOnly(2000, 3, 30),HealthPlan = "Plan A",MembershipNumber = "A44556"},
                new Patient{PatientId = 5,Dni = 55667788,Name = "Luis",LastName = "Fernández",Adress = "Avenida de la Luna 321",DateOfBirth = new DateOnly(1995, 7, 10),HealthPlan = "Plan B",MembershipNumber = "B77889" }

            );
        }
    }
}
