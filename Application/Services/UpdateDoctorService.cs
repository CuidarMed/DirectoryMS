using Application.DTOs.Doctors;
using Application.Interfaces;
using FluentValidation;

namespace Application.Services
{
    public class UpdateDoctorService : IUpdateDoctorService
    {
        private readonly IDoctorCommand _doctorCommand;
        private readonly IDoctorQuery _doctorQuery;
        private readonly IValidator<UpdateDoctorRequest> _validator;


        public UpdateDoctorService(IDoctorCommand doctorCommand, IDoctorQuery doctorQuery, IValidator<UpdateDoctorRequest> validator )
        {
            _doctorCommand = doctorCommand;
            _doctorQuery = doctorQuery;
            _validator = validator;
        }

        public async Task<UpdateDoctorResponse> UpdateDoctorAsync(long id, UpdateDoctorRequest request)
        {
            var result = await _validator.ValidateAsync(request);

            if (!result.IsValid)
                throw new ValidationException(result.Errors)

            var doctor = await _doctorQuery.GetByIdAsync(id);

            if (doctor == null)
                throw new KeyNotFoundException("Doctor no encontrado.");

            if (request.FirstName != null)
                doctor.FirstName = string.IsNullOrWhiteSpace(request.FirstName) ? null : request.FirstName.Trim();

            if (request.LastName != null)
                doctor.LastName = string.IsNullOrWhiteSpace(request.LastName) ? null : request.LastName.Trim();

            if (request.LicenseNumber != null)
                doctor.LicenseNumber = string.IsNullOrWhiteSpace(request.LicenseNumber) ? null : request.LicenseNumber.Trim();

            if (request.Biography != null)
                doctor.Biography = string.IsNullOrWhiteSpace(request.Biography) ? null : request.Biography.Trim();

            if (request.Phone != null)
                doctor.Phone = string.IsNullOrWhiteSpace(request.Phone) ? null : request.Phone.Trim();

            if (request.Specialty != null)
            {
                doctor.Specialty = string.IsNullOrWhiteSpace(request.Specialty)
                    ? null
                    : request.Specialty.Trim();
            }
            var updatedDoctor = await _doctorCommand.UpdateAsync(doctor);

            return new UpdateDoctorResponse
            {
                DoctorId = updatedDoctor.DoctorId,
                FirstName = updatedDoctor.FirstName,
                LastName = updatedDoctor.LastName,
                LicenseNumber = updatedDoctor.LicenseNumber,
                Biography = updatedDoctor.Biography,
                Specialty = updatedDoctor.Specialty,
                Phone = updatedDoctor.Phone,
                UserId = updatedDoctor.UserId
            };
        }
    }
}

