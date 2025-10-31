using Application.DTOs.Patients;
using Application.Exceptions;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class SearchPatientService : ISearchPatientService
    {
        private readonly IPatientQuery _query;

        public SearchPatientService(IPatientQuery query)
        {
            _query = query;
        }
        public async Task<PatientResponse> getPatientById(long id)
        {
            var patient = await _query.getPatientById(id);

            if (patient == null)
            {
                throw new NotFoundException("Usuario no encontrado");
            }

            return MapPatient(patient);
        }

        public async Task<PatientResponse> getPatientByUserId(long userId)
        {
            var patient = await _query.getPatientByUserId(userId);

            if (patient == null)
            {
                throw new NotFoundException("Paciente no encontrado para el usuario especificado");
            }

            return MapPatient(patient);
        }

        private static PatientResponse MapPatient(Domain.Entities.Patient patient)
        {
            return new PatientResponse
            {
                PatientId = patient.PatientId,
                Name = patient.Name,
                LastName = patient.LastName,
                Dni = patient.Dni,
                Adress = patient.Adress,
                DateOfBirth = patient.DateOfBirth,
                HealthPlan = patient.HealthPlan,
                MembershipNumber = patient.MembershipNumber,
                UserId = patient.UserId,
            };
        }
    }
}
