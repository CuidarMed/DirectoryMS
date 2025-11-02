using Application.DTOs.Doctors;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services
{
    public class CreateDoctorService : ICreateDoctorService
    {
        private readonly IDoctorCommand _doctorCommand;
        private readonly IDoctorQuery _doctorQuery;

        public CreateDoctorService(
            IDoctorCommand doctorCommand,
            IDoctorQuery doctorQuery)
        {
            _doctorCommand = doctorCommand;
            _doctorQuery = doctorQuery;
        }

        public async Task<DoctorResponse> CreateDoctorAsync(CreateDoctorRequest request)
        {
            // Crear la entidad Doctor
            var doctor = new Doctor
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                LicenseNumber = request.LicenseNumber,
                Biography = request.Biography,
                Specialty = request.Specialty, // especialidad
                UserId = (int)request.UserId
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
                Biography = doctor.Biography,
                Specialty = doctor.Specialty, // especialidad
                UserId = doctor.UserId
            };
        }
    }
}
