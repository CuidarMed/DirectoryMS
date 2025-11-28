namespace Domain.Entities
{
    public class Specialty
    {
        public long SpecialtyId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Color { get; set; } = "#2563eb";
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}




