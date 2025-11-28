using Domain.Entities;

namespace Application.Interfaces
{
    public interface ISpecialtyCommand
    {
        Task<Specialty> CreateAsync(Specialty specialty);
        Task<bool> DeleteAsync(long id);
    }
}




