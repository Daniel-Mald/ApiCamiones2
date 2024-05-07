using CamionesAPI.Models.Entities;

namespace CamionesAPI.Repositories
{
    public class UnidadRepository(Sistem21TestdbContext context) : GenericRepository<Unidad>(context)
    {
        public Unidad? GetUnidadById(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id && x.IdDisponibilidad == 1);
        }
    }
}
