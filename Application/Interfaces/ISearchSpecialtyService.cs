using Application.DTOs.Specialties;

namespace Application.Interfaces
{
    public interface ISearchSpecialtyService
    {
        Task<IEnumerable<SpecialtyResponse>> GetAllSpecialtiesAsync();
        Task<SpecialtyResponse?> GetSpecialtyByIdAsync(long id);
    }
}




