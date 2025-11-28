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
    public class PatientQuery : IPatientQuery
    {
        private readonly AppDbContext _context;

        public PatientQuery(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Patient> getPatientById(long id)
        {
            Patient? patient = await _context.Patients.AsNoTracking()
                                .FirstOrDefaultAsync(p => p.PatientId == id);

            return patient;
        }

        public async Task<Patient> getPatientByUserId(long userId)
        {
            return await _context.Patients.AsNoTracking()
                .FirstOrDefaultAsync(p => p.UserId == userId);
        }

        public async Task<IEnumerable<Patient>> getAllPatients()
        {
            return await _context.Patients.AsNoTracking()
                .ToListAsync();
        }
    }
}
