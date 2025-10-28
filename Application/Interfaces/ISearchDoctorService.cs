using Application.DTOs.Doctors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ISearchDoctorService
    {
        Task<DoctorResponse?> GetByIdAsync(long id);
        Task<List<DoctorResponse>> GetAllAsync();
    }
}
