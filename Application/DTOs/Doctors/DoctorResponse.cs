namespace Application.DTOs.Doctors
{
    public class DoctorResponse
    {
        public long DoctorId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? LicenseNumber { get; set; } // matrícula
        public string? Biography { get; set; }
    }
}
