using CamionesAPI.Models.DTOs.Chofer;
using FluentValidation;
using System.Text.RegularExpressions;

namespace CamionesAPI.Models.Validators.Chofer
{
    public class ChoferDTOValidator : AbstractValidator<ChoferDTO>
    {
        // Patrón de expresión regular para números de teléfono
        private readonly string Patron = @"^(\+\d{1,2}\s?)?\d{3}[\s.-]?\d{3}[\s.-]?\d{4}$";
        //"1234567890",         Válido
        //"123.456.7890",       No válido
        //"123-456-7890",       Válido
        //"(123) 456-7890",     Válido
        //"123 456 7890",       Válido
        //"+1 123 456 7890"     Válido

        public ChoferDTOValidator()
        {
            RuleFor(x => x.Telefono)
                .Must(IsPhoneNumber).WithMessage("Ingresa un numero de 10 digitos")
                .NotEmpty().NotNull().WithMessage("El telefono no debe ser nulo o vacio");
            RuleFor(x => x.IdentificadorChofer)
                .NotEmpty().NotNull().WithMessage("El identificador no debe ser nulo o vacio");
            RuleFor(x => x.Sueldo)
                .LessThan(800).WithMessage("Ingrese un sueldo valido")
                .NotEmpty().NotNull().WithMessage("El sueldo no debe ser nulo o vacio");
        }

        // Verificar si el número coincide con el patrón
        public bool IsPhoneNumber(string numero) => Regex.IsMatch(numero, Patron);
    }
}
