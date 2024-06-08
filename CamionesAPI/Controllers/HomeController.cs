using CamionesAPI.Helpers;
using CamionesAPI.Models.DTOs.User;
using CamionesAPI.Models.Entities;
using CamionesAPI.Models.Validators.User;
using CamionesAPI.Repositories;
using CamionesAPI.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CamionesAPI.Controllers
{
    [Route("api/[controller]")] 
    [ApiController]
    public class HomeController : ControllerBase
    {
        #region Propiedades y repositorios
        public IConfiguration Configuration { get; }
        public UsuariosRepository Repositorio { get; }
        public JWT? JWTConfiguration { get; }
        #endregion
        public HomeController(Sistem21TestdbContext context, IConfiguration configuration)
        {
            Configuration = configuration;
            Repositorio = new(context);
            JWTConfiguration = Configuration.GetSection("JWT")?.Get<JWT>();
        }
        #region Login
        [HttpPost("/Login")]
        public IActionResult Login(LoginDTO login)
        {
            login.Contraseña = Encriptacion.CalculateSHA256(login.Contraseña);

            //Al recibir el LoginDTO es necesario Encriptar la contraseña
            //para compararla con la registrada en la base de datos
            var user = Repositorio.GetUsuario(login.Correo, login.Contraseña);

            if (user == null) //El Usuario no existe
            {
                return Unauthorized("Correo o contraseña equivocados");
            }

            //Si el usuario existe verifnecesario seguir los siguentes pasos:
            //1.- Crear claimsicara que la configuracion exista
            else if (JWTConfiguration != null)
            {
                //Para utilizar JWT es 
                List<Claim> claims =
                [
                    new Claim("Id", user.Id.ToString()),
                    new Claim("Name",user.Nombre),
                    new Claim(ClaimTypes.Email, user.Correo ?? ""),
                    new Claim(ClaimTypes.Role, user.IdRolNavigation.Nombre)
                ];

                //2.- Crear token Descriptor, es necesario para la creacion del token
                SecurityTokenDescriptor TokenDescriptor = new()
                {
                    //Emisor(quien envia el token)
                    Issuer = JWTConfiguration.Issuer,
                    //Receptor(quien recibe el token)                 
                    Audience = JWTConfiguration.Audiance,
                    //Fecha para cambiar el token si es que expira
                    IssuedAt = DateTime.UtcNow,
                    //El token sera válido por 5 horas
                    Expires = DateTime.UtcNow.AddHours(5),
                    //Algoritmo de cifrado
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTConfiguration.Key)),
                    //Se utilizara Sha256 como algoritmo de encriptación    
                    SecurityAlgorithms.HmacSha256),
                    Subject = new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme)
                };

                // Para utilizar el token es necesario utilizar un manejador(handler)
                JwtSecurityTokenHandler handler = new();

                //3.- Creacion del Token, Creamos el token utilizando el TokenDescription
                var token = handler.CreateToken(TokenDescriptor);

                //4.- Regresar el token
                return Ok(handler.WriteToken(token));
            }
            //En caso de que los datos de la configuracion sean eliminados ya sea intencional o accidentalmente
            //se mostrara lo siguente:
            return Conflict("Servidor no configurado");
        }
        #endregion
        #region Registro
        [HttpPost("/Register")]
        public IActionResult Register(RegisterDTO registro)
        {
            RegisterDTOValidator validator = new();
            var result = validator.Validate(registro);
            if (result.IsValid)
            {
                //Al registrar un usuario es necesario encriptar la contraseña para tener seguridad en la base de datos
                registro.Contraseña = Encriptacion.CalculateSHA256(registro.Contraseña);
                //necesitamos verificar si existe un usuario con el mismo nombre
                var anterior = Repositorio.GetUsuarioByName(registro.Nombre);
                //Esta validacion se hace porque un helper no deberia de hacer una peticion a la base de datos
                //Si no existe un usuario con el mismo nombre
                if (anterior == null)
                {
                    //creamos un usuario
                    Usuario user = new()
                    {
                        //para evitar conflictos al insertar datos a la base de datos el id se asigna en 0
                        Id = 0,
                        Nombre = registro.Nombre,
                        Correo = registro.Correo,
                        //Encriptamos la contraseña para que se guarde en la base de datos
                        Contraseña = registro.Contraseña,
                        //El usuario estara activo cuando se registe, cambiara a 2 si esta dado de baja
                        IdDisponibilidad = 1,
                        //El rol seleccionado sera asignado al usuario
                        IdRol = registro.IdRol
                    };
                    //Agregamos el usuario creado a la base de datos
                    Repositorio.Add(user);
                    return Ok($"Se ah registrado el usuario {user.Nombre}");
                }
                //si existe un usuario previamente, se agregara un error a la lista de errores
                result.Errors.Add(new FluentValidation.Results.ValidationFailure()
                {
                    ErrorMessage = "Ya se ah registrado un usuario con el mismo nombre"
                });
            }
            //si no esta repetido pero hay otros errores se mostraran en el body
            return BadRequest(result.Errors);
        }
        #endregion
        #region Cambio de Contraseña
        [Authorize(Roles = "Administrador,Usuario")]
        [HttpPut("/Password/Change")]
        public IActionResult ChangePassword(UserPasswordDTO dto)
        {
            //si la contraseña esta vacia
            if (string.IsNullOrWhiteSpace(dto.Contraseña))
                return BadRequest("Ingresa una contraseña");
            //si la contraseña es la misma
            var anterior = Repositorio.GetByID(dto.Id);
            dto.Contraseña = Encriptacion.CalculateSHA256(dto.Contraseña);
            if (anterior != null)
            {
                if (anterior.Contraseña == dto.Contraseña)
                {
                    return Conflict("No puedes ingresar la misma contraseña");
                }
                anterior.Contraseña = dto.Contraseña;
                Repositorio.Update(anterior);
                return Ok("Se ah cambiado la contraseña");
            }
            return BadRequest("No se han realizado los cambios");
        }
        #endregion
        #region Edición de Datos de Usuario
        [Authorize(Roles = "Administrador,Usuario")]
        [HttpPut("/Editar")]
        public IActionResult Update(UserDTO dto)
        {
            UserDTOValidator validator = new();
            var result = validator.Validate(dto);
            if (result.IsValid)
            {
                var anterior = Repositorio.GetByID(dto.Id);
                if (anterior != null)
                {
                    anterior.Nombre = dto.Nombre;
                    anterior.Correo = dto.Correo;
                    anterior.IdRol = dto.IdRol;
                    anterior.IdDisponibilidad = dto.IdDisponibilidad;
                    Repositorio.Update(anterior);
                    return Ok("Se han realizado los cambios");
                }
            }
            return BadRequest(result.Errors);
        }
        #endregion
    }
}
