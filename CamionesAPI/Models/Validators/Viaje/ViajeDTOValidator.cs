using CamionesAPI.Models.DTOs.Viaje;
using FluentValidation;

namespace CamionesAPI.Models.Validators.Viaje
{
    public class ViajeDTOValidator : AbstractValidator<ViajeDTO>
    {
        public ViajeDTOValidator()
        {
            RuleFor(x => x.NombreCliente).NotNull().NotEmpty()
                  .WithMessage("Ingrese el nombre del cliente");
            RuleFor(x => x.Origen).NotNull().NotEmpty()
                .WithMessage("Ingrese el origen del viaje");
            RuleFor(x => x.Destino).NotNull().NotEmpty()
                .WithMessage("Ingrese un destino del viaje");
            RuleFor(x => x.FechaApagar).Must(EsFechaValida)
                .WithMessage("Ingrese una fecha posterior al dia de hoy");
            RuleFor(x => x.GananciaMonetaria)
                .GreaterThan(0).NotEmpty().NotNull().WithMessage("Ingresa las ganancias del viaje");
            RuleFor(x => x.TipoViaje).Must(EsTipoViaje)
                   .WithMessage("Seleccione el tipo de viaje");
            RuleFor(x => x.NumeroViaje).NotNull().NotEmpty()
                .WithMessage("El número de viaje no puede ser núlo o vacio");
            RuleFor(x => x.Chofer)
                //Verifica que no sea null o ""
                .NotEmpty().NotNull()
                .WithMessage("Seleccione un chofer para el viaje");
            RuleFor(x => x.Unidad)
                .NotEmpty().NotNull()
                .WithMessage("Seleccione la unidad para el viaje");
        }

        //Tipos de viaje validos: 1.- sencillo,2.- Redondo,3.- Retorno
        private static bool EsTipoViaje(int tipo) => tipo == 1 || tipo == 2 || tipo == 3;

        //regresara true si la fecha dada es mayor o igual a la fecha actual
        private bool EsFechaValida(DateOnly fecha) => fecha >= DateOnly.FromDateTime(DateTime.UtcNow);
    }
}
