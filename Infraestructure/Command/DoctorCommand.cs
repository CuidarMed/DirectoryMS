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

        //public Task ExecuteAsync()
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<Doctor> UpdateAsync(Doctor doctor)
        {
            _context.Doctors.Update(doctor);
            await _context.SaveChangesAsync();
            return doctor;
        }
    }
}
