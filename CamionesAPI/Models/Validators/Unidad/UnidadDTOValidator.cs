using CamionesAPI.Models.DTOs.Unidad;
using FluentValidation;

namespace CamionesAPI.Models.Validators.Unidad
{
    public class UnidadDTOValidator : AbstractValidator<UnidadDTO>
    {
        public UnidadDTOValidator()
        {
            RuleFor(x => x.Modelo).NotEmpty().NotNull().WithMessage("El modelo no debe ser nulo o vacio");
            RuleFor(x => x.Placa).NotEmpty().NotNull().WithMessage("La placa no debe ser nulo o vacia");
            RuleFor(x => x.Capacidad).NotEmpty().NotNull().WithMessage("La capacidad no debe ser nula o vacia");
        }
    }
}
