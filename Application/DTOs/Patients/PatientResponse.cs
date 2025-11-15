namespace Application.DTOs.Patients
{
    public class PatientResponse
    {
        public long PatientId { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public int Dni { get; set; }
        public string? Adress { get; set; }
        public string? Phone { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string? HealthPlan { get; set; }
        public string? MembershipNumber { get; set; }
        public long UserId { get; set; }
    }
}
