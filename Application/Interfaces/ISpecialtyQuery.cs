using Domain.Entities;

namespace Application.Interfaces
{
    public interface ISpecialtyQuery
    {
        Task<Specialty?> GetByIdAsync(long id);
        Task<List<Specialty>> GetAllAsync();
    }
}




