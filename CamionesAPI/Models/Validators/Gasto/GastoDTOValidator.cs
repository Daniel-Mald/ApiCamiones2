using CamionesAPI.Models.DTOs.Gasto;
using FluentValidation;

namespace CamionesAPI.Models.Validators.Gasto
{
    public class GastoDTOValidator : AbstractValidator<GastoDTO>
    {
        public GastoDTOValidator()
        {
            RuleFor(x => x.Monto).Must(MontoValido).WithMessage("Ingrese un valor de monto valido");
            RuleFor(x => x.Nombre).NotEmpty().NotNull().WithMessage("Ingrese un nombre para el gasto.");
        }
        bool MontoValido(decimal n) => (n > 0);
    }
}
