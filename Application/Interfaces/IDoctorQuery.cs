using Domain.Entities;

namespace Application.Interfaces
{
    public interface IDoctorQuery
    {
        Task<Doctor?> GetByIdAsync(long id);
        Task<List<Doctor>> GetAllAsync();
        Task<Doctor?> GetByUserIdAsync(long userId);
    }
}
