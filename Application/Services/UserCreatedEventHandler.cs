using Application.Contracts.Events;
using Application.Interfaces;
using Domain.Entities; // si están en otro namespace

namespace Application.Services
{
    public class UserCreatedEventHandler : IUserCreatedEventHandler
    {
        private readonly IPatientCommand _patientCommand;
        private readonly IPatientQuery _patientQuery;
        private readonly IDoctorCommand _doctorCommand;
        private readonly IDoctorQuery _doctorQuery;

        public UserCreatedEventHandler
        (IPatientCommand patientCommand, IPatientQuery patientQuery, IDoctorCommand doctorCommand, IDoctorQuery doctorQuery)
        {
            _patientCommand = patientCommand;
            _patientQuery = patientQuery;
            _doctorCommand = doctorCommand;
            _doctorQuery = doctorQuery;
        }
        public async Task HandleAsync(UserCreatedEvent @event, CancellationToken cancellationToken = default)
        {
            var role = @event.Role?.Trim();
            if (string.Equals(role, "Patient", StringComparison.OrdinalIgnoreCase))
            {
                await HandlePatienAsync(@event, cancellationToken);
            }
            else if (string.Equals(role, "Doctor", StringComparison.OrdinalIgnoreCase))
            {
                await HandleDoctorAsync(@event);
            }
        }
        private async Task HandlePatienAsync(UserCreatedEvent @event, CancellationToken cancellationToken = default)
        {
            var alreadyExists = await _patientQuery.ExistsByUserIdAsync(@event.UserId);
            if (alreadyExists) return;
            var patient = new Patient
            {
                UserId = @event.UserId,
                Name = @event.FirstName,
                LastName = @event.LastName,
                Dni = int.Parse(@event.Dni),
                Phone = @event.Phone,
                DateOfBirth = (DateOnly)@event.DateOfBirth,
                Adress = @event.Adress,
                HealthPlan = @event.HealthPlan,
                MembershipNumber = @event.MembershipNumber
            };

            await _patientCommand.addPatient(patient);
        }
        private async Task HandleDoctorAsync(UserCreatedEvent @event)
        {
            var alreadyExists = await _doctorQuery.ExistsByUserIdAsync(@event.UserId);
            if (alreadyExists) return;

            var doctor = new Doctor
            {
                UserId = @event.UserId,
                FirstName = @event.FirstName,
                LastName = @event.LastName,
                Phone = @event.Phone,
                LicenseNumber = @event.LicenseNumber,
                Biography = @event.Biography,
                Specialty = @event.Specialty
            };

            await _doctorCommand.CreateAsync(doctor);
        }
    }
}
