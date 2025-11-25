using Application.DTOs.Patients;
using FluentValidation;

namespace Application.Validators
{
    public class CreatePatientRequestValidator : AbstractValidator<CreatePatientRequest>
    {
        public CreatePatientRequestValidator()
        {
            RuleFor(x => x.Dni)
                .NotNull().WithMessage("El DNI es obligatorio.")
                .GreaterThan(0).WithMessage("El DNI debe ser mayor que 0.");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("El nombre es obligatorio.")
                .MaximumLength(100).WithMessage("El nombre no puede exceder 100 caracteres.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("El apellido es obligatorio.")
                .MaximumLength(100).WithMessage("El apellido no puede exceder 100 caracteres.");

            RuleFor(x => x.Adress)
                .MaximumLength(200).WithMessage("La dirección no puede exceder 200 caracteres.")
                .When(x => !string.IsNullOrWhiteSpace(x.Adress));

            RuleFor(x => x.Phone)
                .MaximumLength(20).WithMessage("El teléfono no puede exceder 20 caracteres.")
                .When(x => !string.IsNullOrWhiteSpace(x.Phone));

            RuleFor(x => x.DateOfBirth)
                .Must(date => date.Year > 1900) // regla suave para validar fechas imposibles
                .WithMessage("La fecha de nacimiento no es válida.");

            RuleFor(x => x.HealthPlan)
                .MaximumLength(100).WithMessage("La obra social no puede exceder 100 caracteres.")
                .When(x => !string.IsNullOrWhiteSpace(x.HealthPlan));

            RuleFor(x => x.MembershipNumber)
                .MaximumLength(50).WithMessage("El número de afiliado no puede exceder 50 caracteres.")
                .When(x => !string.IsNullOrWhiteSpace(x.MembershipNumber));

            RuleFor(x => x.UserId)
                .GreaterThan(0).WithMessage("El UserId debe ser mayor que 0.");

            RuleFor(x => x).NotNull().WithMessage("El body no puede ser nulo");
        }
    }
}
