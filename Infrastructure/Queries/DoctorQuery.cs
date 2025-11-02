﻿using Application.Interfaces;
using Domain.Entities;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

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
                .FirstOrDefaultAsync(d => d.DoctorId == id);
        }

        public async Task<List<Doctor>> GetAllAsync()
        {
            //var query = _context.Doctors
            //    .Include(d => d.UserNavigation)
            //    .AsQueryable();

            //return await query.ToListAsync();

            return await _context.Doctors
                .ToListAsync();
        }

        public async Task<Doctor?> GetByUserIdAsync(long userId)
        {
            return await _context.Doctors
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.UserId == userId);
        }
    }
}
