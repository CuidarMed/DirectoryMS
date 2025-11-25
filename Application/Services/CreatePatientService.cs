using Application.DTOs.Patients;
using Application.Interfaces;
using Domain.Entities;
using FluentValidation;

namespace Application.Services
{
    public class CreatePatientService : ICreatePatientService
    {
        private readonly IPatientCommand _command;
        private readonly IPatientQuery _query;
        private readonly IValidator<CreatePatientRequest> _validator;

        public CreatePatientService(IPatientCommand command, IPatientQuery query,IValidator<CreatePatientRequest> validator)
        {
            _command = command;
            _query = query;
            _validator = validator;
        }

        public async Task<PatientResponse> createaPatient(CreatePatientRequest p)
        {
            var result = await _validator.ValidateAsync(p);

            if (!result.IsValid)
                throw new ValidationException(result.Errors);

            var existingPatient = await _query.getPatientByUserId(p.UserId);

            if (existingPatient != null)
                throw new InvalidOperationException("Ya existe un paciente asociado a este usuario.");

            var newPatient = new Patient
            {
                Dni = p.Dni ?? 0,
                Name = p.FirstName?.Trim(),
                LastName = p.LastName?.Trim(),
                Adress = p.Adress?.Trim(),
                Phone = p.Phone?.Trim(),
                DateOfBirth = p.DateOfBirth,
                HealthPlan = p.HealthPlan?.Trim(),
                MembershipNumber = p.MembershipNumber?.Trim(),
                UserId = p.UserId
            };

            await _command.addPatient(newPatient);

            return new PatientResponse
            {
                PatientId = newPatient.PatientId,
                Name = newPatient.Name,
                LastName = newPatient.LastName,
                Dni = newPatient.Dni,
                Adress = newPatient.Adress,
                DateOfBirth = newPatient.DateOfBirth,
                HealthPlan = newPatient.HealthPlan,
                MembershipNumber = newPatient.MembershipNumber,
                UserId = newPatient.UserId
            };
        }
    }
}
