namespace Application.DTOs.Specialties
{
    public class SpecialtyResponse
    {
        public long SpecialtyId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Color { get; set; } = "#2563eb";
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}




