namespace Application.DTOs.Patients
{
    public class UpdatePatientRequest
    {
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public int Dni { get; set; }
        public string? Adress { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public string? HealthPlan { get; set; }
        public string? MembershipNumber { get; set; }
    }
}
