using Application.DTOs.Patients;

namespace Application.Interfaces
{
    public interface ISearchPatientService
    {
        Task<PatientResponse> getPatientById(long id);
        Task<PatientResponse> getPatientByUserId(long userId);
        Task<List<PatientResponse>> GetAllAsync();
    }
}
