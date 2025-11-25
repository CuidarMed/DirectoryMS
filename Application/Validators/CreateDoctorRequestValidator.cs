using Application.DTOs.Doctors;
using FluentValidation;

namespace Application.Validators
{
    public class CreateDoctorRequestValidator : AbstractValidator<CreateDoctorRequest>
    {
        private static readonly string[] ValidSpecialties = { "Neurologo", "Pediatra", "Clinico", "Cardiologo", "Cirujano", "Dermatologo", "Psiquiatra" };

        public CreateDoctorRequestValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("El nombre es obligatorio.")
                .MaximumLength(100).WithMessage("El nombre no puede exceder 100 caracteres.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("El apellido es obligatorio.")
                .MaximumLength(100).WithMessage("El apellido no puede exceder 100 caracteres.");

            RuleFor(x => x.UserId)
                .GreaterThan(0).WithMessage("El UserId debe ser mayor que 0.");

            RuleFor(x => x.Specialty)
                .NotEmpty().WithMessage("La especialidad es obligatoria.")
                .Must(specialty => ValidSpecialties.Contains(specialty))
                .WithMessage($"La especialidad debe ser una de las siguientes: {string.Join(", ", ValidSpecialties)}.");

            RuleFor(x => x.LicenseNumber)
                .MaximumLength(50).WithMessage("La matrícula no puede exceder 50 caracteres.");

            RuleFor(x => x.Biography)
                .MaximumLength(1000).WithMessage("La biografía no puede exceder 1000 caracteres.");
        }
    }
}

