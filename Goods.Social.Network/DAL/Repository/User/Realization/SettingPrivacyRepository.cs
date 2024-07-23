using Goods.System.Social.Network.DAL.Repository.Interface;
using Goods.System.Social.Network.DAL;
using Goods.System.Social.Network.DomainModel.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NLog;
namespace Goods.System.Social.Network.DAL.Repository.Realization
{

    public class SettingPrivacyRepository : ISettingPrivacyRepository
    {
        private readonly IDbContextFactory<UserContext> _contextFactory;
        private readonly ILogger<SettingPrivacyRepository> _logger;
        public SettingPrivacyRepository(IDbContextFactory<UserContext> dbContextFactory, ILogger<SettingPrivacyRepository> logger)
        {
            _logger = logger;
            _contextFactory = dbContextFactory;
        }

        public async Task<SettingPrivacy> GetAsync(int id)
        {
            _logger.LogTrace($"Вызван метод GetAsync с параметрами: id: {id}");

            using var db = _contextFactory.CreateDbContext();
            return await db.SettingPrivacies.FindAsync(id);
        }

        public async Task<List<SettingPrivacy>> GetAllAsync()
        {
            _logger.LogTrace($"Вызван метод GetAllAsync");

            using var db = _contextFactory.CreateDbContext();
            var settingNotifications = await db.SettingPrivacies.ToListAsync();
            return settingNotifications;
        }

        public async Task<SettingPrivacy> GetByItemAsync(SettingPrivacy item)
        {
            _logger.LogTrace($"Вызван метод GetByItemAsync с параметрами: item: {item}");

            using var db = _contextFactory.CreateDbContext();
            return await db.SettingPrivacies.FirstOrDefaultAsync(s => s.InvateChats == item.InvateChats && s.ShowDateBirthday == item.ShowDateBirthday && s.ShowPost == item.ShowPost);
        }

        public void Create(SettingPrivacy item)
        {
            _logger.LogTrace($"Вызван метод Create с параметрами: item: {item}");

            using var db = _contextFactory.CreateDbContext();
            db.SettingPrivacies.Add(item);
            db.SaveChanges();
        }

        public void Update(SettingPrivacy item)
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
            SettingPrivacy item = await GetAsync(id);
            db.Remove(item);
            db.SaveChanges();
        }
    }
}