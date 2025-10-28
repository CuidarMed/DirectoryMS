using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Doctors
{
    public class UpdateDoctorResponse
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? LicenseNumber { get; set; } // matrícula
        public string? Biography { get; set; }
    }
}
