using Application.DTOs.Doctors;

namespace Application.Interfaces
{
    public interface ISearchDoctorService
    {
        Task<DoctorResponse?> GetByIdAsync(long id);
        Task<List<DoctorResponse>> GetAllAsync();
        Task<DoctorResponse?> GetByUserIdAsync(long userId);
    }
}
