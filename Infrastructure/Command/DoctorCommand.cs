using Application.Interfaces;
using Domain.Entities;
using Infraestructure.Persistence;

namespace Infraestructure.Command
{
    public class DoctorCommand : IDoctorCommand
    {
        private readonly AppDbContext _context;

        public DoctorCommand(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Doctor> CreateAsync(Doctor doctor)
        {

            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();
            return doctor;
        }
        public async Task<Doctor> UpdateAsync(Doctor doctor)
        {
            _context.Doctors.Update(doctor);

            _context.Entry(doctor).Property(d => d.Specialty).IsModified = true;
            
            await _context.SaveChangesAsync();
                  
            return doctor;
        }
    }
}
