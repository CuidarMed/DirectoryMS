using Application.DTOs.Doctors;
using Application.Interfaces;

namespace Application.Services
{
    public class SearchDoctorService : ISearchDoctorService
    {
        private readonly IDoctorQuery _doctorQuery;

        public SearchDoctorService(IDoctorQuery doctorQuery)
        {
            _doctorQuery = doctorQuery;
        }

        public async Task<DoctorResponse?> GetByIdAsync(long id)
        {
            var doctor = await _doctorQuery.GetByIdAsync(id);

            if (doctor == null)
                return null;

            return new DoctorResponse
            {
                DoctorId = doctor.DoctorId,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                LicenseNumber = doctor.LicenseNumber,
                Biography = doctor.Biography,
                Specialty = doctor.Specialty,
                UserId = doctor.UserId
            };
        }

        public async Task<List<DoctorResponse>> GetAllAsync()
        {
            var doctors = await _doctorQuery.GetAllAsync();

            return doctors.Select(d => new DoctorResponse
            {
                DoctorId = d.DoctorId,
                FirstName = d.FirstName,
                LastName = d.LastName,
                LicenseNumber = d.LicenseNumber,
                Biography = d.Biography,
                Specialty = d.Specialty,
                UserId = d.UserId
            }).ToList();
        }

        public async Task<DoctorResponse?> GetByUserIdAsync(long userId)
        {
            var doctor = await _doctorQuery.GetByUserIdAsync(userId);

            if (doctor == null)
                return null;

            return new DoctorResponse
            {
                DoctorId = doctor.DoctorId,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                LicenseNumber = doctor.LicenseNumber,
                Biography = doctor.Biography,
                Specialty = doctor.Specialty,
                UserId = doctor.UserId
            };
        }
    }
}
