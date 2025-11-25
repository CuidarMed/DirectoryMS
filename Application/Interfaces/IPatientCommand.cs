using Domain.Entities;

namespace Application.Interfaces
{
    public interface IPatientCommand
    {
        Task addPatient(Patient patient);
        Task<Patient> updatePatient(Patient patient);
    }
}
