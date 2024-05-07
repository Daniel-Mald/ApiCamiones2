using CamionesAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CamionesAPI.Repositories
{
    public class UsuariosRepository(Sistem21TestdbContext context) : GenericRepository<Usuario>(context)
    {
        public Usuario? GetUsuario(string correo, string contraseña)
        {
            return GetAll().Include(x => x.IdRolNavigation).FirstOrDefault(x => x.Correo == correo && x.Contraseña == contraseña && x.IdDisponibilidad == 1);
        }
        public Usuario? GetUsuarioByName(string name)
        {
            return GetAll().FirstOrDefault(x => x.Nombre == name);
        }
        public override Usuario? GetByID(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }
    }
}
