using Application.DTOs.Doctors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUpdateDoctorService
    {
        Task<UpdateDoctorResponse> UpdateDoctorAsync(long id, UpdateDoctorRequest request);
    }
}
