using Application.DTOs.Doctors;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UpdateDoctorService : IUpdateDoctorService
    {
        private readonly IDoctorCommand _doctorCommand;
        private readonly IDoctorQuery _doctorQuery;

        public UpdateDoctorService(IDoctorCommand doctorCommand, IDoctorQuery doctorQuery)
        {
            _doctorCommand = doctorCommand;
            _doctorQuery = doctorQuery;
        }

        public async Task<UpdateDoctorResponse> UpdateDoctorAsync(long id, UpdateDoctorRequest request)
        {
            // Buscar el doctor existente
            var doctor = await _doctorQuery.GetByIdAsync(id);
            if (doctor == null)
            {
                throw new Exception("Doctor no encontrado");
            }

            // Actualizar solo los campos que no sean null
            if (!string.IsNullOrEmpty(request.FirstName))
                doctor.FirstName = request.FirstName;

            if (!string.IsNullOrEmpty(request.LastName))
                doctor.LastName = request.LastName;

            if (!string.IsNullOrEmpty(request.LicenseNumber))
                doctor.LicenseNumber = request.LicenseNumber;

            if (!string.IsNullOrEmpty(request.Biography))
                doctor.Biography = request.Biography;

            // Guardar cambios
            var updatedDoctor = await _doctorCommand.UpdateAsync(doctor);

            // Retornar respuesta
            return new UpdateDoctorResponse
            {
                FirstName = updatedDoctor.FirstName,
                LastName = updatedDoctor.LastName,
                LicenseNumber = updatedDoctor.LicenseNumber,
                Biography = updatedDoctor.Biography
            };
        }
    }
}
