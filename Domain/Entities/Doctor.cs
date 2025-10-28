namespace Domain.Entities
{
    public class Doctor
    {
        public long DoctorId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? LicenseNumber { get; set; } // matrícula
        public string? Biography { get; set; }

        // Relación con especialidades
        public ICollection<DoctorSpecialty>? DoctorSpecialties { get; set; }

    }
}
