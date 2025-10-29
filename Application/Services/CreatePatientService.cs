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
            var user = await _query.getPatientById(p.UserId);

            if(user == null)
            {
                throw new NotFoundException("Usuario no encontrado");
            }

            var newPatient = new Patient
            {
                Dni = p.Dni.HasValue ? p.Dni.Value : 0,
                Name = p.FirstName,
                LastName = p.LastName,
                Adress = p.Adress,
                DateOfBirth = p.DateOfBirth,
                HealthPlan = p.HealthPlan,
                MembershipNumber = p.MembershipNumber,
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
