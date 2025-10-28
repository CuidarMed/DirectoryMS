namespace Domain.Entities
{
    public class Specialty
    {
        public long SpecialtyId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public ICollection<DoctorSpecialty>? DoctorSpecialties { get; set; }

    }
}
