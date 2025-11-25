using Application.DTOs.Doctors;
using Application.Interfaces;
using Domain.Entities;
using FluentValidation;

namespace Application.Services
{
    public class CreateDoctorService : ICreateDoctorService
    {
        private readonly IDoctorCommand _doctorCommand;
        private readonly IValidator<CreateDoctorRequest> _validator;

        public CreateDoctorService(IDoctorCommand doctorCommand, IValidator<CreateDoctorRequest> validator)
        {
            _doctorCommand = doctorCommand;
            _validator = validator;
        }

        public async Task<DoctorResponse> CreateDoctorAsync(CreateDoctorRequest request)
        {
            var result = await _validator.ValidateAsync(request);

            if (!result.IsValid)
                throw new ValidationException(result.Errors);


            var doctor = new Doctor
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                LicenseNumber = request.LicenseNumber,
                Biography = request.Biography,
                Specialty = request.Specialty,
                Phone = request.Phone,
                UserId = request.UserId
            };
            doctor = await _doctorCommand.CreateAsync(doctor);

            return new DoctorResponse
            {
                DoctorId = doctor.DoctorId,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                LicenseNumber = doctor.LicenseNumber,
                Biography = doctor.Biography,
                Specialty = doctor.Specialty, 
                Phone = doctor.Phone, 
                UserId = doctor.UserId
            };
        }
    }
}
