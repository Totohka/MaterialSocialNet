using Goods.System.Social.Network.DAL.Repository.Interface;
using Goods.System.Social.Network.DAL;
using Microsoft.EntityFrameworkCore;
using Goods.System.Social.Network.DomainModel.Entities;
using Microsoft.Extensions.Logging;
using NLog;
namespace Goods.System.Social.Network.DAL.Repository.Realization
{
    public class ReactionRepository : IReactionRepository
    {
        private readonly IDbContextFactory<UserContext> _contextFactory;
        private readonly ILogger<ReactionRepository> _logger;
        public ReactionRepository(IDbContextFactory<UserContext> dbContextFactory, ILogger<ReactionRepository> logger)
        {
            _logger = logger;
            _contextFactory = dbContextFactory;
        }

        public void Create(Reaction reaction)
        {
            _logger.LogTrace($"Вызван метод Create с параметрами: reaction: {reaction}");

            using var db = _contextFactory.CreateDbContext();
            if (!db.Reactions.Where(r => r.UserId == reaction.UserId && r.TypeReactionId == reaction.TypeReactionId).Any())
            {
                db.Reactions.Add(reaction);
                db.SaveChanges();
            }
        }

        public async Task DeleteAsync(int userId, int typeId)
        {
            _logger.LogTrace($"Вызван метод DeleteAsync с параметрами: userId: {userId}, typeId: {typeId}");

            using var db = _contextFactory.CreateDbContext();
            var reaction = await GetAsync(userId, typeId);
            db.Reactions.Remove(reaction);
            db.SaveChanges();
        }

        public async Task<Reaction> GetAsync(int userId, int typeId)
        {
            _logger.LogTrace($"Вызван метод GetAsync с параметрами: userId: {userId}, typeId: {typeId}");

            using var db = _contextFactory.CreateDbContext();
            return await db.Reactions.Where(r => r.UserId == userId && r.TypeReactionId == typeId).FirstAsync();
        }

        public void Update(Reaction reaction)
        {
            _logger.LogTrace($"Вызван метод Update с параметрами: reaction: {reaction}");

            using var db = _contextFactory.CreateDbContext();
            db.Reactions.Update(reaction);
            db.SaveChanges();
        }

        public async Task<List<Reaction>> GetAllByUserAsync(int userId)
        {
            _logger.LogTrace($"Вызван метод GetAllByUser с параметрами: userId: {userId}");

            using var db = _contextFactory.CreateDbContext();
            return await db.Reactions.Where(r => r.UserId == userId).ToListAsync();
        }
    }
}