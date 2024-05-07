using CamionesAPI.Models.DTOs.User;
using FluentValidation;

namespace CamionesAPI.Models.Validators.User
{
    public class RegisterDTOValidator : AbstractValidator<RegisterDTO>
    {
        public RegisterDTOValidator()
        {
            RuleFor(x => x.Correo)
                .EmailAddress().WithMessage("Ingresa un correo electrónico")
                .NotNull().NotEmpty().WithMessage("El correo no debe ser nulo o vacio");
            RuleFor(x => x.Contraseña)
                .NotNull().NotEmpty().WithMessage("La contraseña no deber nula o vacia");
            RuleFor(x => x.Nombre)
                .NotNull().NotEmpty().WithMessage("El nombre no puede ser nulo o vacio");
        }
    }
}
