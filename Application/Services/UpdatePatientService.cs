using Application.DTOs.Patients;
using Application.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UpdatePatientService : IUpdatePatientService
    {
        private IPatientCommand _command;
        private IPatientQuery _query;

        public UpdatePatientService(IPatientCommand command, IPatientQuery query)
        {
            _command = command;
            _query = query;
        }

        public async Task<PatientResponse> UpdatePatient(long id, UpdatePatientRequest request)
        {
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

            if (!string.IsNullOrEmpty(request.HealthPlan))
                patient.HealthPlan = request.HealthPlan;

            var updatePatient = await _command.updatePatient(patient);

            return new PatientResponse { 
                PatientId = id,
                Name = updatePatient.Name,
                LastName = updatePatient.LastName,
                Dni = updatePatient.Dni,
                Adress = updatePatient.Adress,
                DateOfBirth = updatePatient.DateOfBirth,
                HealthPlan = updatePatient.HealthPlan,
                MembershipNumber = updatePatient.MembershipNumber,
                UserId = updatePatient.UserId,
            };
        }
    }
}
