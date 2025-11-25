namespace Application.DTOs.Doctors
{
    public class UpdateDoctorRequest
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? LicenseNumber { get; set; } // matrícula
        public string? Biography { get; set; }
        public string? Specialty { get; set; } 
        public string? Phone { get; set; } 
    }
}
