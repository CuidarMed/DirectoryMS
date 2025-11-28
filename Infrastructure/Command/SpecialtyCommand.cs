using Application.Interfaces;
using Domain.Entities;
using Infraestructure.Persistence;

namespace Infraestructure.Command
{
    public class SpecialtyCommand : ISpecialtyCommand
    {
        private readonly AppDbContext _context;

        public SpecialtyCommand(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Specialty> CreateAsync(Specialty specialty)
        {
            _context.Specialties.Add(specialty);
            await _context.SaveChangesAsync();
            return specialty;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var specialty = await _context.Specialties.FindAsync(id);
            if (specialty == null) return false;

            // Soft delete: marcar como inactivo en lugar de eliminar físicamente
            specialty.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}




