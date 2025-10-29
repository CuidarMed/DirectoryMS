using Application.DTOs.Doctors;

namespace Application.Interfaces
{
    public interface IUpdateDoctorService
    {
        Task<UpdateDoctorResponse> UpdateDoctorAsync(long id, UpdateDoctorRequest request);
    }
}
