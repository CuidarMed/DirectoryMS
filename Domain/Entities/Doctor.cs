﻿namespace Domain.Entities
{
    public class Doctor
    {
        public long DoctorId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? LicenseNumber { get; set; } // matrícula
        public string? Biography { get; set; }
        public string? Specialty { get; set; } // especialidad
        public long UserId { get; set; }

    }
}
