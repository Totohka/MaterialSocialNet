using Goods.System.Social.Network.DAL.Repository.Interface;
using Goods.System.Social.Network.DAL;
using Goods.System.Social.Network.DomainModel.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NLog;
namespace Goods.System.Social.Network.DAL.Repository.Realization
{

    public class SubscribeRepository : ISubscribeRepository
    {
        private readonly IDbContextFactory<UserContext> _contextFactory;
        private readonly ILogger<SubscribeRepository> _logger;
        public SubscribeRepository(IDbContextFactory<UserContext> dbContextFactory, ILogger<SubscribeRepository> logger)
        {
            _logger = logger;
            _contextFactory = dbContextFactory;
        }

        public async Task<UserFriend> GetAsync(int id, int idFriend)
        {
            _logger.LogTrace($"Вызван метод GetAsync с параметрами: id: {id}, idFriend: {idFriend}");

            using var db = _contextFactory.CreateDbContext();
            var userFriend = await db.UserFriends.FirstOrDefaultAsync(uf => uf.UserId == id && uf.UserFriendId == idFriend);
            return userFriend;
        }

        public async Task<bool> GetCheckAsync(int id, int idFriend)
        {
            _logger.LogTrace($"Вызван метод GetCheckAsync с параметрами: id: {id}, idFriend: {idFriend}");

            using var db = _contextFactory.CreateDbContext();
            var userFriend = await db.UserFriends.FirstOrDefaultAsync(uf => uf.UserId == id && uf.UserFriendId == idFriend);
            return (userFriend is not null);
        }

        public async Task CreateAsync(UserFriend item)
        {
            _logger.LogTrace($"Вызван метод Create с параметрами: item: {item}");

            using var db = _contextFactory.CreateDbContext();
            await db.UserFriends.AddAsync(item);
            db.SaveChanges();
        }

        public async Task DeleteAsync(UserFriend item)
        {
            _logger.LogTrace($"Вызван метод DeleteAsync с параметрами: item: {item}");

            using var db = _contextFactory.CreateDbContext();
            var userFriend = await GetAsync(item.UserId, item.UserFriendId);
            db.Remove(userFriend);
            db.SaveChanges();
        }
    }
}