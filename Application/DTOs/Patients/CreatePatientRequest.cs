using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Patients
{
    public class CreatePatientRequest
    {
        public int? Dni { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Adress { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string? HealthPlan { get; set; }
        public string? MembershipNumber { get; set; }
    }
}
