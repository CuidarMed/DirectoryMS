using Application.DTOs.Patients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUpdatePatientService
    {
        Task<PatientResponse> UpdatePatient(long id, UpdatePatientRequest request);
    }
}
