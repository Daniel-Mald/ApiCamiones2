using CamionesAPI.Models.DTOs.Chofer;
using CamionesAPI.Models.Entities;
using CamionesAPI.Models.Validators.Chofer;
using CamionesAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CamionesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Administrador,Usuario")]
    public class ChoferesController(ChoferRepository repository) : ControllerBase
    {
        #region CRUD
        #region Read
        [HttpGet("/Choferes")]//Get todos lo cheferes
        public IActionResult Index()
        {
            var data = repository.GetAll().Where(x => x.IdDisponibilidad == 1).OrderBy(x => x.Nombre);
            return Ok(data);
        }

        [HttpGet("/Choferes/{Id}")]
        public IActionResult GetChofer(int Id)
        {
            var data = repository.GetByID(Id);
            if (data != null && data.IdDisponibilidad == 1)
                return Ok(data);
            return NotFound("No se ah encontrado el chofer");
        }
        #endregion
        #region Create
        [HttpPost("/Choferes/Agregar")]
        public IActionResult Agregar(ChoferDTO c)
        {
            ChoferDTOValidator validador = new();
            var resultado = validador.Validate(c);
            if (resultado.IsValid)
            {
                var _existente = repository.GetChofer(c.IdentificadorChofer);
                if (_existente != null)
                {
                    return BadRequest("Ya existe un chofer con el mismo Identificador");
                }
                Chofer _newChofer = new()
                {
                    Nombre = c.Nombre,
                    Sueldo = c.Sueldo,
                    IdDisponibilidad = 1,
                    Telefono = c.Telefono,
                    IdentificadorChofer = c.IdentificadorChofer
                };
                repository.Add(_newChofer);
                return Ok("Chofer registrado");
            }
            return BadRequest(resultado.Errors.Select(x => x.ErrorMessage));
        }
        #endregion
        #region Update
        [HttpPut("/Choferes/Editar")]
        public IActionResult Update(ChoferDTO c)
        {
            ChoferDTOValidator validador = new();
            var resultado = validador.Validate(c);
            if (resultado.IsValid)
            {
                var _ex = repository.GetByID(c.Id);
                if (_ex == null || _ex.IdDisponibilidad == 2)
                {
                    return NotFound("No existe el chofer");
                }
                else
                {
                    _ex.Sueldo = c.Sueldo;
                    _ex.IdentificadorChofer = c.IdentificadorChofer;
                    _ex.Nombre = c.Nombre;
                    _ex.Telefono = c.Telefono;

                    repository.Update(_ex);
                    return Ok("Se han realizado los cambios del chofer.");
                }
            }
            return BadRequest(resultado.Errors.Select(x => x.ErrorMessage));

        }
        #endregion
        #region Delete
        [HttpDelete("/Choferes/Eliminar/{Id:int}")]
        public IActionResult Eliminar(int Id)
        {
            var _existente = repository.GetByID(Id);
            if (_existente != null && _existente.IdDisponibilidad == 1)
            {
                _existente.IdDisponibilidad = 2;
                repository.Update(_existente);
                return Ok("Se ha eliminado el chofer");
            }
            return NotFound("No se ha encontrado ese chofer");
        }
        #endregion
        #endregion
    }
}
