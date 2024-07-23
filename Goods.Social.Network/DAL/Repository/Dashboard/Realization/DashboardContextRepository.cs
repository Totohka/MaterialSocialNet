using Goods.System.Social.Network.DAL.Repository.Interface;
using Goods.System.Social.Network.DAL;
using Microsoft.EntityFrameworkCore;

namespace Goods.System.Social.Network.DAL.Repository.Realization
{

    public class DashboardContextRepository<T> : IRepository<T> where T : class
    {
        private readonly IDbContextFactory<DashboardContext> _contextFactory;
        public DashboardContextRepository(IDbContextFactory<DashboardContext> dbContextFactory)
        {
            _contextFactory = dbContextFactory;
        }
        public async Task DeleteAsync(int id)
        {
            using var db = _contextFactory.CreateDbContext();
            T item = await GetAsync(id);
            db.Remove(item);
            db.SaveChanges();
        }
        public async Task<T> GetAsync(int id)
        {
            using var db = _contextFactory.CreateDbContext();
            return await db.Set<T>().FindAsync(id);
        }
        public async Task<List<T>> GetAllAsync()
        {
            using var db = _contextFactory.CreateDbContext();
            return await db.Set<T>().ToListAsync();
        }
        public void Create(T item)
        {
            using var db = _contextFactory.CreateDbContext();
            db.Set<T>().Add(item);
            db.SaveChanges();
        }

        public void Update(T item)
        {
            using var db = _contextFactory.CreateDbContext();
            db.Update(item);
            db.SaveChanges();
        }
    }
}