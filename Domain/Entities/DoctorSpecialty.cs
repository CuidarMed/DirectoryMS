namespace Domain.Entities
{
    public class DoctorSpecialty
    {
        public long DoctorId { get; set; }
        public Doctor? Doctor { get; set; }
        public long SpecialtyId { get; set; }
        public Specialty? Specialty { get; set; }
    }
}
