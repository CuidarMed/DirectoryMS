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

            Console.WriteLine($"[UpdatePatientService] Actualizando paciente ID {id}");
            Console.WriteLine($"[UpdatePatientService] Request recibido: Name={request.Name}, LastName={request.LastName}, DateOfBirth={request.DateOfBirth}, Adress={request.Adress}, HealthPlan={request.HealthPlan}, MembershipNumber={request.MembershipNumber}");

            if (!string.IsNullOrEmpty(request.Name))
                patient.Name = request.Name;

            if (!string.IsNullOrEmpty(request.LastName))
                patient.LastName = request.LastName;

            if(!string.IsNullOrEmpty(request.Adress))
                patient.Adress = request.Adress;

            if (!string.IsNullOrEmpty(request.Phone))
            {
                Console.WriteLine($"[UpdatePatientService] Actualizando Phone de '{patient.Phone}' a '{request.Phone}'");
                patient.Phone = request.Phone;
            }
            else
            {
                Console.WriteLine($"[UpdatePatientService] Phone no proporcionado o vacío en el request. Valor actual: '{patient.Phone}'");
            }

            if (request.DateOfBirth.HasValue)
            {
                Console.WriteLine($"[UpdatePatientService] Actualizando DateOfBirth de {patient.DateOfBirth} a {request.DateOfBirth.Value}");
                patient.DateOfBirth = request.DateOfBirth.Value;
            }
            else
            {
                Console.WriteLine($"[UpdatePatientService] DateOfBirth no proporcionado en el request (HasValue={request.DateOfBirth.HasValue})");
            }

            if (!string.IsNullOrEmpty(request.HealthPlan))
            {
                Console.WriteLine($"[UpdatePatientService] Actualizando HealthPlan de '{patient.HealthPlan}' a '{request.HealthPlan}'");
                patient.HealthPlan = request.HealthPlan;
            }
            else
            {
                Console.WriteLine($"[UpdatePatientService] HealthPlan no proporcionado o vacío en el request");
            }

            if (!string.IsNullOrEmpty(request.MembershipNumber))
            {
                Console.WriteLine($"[UpdatePatientService] Actualizando MembershipNumber de '{patient.MembershipNumber}' a '{request.MembershipNumber}'");
                patient.MembershipNumber = request.MembershipNumber;
            }
            else
            {
                Console.WriteLine($"[UpdatePatientService] MembershipNumber no proporcionado o vacío en el request. Valor actual: '{patient.MembershipNumber}'");
            }

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
