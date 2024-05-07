using CamionesAPI.Models.DTOs.Viaje;
using CamionesAPI.Models.Entities;
using CamionesAPI.Models.Validators.Viaje;
using CamionesAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace CamionesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViajesController(ViajesRepository repository, GenericRepository<Vwvistageneral> viewRepository) : ControllerBase
    {
        #region CRUD
        #region Create
        [HttpPost("/Viaje/Crear")]
        public IActionResult Crear(ViajeDTO dto)
        {
            ViajeDTOValidator validador = new();
            var result = validador.Validate(dto);

            if (result.IsValid)
            {
                Viaje viaje = new()
                {
                    //Evita problemas al crear objetos en la base de datos
                    Id = 0,
                    Fecha = DateOnly.FromDateTime(DateTime.UtcNow),
                    //Obtiene el numero de la semana en la que se realizo el viaje
                    Semana = (sbyte)CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(
                                DateTime.UtcNow,
                                CalendarWeekRule.FirstFourDayWeek
                                //Primero dia de la semana
                                , DayOfWeek.Monday),
                    IdFactura = dto.EstatusFactura,
                    Observaciones = dto.Observaciones ?? "",
                    ChoferId = dto.Chofer,
                    IdDisponibilidad = 1,
                    IdTipoViaje = dto.TipoViaje,
                    UnidadId = dto.Unidad,
                    NombreCliente = dto.NombreCliente ?? "",
                    FechaApagar = dto.FechaApagar,
                    Origen = dto.Origen,
                    Destino = dto.Destino,
                    GananciaMonetaria = dto.GananciaMonetaria,
                    NumeroViaje = dto.NumeroViaje
                };
                return Ok("Se ah agregado el viaje");
            }
            return BadRequest(result.Errors.Select(x => x.ErrorMessage));
        }
        #endregion
        #region Read
        #region Viajes
        [HttpGet("/Viajes/{fecha:datetime}")]
        public IActionResult GetViajes(DateTime fecha)
        {
            var data = repository.GetAll().Where(x => x.IdDisponibilidad == 1 &&
            x.Fecha >= DateOnly.FromDateTime(fecha))
                .OrderBy(x => x.NombreCliente).Select(viaje => new ViajeDTO
                {
                    //Evita problemas al crear objetos en la base de datos
                    Id = viaje.Id,
                    Fecha = viaje.Fecha,
                    //Obtiene el numero de la semana en la que se realizo el viaje
                    Semana = viaje.Semana,
                    EstatusFactura = viaje.IdFactura,
                    Observaciones = viaje.Observaciones ?? "",
                    Chofer = viaje.ChoferId,
                    TipoViaje = viaje.IdTipoViaje,
                    Unidad = viaje.UnidadId,
                    NombreCliente = viaje.NombreCliente,
                    FechaApagar = viaje.FechaApagar,
                    Origen = viaje.Origen,
                    Destino = viaje.Destino,
                    GananciaMonetaria = viaje.GananciaMonetaria,
                    NumeroViaje = viaje.NumeroViaje,
                });
            if (data != null)
                return Ok(data);
            return NotFound("No hay registrados");
        }

        [HttpGet("/Viaje/{id}")]
        public IActionResult GetViaje(int id)
        {
            var data = repository.GetByID(id);
            if (data != null && data.IdDisponibilidad == 1)
            {
                return Ok(data);
            }
            return BadRequest("No existe el viaje solicitado");
        }
        #endregion
        #region Vista General
        [HttpGet("/VistaGeneral")]
        public IActionResult VistaGeneral()
        {
            var data = viewRepository.GetAll();
            return Ok(data);
        }

        #endregion
        #endregion
        #region Update
        [HttpPut("/Viaje/Editar")]
        public IActionResult Editar(ViajeDTO dto)
        {
            ViajeDTOValidator validador = new();
            var result = validador.Validate(dto);
            if (result.IsValid)
            {
                var anterior = repository.GetByID(dto.Id);
                if (anterior != null && anterior.IdDisponibilidad == 1)
                {
                    //La fecha se cambiara automaicamente a la fecha en la que se realizo la edicion
                    anterior.Fecha = DateOnly.FromDateTime(DateTime.UtcNow);
                    //La semana se cambiara automaticamente a la semana en la que se realizo la edicion
                    anterior.Semana = (sbyte)CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(
                                    DateTime.UtcNow,
                                    CalendarWeekRule.FirstFourDayWeek
                                    //Primero dia de la semana
                                    , DayOfWeek.Monday);
                    anterior.IdFactura = dto.EstatusFactura;
                    anterior.Observaciones = dto.Observaciones ?? "";
                    anterior.ChoferId = dto.Chofer;
                    anterior.IdTipoViaje = dto.TipoViaje;
                    anterior.UnidadId = dto.Unidad;
                    anterior.NombreCliente = dto.NombreCliente;
                    anterior.FechaApagar = dto.FechaApagar;
                    anterior.Origen = dto.Origen;
                    anterior.Destino = dto.Destino;
                    anterior.GananciaMonetaria = dto.GananciaMonetaria;
                    anterior.NumeroViaje = dto.NumeroViaje;

                    return Ok("Se ah editado el viaje");
                };
                return NotFound("No se ah encontrado el viaje");
            }
            return BadRequest(result.Errors.Select(x => x.ErrorMessage));
        }
        #endregion
        #region Delete
        [HttpDelete("/Viaje/Eliminar/{id:int}")]
        public IActionResult Eliminar(int id)
        {
            var viaje = repository.GetByID(id);
            if (viaje != null && viaje.IdDisponibilidad == 1)
            {
                //Eliminacion logica
                viaje.IdDisponibilidad = 2;
                repository.Update(viaje);
                return Ok("Se ah eliminado el viaje correctamente");
            }
            return NotFound("No se ah encontrado el viaje que desea eliminar");
        }
        #endregion
        #endregion
    }
}
