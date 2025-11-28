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

        public async Task<List<PatientResponse>> getAllPatients()
        {
            var patients = await _query.getAllPatients();
            if (patients == null)
            {
                return new List<PatientResponse>();
            }
            return patients.Select(MapPatient).ToList();
        }

        private static PatientResponse MapPatient(Domain.Entities.Patient patient)
        {
            // Asegurar que HealthPlan y MembershipNumber no sean null
            var healthPlan = patient.HealthPlan;
            var membershipNumber = patient.MembershipNumber;
            
            // Si están vacíos o null, asignar null explícitamente (no string.Empty)
            // El JSON serializer convertirá null a null en JSON, que el frontend puede manejar mejor
            if (string.IsNullOrWhiteSpace(healthPlan))
            {
                healthPlan = null;
            }
            
            if (string.IsNullOrWhiteSpace(membershipNumber))
            {
                membershipNumber = null;
            }
            
            return new PatientResponse
            {
                PatientId = patient.PatientId,
                Name = patient.Name,
                LastName = patient.LastName,
                Dni = patient.Dni,
                Adress = patient.Adress,
                DateOfBirth = patient.DateOfBirth,
                HealthPlan = healthPlan,
                MembershipNumber = membershipNumber,
                UserId = patient.UserId,
            };
        }
    }
}
