using Application.DTOs.Specialties;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services
{
    public class CreateSpecialtyService : ICreateSpecialtyService
    {
        private readonly ISpecialtyCommand _specialtyCommand;

        public CreateSpecialtyService(ISpecialtyCommand specialtyCommand)
        {
            _specialtyCommand = specialtyCommand;
        }

        public async Task<SpecialtyResponse> CreateSpecialtyAsync(CreateSpecialtyRequest request)
        {
            var specialty = new Specialty
            {
                Name = request.Name,
                Color = request.Color,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            specialty = await _specialtyCommand.CreateAsync(specialty);

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




