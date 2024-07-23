using Goods.System.Social.Network.DAL.Repository.Interface;
using Goods.System.Social.Network.DAL;
using Goods.System.Social.Network.DomainModel.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NLog;
namespace Goods.System.Social.Network.DAL.Repository.Realization
{

    public class SettingNotificationRepository : ISettingNotificationRepository
    {
        private readonly IDbContextFactory<UserContext> _contextFactory;
        private readonly ILogger<SettingNotificationRepository> _logger;
        public SettingNotificationRepository(IDbContextFactory<UserContext> dbContextFactory, ILogger<SettingNotificationRepository> logger)
        {
            _logger = logger;
            _contextFactory = dbContextFactory;
        }

        public async Task<SettingNotification> GetAsync(int id)
        {
            _logger.LogTrace($"Вызван метод GetAsync с параметрами: id: {id}");

            using var db = _contextFactory.CreateDbContext();
            return await db.SettingNotifications.FindAsync(id);
        }

        public async Task<List<SettingNotification>> GetAllAsync()
        {
            _logger.LogTrace($"Вызван метод GetAllAsync");

            using var db = _contextFactory.CreateDbContext();
            var settingNotifications = await db.SettingNotifications.ToListAsync();
            return settingNotifications;
        }

        public async Task<SettingNotification> GetByItemAsync(SettingNotification item)
        {
            _logger.LogTrace($"Вызван метод GetByItemAsync с параметрами: item: {item}");

            using var db = _contextFactory.CreateDbContext();
            return await db.SettingNotifications.FirstOrDefaultAsync(s => s.NewPosts == item.NewPosts && s.NewSubscibe == item.NewSubscibe && s.NewMessage == item.NewMessage);
        }

        public void Create(SettingNotification item)
        {
            _logger.LogTrace($"Вызван метод Create с параметрами: item: {item}");

            using var db = _contextFactory.CreateDbContext();
            db.SettingNotifications.Add(item);
            db.SaveChanges();
        }

        public void Update(SettingNotification item)
        {
            _logger.LogTrace($"Вызван метод Update с параметрами: item: {item}");

            using var db = _contextFactory.CreateDbContext();
            db.Update(item);
            db.SaveChanges();
        }

        public async Task DeleteAsync(int id)
        {
            _logger.LogTrace($"Вызван метод DeleteAsync с параметрами: id: {id}");

            using var db = _contextFactory.CreateDbContext();
            SettingNotification item = await GetAsync(id);
            db.Remove(item);
            db.SaveChanges();
        }
    }
}