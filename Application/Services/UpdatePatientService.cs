using Application.DTOs.Patients;
using Application.Exceptions;
using Application.Interfaces;
using FluentValidation;

namespace Application.Services
{
    public class UpdatePatientService : IUpdatePatientService
    {
        private IPatientCommand _command;
        private IPatientQuery _query;
        private IValidator<UpdatePatientRequest> _validator;

        public UpdatePatientService(IPatientCommand command, IPatientQuery query, IValidator<UpdatePatientRequest> validator)
        {
            _command = command;
            _query = query;
            _validator = validator;
        }

        public async Task<PatientResponse> UpdatePatient(long id, UpdatePatientRequest request)
        {
            var result = await _validator.ValidateAsync(request);
            if (!result.IsValid)
                throw new ValidationException(result.Errors);

            var patient = await _query.getPatientById(id);

            if (patient == null) {
                throw new BadRequestException("Paciente no encontrado");
            }

            if (!string.IsNullOrEmpty(request.Name))
                patient.Name = request.Name;

            if (!string.IsNullOrEmpty(request.LastName))
                patient.LastName = request.LastName;

            if(!string.IsNullOrEmpty(request.Adress))
                patient.Adress = request.Adress;

            if (!string.IsNullOrWhiteSpace(request.Phone))
                patient.Phone = request.Phone;


            if (request.DateOfBirth.HasValue)              
                patient.DateOfBirth = request.DateOfBirth.Value;
           
            if (!string.IsNullOrEmpty(request.HealthPlan))
                patient.HealthPlan = request.HealthPlan;

            if (!string.IsNullOrEmpty(request.MembershipNumber))
                patient.MembershipNumber = request.MembershipNumber;
            
            var updatePatient = await _command.updatePatient(patient);

            return new PatientResponse { 
                PatientId = id,
                Name = updatePatient.Name,
                LastName = updatePatient.LastName,
                Dni = updatePatient.Dni,
                Adress = updatePatient.Adress,
                Phone = updatePatient.Phone,
                DateOfBirth = updatePatient.DateOfBirth,
                HealthPlan = updatePatient.HealthPlan,
                MembershipNumber = updatePatient.MembershipNumber,
                UserId = updatePatient.UserId,
            };
        }
    }
}
