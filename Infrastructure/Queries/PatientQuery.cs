using Application.Interfaces;
using Domain.Entities;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Queries
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
    }
}
