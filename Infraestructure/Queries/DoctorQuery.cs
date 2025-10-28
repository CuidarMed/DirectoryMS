using Application.Interfaces;
using Domain.Entities;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Queries
{
    public class DoctorQuery : IDoctorQuery
    {
        private readonly AppDbContext _context;

        public DoctorQuery(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Doctor?> GetByIdAsync(long id)
        {
            return await _context.Doctors
                .Include(d => d.DoctorSpecialties)
                    .ThenInclude(ds => ds.Specialty)
                .FirstOrDefaultAsync(d => d.DoctorId == id);
            
        }

        public async Task<List<Doctor>> GetAllAsync()
        {
            //var query = _context.Doctors
            //    .Include(d => d.UserNavigation)
            //    .AsQueryable();

            //return await query.ToListAsync();

            return await _context.Doctors
                .Include(d => d.DoctorSpecialties)
                    .ThenInclude(ds => ds.Specialty)
                .ToListAsync();
        }
    }
}
