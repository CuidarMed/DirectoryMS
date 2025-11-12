using Application.DTOs.Doctors;
using FluentValidation;

namespace Application.Validators
{
    public class UpdateDoctorRequestValidator : AbstractValidator<UpdateDoctorRequest>
    {
        private static readonly string[] ValidSpecialties = { "Neurologo", "Pediatra", "Clinico", "Cardiologo", "Cirujano", "Dermatologo", "Psiquiatra" };

        public UpdateDoctorRequestValidator()
        {
            RuleFor(x => x.FirstName)
                .MaximumLength(100).WithMessage("El nombre no puede exceder 100 caracteres.")
                .When(x => !string.IsNullOrEmpty(x.FirstName));

            RuleFor(x => x.LastName)
                .MaximumLength(100).WithMessage("El apellido no puede exceder 100 caracteres.")
                .When(x => !string.IsNullOrEmpty(x.LastName));

            RuleFor(x => x.Specialty)
                .Must(specialty => string.IsNullOrEmpty(specialty) || ValidSpecialties.Contains(specialty))
                .WithMessage($"Si se proporciona, la especialidad debe ser una de las siguientes: {string.Join(", ", ValidSpecialties)}.")
                .When(x => !string.IsNullOrEmpty(x.Specialty));

            RuleFor(x => x.LicenseNumber)
                .MaximumLength(50).WithMessage("La matrícula no puede exceder 50 caracteres.")
                .When(x => !string.IsNullOrEmpty(x.LicenseNumber));

            RuleFor(x => x.Biography)
                .MaximumLength(1000).WithMessage("La biografía no puede exceder 1000 caracteres.")
                .When(x => !string.IsNullOrEmpty(x.Biography));
        }
    }
}

