using CamionesAPI.Models.Entities;

namespace CamionesAPI.Repositories
{
    public class ChoferRepository(Sistem21TestdbContext context) : GenericRepository<Chofer>(context)
    {
        public override Chofer? GetByID(int id) => GetAll().FirstOrDefault(x => x.Id == id);
        public Chofer? GetChofer(string identificador) => GetAll().FirstOrDefault(x => x.IdentificadorChofer == identificador);
    }
}
