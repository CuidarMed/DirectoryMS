using Application.DTOs.Doctors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICreateDoctorService
    {
        Task<DoctorResponse> CreateDoctorAsync(CreateDoctorRequest request);
    }
}
