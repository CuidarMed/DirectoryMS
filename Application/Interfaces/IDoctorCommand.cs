using Domain.Entities;

namespace Application.Interfaces
{
    public interface IDoctorCommand
    {
        Task<Doctor> CreateAsync(Doctor doctor);
        Task<Doctor> UpdateAsync(Doctor doctor);
    }
}
