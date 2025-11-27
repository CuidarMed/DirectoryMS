using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using System;

namespace Infrastructure.Command
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
            Console.WriteLine($"[DoctorCommand] Actualizando doctor ID: {doctor.DoctorId}");
            Console.WriteLine($"[DoctorCommand] Specialty antes de Update: '{doctor.Specialty}'");
            
            // Marcar la entidad como modificada
            _context.Doctors.Update(doctor);
            
            // Forzar el seguimiento de todas las propiedades, especialmente Specialty
            _context.Entry(doctor).Property(d => d.Specialty).IsModified = true;
            
            Console.WriteLine($"[DoctorCommand] Entry Specialty IsModified: {_context.Entry(doctor).Property(d => d.Specialty).IsModified}");
            
            await _context.SaveChangesAsync();
            
            Console.WriteLine($"[DoctorCommand] Specialty después de SaveChanges: '{doctor.Specialty}'");
            
            return doctor;
        }
    }
}
