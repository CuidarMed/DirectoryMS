using Application.DTOs.Specialties;

namespace Application.Interfaces
{
    public interface ICreateSpecialtyService
    {
        Task<SpecialtyResponse> CreateSpecialtyAsync(CreateSpecialtyRequest request);
    }
}




