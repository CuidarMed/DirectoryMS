using Application.DTOs.Doctors;

namespace Application.Interfaces
{
    public interface ICreateDoctorService
    {
        Task<DoctorResponse> CreateDoctorAsync(CreateDoctorRequest request);
    }
}
