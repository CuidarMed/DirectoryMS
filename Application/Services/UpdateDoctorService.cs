using Application.DTOs.Doctors;
using Application.Interfaces;

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

            // Actualizar Specialty si se proporciona (puede ser null explícitamente)
            Console.WriteLine($"[UpdateDoctorService] Request recibido - Specialty: '{request.Specialty}'");
            Console.WriteLine($"[UpdateDoctorService] Specialty != null: {request.Specialty != null}");
            
            // Actualizar Specialty siempre si se proporciona, incluso si es un string vacío
            // Esto permite establecer null explícitamente
            if (request.Specialty != null)
            {
                var newSpecialty = string.IsNullOrWhiteSpace(request.Specialty) ? null : request.Specialty.Trim();
                Console.WriteLine($"[UpdateDoctorService] Actualizando Specialty de '{doctor.Specialty}' a '{newSpecialty}'");
                doctor.Specialty = newSpecialty;
            }
            else
            {
                Console.WriteLine($"[UpdateDoctorService] Specialty no proporcionado en el request (es null)");
            }

            Console.WriteLine($"[UpdateDoctorService] Doctor antes de guardar - Specialty: '{doctor.Specialty}'");

            // Guardar cambios
            var updatedDoctor = await _doctorCommand.UpdateAsync(doctor);
            
            Console.WriteLine($"[UpdateDoctorService] Doctor después de guardar - Specialty: '{updatedDoctor.Specialty}'");

            // Retornar respuesta
            return new UpdateDoctorResponse
            {
                DoctorId = updatedDoctor.DoctorId,
                FirstName = updatedDoctor.FirstName,
                LastName = updatedDoctor.LastName,
                LicenseNumber = updatedDoctor.LicenseNumber,
                Biography = updatedDoctor.Biography,
                Specialty = updatedDoctor.Specialty,
                UserId = updatedDoctor.UserId
            };
        }
    }
}
