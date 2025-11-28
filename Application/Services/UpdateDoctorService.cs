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
            {
                Console.WriteLine($"[UpdateDoctorService] Actualizando FirstName de '{doctor.FirstName}' a '{request.FirstName}'");
                doctor.FirstName = request.FirstName;
            }

            if (!string.IsNullOrEmpty(request.LastName))
            {
                Console.WriteLine($"[UpdateDoctorService] Actualizando LastName de '{doctor.LastName}' a '{request.LastName}'");
                doctor.LastName = request.LastName;
            }

            if (!string.IsNullOrEmpty(request.LicenseNumber))
            {
                Console.WriteLine($"[UpdateDoctorService] Actualizando LicenseNumber de '{doctor.LicenseNumber}' a '{request.LicenseNumber}'");
                doctor.LicenseNumber = request.LicenseNumber;
            }
            else
            {
                Console.WriteLine($"[UpdateDoctorService] LicenseNumber no proporcionado o vacío en el request. Valor actual: '{doctor.LicenseNumber}'");
            }

            if (!string.IsNullOrEmpty(request.Biography))
            {
                Console.WriteLine($"[UpdateDoctorService] Actualizando Biography de '{doctor.Biography}' a '{request.Biography}'");
                doctor.Biography = request.Biography;
            }
            else
            {
                Console.WriteLine($"[UpdateDoctorService] Biography no proporcionado o vacío en el request. Valor actual: '{doctor.Biography ?? "null"}'");
            }

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
