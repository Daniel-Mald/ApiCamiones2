using CamionesAPI.Models.DTOs.Unidad;
using CamionesAPI.Models.Entities;
using CamionesAPI.Models.Validators.Unidad;
using CamionesAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CamionesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize(Roles = "Administrador")]
    public class VehiculosController(UnidadRepository unidadRepos) : ControllerBase
    {
        #region CRUD
        #region Create
        [Authorize(Roles = "Administrador")]
        [HttpPost("/Vehiculos/Agregar")]
        public IActionResult Agregar(UnidadDTO u)
        {
            UnidadDTOValidator validator = new();
            var results = validator.Validate(u);
            if (results.IsValid)
            {
                //Buscar la unidad en la base de datos utilizando la placa como identificadores
                var anterior = unidadRepos.GetAll().FirstOrDefault(x => x.Placa == u.Placa);
                //Si la unidad ya existe en la base de datos
                if (anterior != null)
                {
                    //Dar de alta
                    anterior.IdDisponibilidad = 1;
                    unidadRepos.Update(anterior);
                }
                //Si la unidad no existe
                else
                {
                    Unidad unidad = new()
                    {
                        Id = 0,
                        Capacidad = u.Capacidad,
                        Placa = u.Placa,
                        FechaDeRegistro = DateOnly.FromDateTime(DateTime.UtcNow),
                        Modelo = u.Modelo,
                        IdDisponibilidad = 1 //Activo
                    };
                    //Se crea
                    unidadRepos.Add(unidad);
                }
                return Ok("Unidad Registrada");
            }
            return BadRequest(results.Errors);
        }
        #endregion
        #region Read
        [Authorize(Roles = "Administrador,Usuario")]
        [HttpGet("/Vehiculos")]
        public IActionResult Index()
        {
            var _data = unidadRepos.GetAll().Where(camion => camion.IdDisponibilidad == 1);
            if (_data != null)
            {
                return Ok(_data);
            }
            return NotFound("No hay unidades disponibles");
        }
        [Authorize(Roles = "Administrador,Usuario")]
        [HttpGet("/Vehiculo/{id}")]
        public IActionResult GetVehiculo(int id)
        {
            var _data = unidadRepos.GetByID(id);
            //cuando se hace el metodo Get verifica si la unidad existe y no esta dado de baja 
            if (_data != null && _data.IdDisponibilidad == 1)
            {
                //regresa la unidad si existe
                return Ok(_data);
            }
            //si no encuentra la unidad se enviara el mensaje
            return NotFound("La unidad no se encuentra disponible");
        }
        #endregion
        #region Update
        [Authorize(Roles = "Administrador")]
        [HttpPut("/Vehiculos/Editar")]
        public IActionResult Editar(UnidadDTO u)
        {
            UnidadDTOValidator validator = new();
            var results = validator.Validate(u);
            if (results.IsValid)
            {
                var anterior = unidadRepos.GetByID(u.Id);
                if (anterior == null)
                {
                    return NotFound("No existe la unidad");
                }
                anterior.Placa = u.Placa;
                anterior.Capacidad = u.Capacidad;
                anterior.Modelo = u.Modelo;
                unidadRepos.Update(anterior);
                return Ok("Se han realizado los cambios en la unidad.");
            }
            return BadRequest(results.Errors.Select(x => x.ErrorMessage));
        }
        #endregion
        #region Delete
        [Authorize(Roles = "Administrador")]
        [HttpDelete("/Vehiculos/Eliminar")]
        public IActionResult Eliminar(int id)
        {
            var unidad = unidadRepos.GetByID(id);
            if (unidad != null)
            {
                //Se da de baja logicamente
                unidad.IdDisponibilidad = 2; // Inactivo
                unidadRepos.Update(unidad);
                return Ok("Se ah eliminado la unidad");
            }
            return NotFound("No existe la unidad");
        }
        #endregion
        #endregion
    }
}
