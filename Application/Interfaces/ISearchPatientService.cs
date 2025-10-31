using Application.DTOs.Patients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ISearchPatientService
    {
        Task<PatientResponse> getPatientById(long id);
        Task<PatientResponse> getPatientByUserId(long userId);
    }
}
