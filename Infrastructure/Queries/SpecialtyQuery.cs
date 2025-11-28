using Application.Interfaces;
using Domain.Entities;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Queries
{
    public class SpecialtyQuery : ISpecialtyQuery
    {
        private readonly AppDbContext _context;

        public SpecialtyQuery(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Specialty?> GetByIdAsync(long id)
        {
            return await _context.Specialties
                .FirstOrDefaultAsync(s => s.SpecialtyId == id && s.IsActive);
        }

        public async Task<List<Specialty>> GetAllAsync()
        {
            return await _context.Specialties
                .Where(s => s.IsActive)
                .OrderBy(s => s.Name)
                .ToListAsync();
        }
    }
}




