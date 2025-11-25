using Application.DTOs.Patients;

namespace Application.Interfaces
{
    public interface IUpdatePatientService
    {
        Task<PatientResponse> UpdatePatient(long id, UpdatePatientRequest request);
    }
}
