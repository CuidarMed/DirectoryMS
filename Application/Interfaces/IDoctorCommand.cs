using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IDoctorCommand
    {
        Task<Doctor> CreateAsync(Doctor doctor);
        Task<Doctor> UpdateAsync(Doctor doctor);
    }
}
