using CamionesAPI.Models.DTOs.Gasto;
using CamionesAPI.Models.Entities;
using CamionesAPI.Models.Validators.Gasto;
using CamionesAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CamionesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Administrador,Usuario")]
    public class GastosController(GastoRepository _repos) : ControllerBase
    {
        #region Read
        [HttpGet("/Gastos")]
        public IActionResult GetGastos()
        {
            var _data = _repos.GetAll();
            return _data != null ? Ok(_data) : NotFound("No se han encontrado gastos");
        }

        [HttpGet("/Gasto/Viaje/{id:int}")]
        public IActionResult GetGastosByViaje(int id)
        {
            var _data = _repos.GetViajegastos(id).Select(x => x.IdGastoNavigation.Monto).OrderBy(g => g);
            return _data != null ? Ok(_data) : NotFound("No se encontraron gastos en el viaje");
        }
        #endregion
        #region Add
        [HttpPost("/Gasto/Agregar")]
        public IActionResult AddGasto(GastoDTO _dto)
        {
            //Checar si el viaje existe
            GastoDTOValidator _validator = new();
            var resultado = _validator.Validate(_dto);
            if (resultado.IsValid)
            {
                Gasto g = new()
                {
                    Nombre = _dto.Nombre,
                    Monto = _dto.Monto
                };
                _repos.Add(g);
                Viajegasto v = new()
                {
                    IdGasto = g.Id,
                    IdViaje = _dto.IdViaje
                };
                _repos.AddViajeGasto(v);
                return Ok("Se ah agregado el gasto al viaje");
            }
            return BadRequest(resultado.Errors.Select(x => x.ErrorMessage));
        }
        #endregion
        #region Update
        [HttpPut("/Gasto/Editar")]
        public IActionResult Update(GastoDTO _dto)
        {
            GastoDTOValidator _validator = new();
            var _result = _validator.Validate(_dto);
            if (_result.IsValid)
            {
                var _gastoAnterior = _repos.GetByID(_dto.IdGasto);
                if (_gastoAnterior != null)
                {
                    _gastoAnterior.Nombre = _dto.Nombre;
                    _gastoAnterior.Monto = _dto.Monto;
                    _repos.Update(_gastoAnterior);
                    return Ok("Se ah editado el gasto correctamente");
                }
                return NotFound("No se ah encontrado el gasto solicitado");
            }
            return BadRequest(_result.Errors.Select(x => x.ErrorMessage));
        }
        #endregion
        #region Delete
        [HttpDelete("/Gasto/Eliminar")]
        public IActionResult Delete(GastoDTO dto)
        {
            var gasto = _repos.GetByID(dto.IdGasto);
            var viajegasto = _repos.GetViajegasto(dto.IdGasto, dto.IdViaje);
            if (gasto != null && viajegasto != null)
            {
                _repos.DeleteViajeGasto(viajegasto);
                _repos.Delete(gasto);
            }
            return Ok("Se ah eliminado el gasto");
        }
        #endregion
    }
}
