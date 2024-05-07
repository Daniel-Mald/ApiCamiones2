using CamionesAPI.Models.Entities;

namespace CamionesAPI.Repositories
{
    public class ViajesRepository(Sistem21TestdbContext context) : GenericRepository<Viaje>(context)
    {
        public override Viaje? GetByID(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }
    }
}
