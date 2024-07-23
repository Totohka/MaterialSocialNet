using Goods.System.Social.Network.DAL.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Goods.System.Social.Network.DomainModel.Entities.Dashboard;
using Microsoft.Extensions.Logging;
using NLog;
namespace Goods.System.Social.Network.DAL.Repository.Realization
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly IDbContextFactory<DashboardContext> _contextFactory;
        private readonly ILogger<DashboardRepository> _logger;  
        public DashboardRepository(IDbContextFactory<DashboardContext> dbContextFactory, ILogger<DashboardRepository> logger)
        {
            _logger = logger;
            _contextFactory = dbContextFactory;
        }
        public async Task<List<Visit>> GetAllAsync()
        {
            _logger.LogTrace($"Вызван метод GetAllAsync");

            using var db = _contextFactory.CreateDbContext();
            return await db.Set<Visit>().ToListAsync();
        }
        public async void Create(Visit item)
        {
            _logger.LogTrace($"Вызван метод Create с параметрами: item: {item}");

            using var db = _contextFactory.CreateDbContext();
            await db.Set<Visit>().AddAsync(item);
            db.SaveChanges();
        }
    }
}