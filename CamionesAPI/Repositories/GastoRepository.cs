using CamionesAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CamionesAPI.Repositories
{
    public class GastoRepository(Sistem21TestdbContext context) : GenericRepository<Gasto>(context)
    {
        readonly Sistem21TestdbContext _context = context;
        public override DbSet<Gasto> GetAll()
        {
            return _context.Gasto;
        }
        //public IEnumerable<Gasto> GetGastoPorViaje(int id)
        //{
        //    return _context.Gasto.Include(x=>x.Viajegasto).OrderBy(x=>x.Viajegasto.;
        //}
        public IEnumerable<Viajegasto> GetViajegastos(int id)
        {
            return _context.Viajegasto.Include(x => x.IdGastoNavigation).Where(x => x.IdViaje == id);
        }
        public void AddViajeGasto(Viajegasto v)
        {
            _context.Viajegasto.Add(v);
            _context.SaveChanges();
        }
        public Viajegasto? GetViajegasto(int IdGasto, int IdViaje)
        {
            return _context.Viajegasto.Where(x => x.IdViaje == IdViaje && x.IdGasto == IdGasto).FirstOrDefault();
        }
        public void DeleteViajeGasto(Viajegasto v)
        {
            _context.Remove(v);
            _context.SaveChanges();
        }
    }
}
