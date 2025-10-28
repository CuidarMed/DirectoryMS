using Application.DTOs.Doctors;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services
{
    public class CreateDoctorService : ICreateDoctorService
    {
        private readonly IDoctorCommand _doctorCommand;

        public CreateDoctorService(IDoctorCommand doctorCommand)
        {
            _doctorCommand = doctorCommand;
        }

        public async Task<DoctorResponse> CreateDoctorAsync(CreateDoctorRequest request)
        {
            // Crear la entidad Doctor
            var doctor = new Doctor
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                LicenseNumber = request.LicenseNumber,
                Biography = request.Biography
            };

            // Guardar en la base de datos
            doctor = await _doctorCommand.CreateAsync(doctor);

            // Retornar la respuesta
            return new DoctorResponse
            {
                DoctorId = doctor.DoctorId,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                LicenseNumber = doctor.LicenseNumber,
                Biography = doctor.Biography
            };
        }
    }
}
