using CamionesAPI.Models.DTOs.User;
using FluentValidation;

namespace CamionesAPI.Models.Validators.User
{
    public class UserDTOValidator : AbstractValidator<UserDTO>
    {
        public UserDTOValidator()
        {
            RuleFor(x => x.Correo).EmailAddress().WithMessage("Ingresa un correo electrónico")
                .NotNull().NotEmpty().WithMessage("El correo no debe ser nulo o vacio");
            RuleFor(x => x.Nombre).NotNull().NotEmpty().WithMessage("El nombre no debe ser nulo o vacio");
            RuleFor(x => x.IdRol).Must(IsRol).WithMessage("Selecciona un rol");
        }

        //Verifica si el IdRol es 1 o 2(administrador o usuario)
        private bool IsRol(int rol) => (rol == 1 || rol == 2);
    }
}