using Application.DTOs.Patients;
using Application.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CreatePatientService : ICreatePatientService
    {
        private IPatientCommand _command;
        private IPatientQuery _query;

        public CreatePatientService(IPatientCommand command, IPatientQuery query)
        {
            _command = command;
            _query = query;
        }

        public async Task<PatientResponse> createaPatient(CreatePatientRequest p)
        {
            // Validar que el request no sea null
            if (p == null)
            {
                throw new ArgumentNullException(nameof(p), "El request del paciente no puede ser nulo.");
            }

            // Validar campos requeridos
            if (string.IsNullOrWhiteSpace(p.FirstName))
            {
                throw new ArgumentException("El nombre del paciente es requerido.", nameof(p.FirstName));
            }

            if (string.IsNullOrWhiteSpace(p.LastName))
            {
                throw new ArgumentException("El apellido del paciente es requerido.", nameof(p.LastName));
            }

            if (p.UserId <= 0)
            {
                throw new ArgumentException("El UserId debe ser mayor que 0.", nameof(p.UserId));
            }

            var existingPatient = await _query.getPatientByUserId(p.UserId);

            if(existingPatient != null)
            {
                throw new ConflictException("Ya existe un paciente asociado a este usuario.");
            }

            var newPatient = new Patient
            {
                Dni = p.Dni.HasValue ? p.Dni.Value : 0,
                Name = p.FirstName ?? string.Empty,
                LastName = p.LastName ?? string.Empty,
                Adress = p.Adress ?? string.Empty,
                Phone = p.Phone ?? string.Empty,
                DateOfBirth = p.DateOfBirth,
                HealthPlan = p.HealthPlan ?? string.Empty, // Valor por defecto si es null (se actualizará después)
                MembershipNumber = p.MembershipNumber ?? string.Empty, // Valor por defecto si es null (se actualizará después)
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
