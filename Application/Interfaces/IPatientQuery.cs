using Domain.Entities;

namespace Application.Interfaces
{
    public interface IPatientQuery
    {
        Task<Patient> getPatientById(long id);
        Task<Patient> getPatientByUserId(long userId);
        Task<List<Patient>> GetAllAsync();
        Task<bool> ExistsByUserIdAsync(long userId);
    }
}
