using Application.DTOs.Patients;

namespace Application.Interfaces
{
    public interface ICreatePatientService
    {
        Task<PatientResponse> createaPatient(CreatePatientRequest p);
    }
}
