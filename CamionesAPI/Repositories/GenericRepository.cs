using CamionesAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CamionesAPI.Repositories
{
    public class GenericRepository<T>(Sistem21TestdbContext context) where T : class
    {
        public virtual DbSet<T> GetAll()
        {
            return context.Set<T>();
        }
        public virtual T? GetByID(int id)
        {
            return context.Find<T>(id);
        }
        public void Add(T entity)
        {
            context.Add(entity);
            context.SaveChanges();
        }
        public void Update(T entity)
        {
            context.Update(entity);
            context.SaveChanges();
        }
        public void Delete(T entity)
        {
            context.Remove(entity);
            context.SaveChanges();
        }
    }
}
