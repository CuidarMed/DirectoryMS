using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Events
{
    public class UserCreatedEvent
    {
        public int UserId { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Dni { get; set; }
        public string Phone { get; set; }

        // Datos de Patient
        public DateOnly? DateOfBirth { get; set; }
        public string? Adress { get; set; }
        public string? HealthPlan { get; set; }
        public string? MembershipNumber { get; set; }

        // Datos de Doctor
        public string? LicenseNumber { get; set; }
        public string? Biography { get; set; }
        public string? Specialty { get; set; }
    }
}
