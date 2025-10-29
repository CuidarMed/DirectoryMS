using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPatientCommand
    {
        Task addPatient(Patient patient);
        Task<Patient> updatePatient(Patient patient);
    }
}
