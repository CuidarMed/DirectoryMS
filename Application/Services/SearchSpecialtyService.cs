using Application.DTOs.Specialties;
using Application.Interfaces;

namespace Application.Services
{
    public class SearchSpecialtyService : ISearchSpecialtyService
    {
        private readonly ISpecialtyQuery _specialtyQuery;

        public SearchSpecialtyService(ISpecialtyQuery specialtyQuery)
        {
            _specialtyQuery = specialtyQuery;
        }

        public async Task<IEnumerable<SpecialtyResponse>> GetAllSpecialtiesAsync()
        {
            var specialties = await _specialtyQuery.GetAllAsync();
            return specialties.Select(s => new SpecialtyResponse
            {
                SpecialtyId = s.SpecialtyId,
                Name = s.Name,
                Color = s.Color,
                IsActive = s.IsActive,
                CreatedAt = s.CreatedAt
            });
        }

        public async Task<SpecialtyResponse?> GetSpecialtyByIdAsync(long id)
        {
            var specialty = await _specialtyQuery.GetByIdAsync(id);
            if (specialty == null) return null;

            return new SpecialtyResponse
            {
                SpecialtyId = specialty.SpecialtyId,
                Name = specialty.Name,
                Color = specialty.Color,
                IsActive = specialty.IsActive,
                CreatedAt = specialty.CreatedAt
            };
        }
    }
}




