using Goods.System.Social.Network.DAL.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Goods.System.Social.Network.DomainModel.Entities;
using Microsoft.Extensions.Logging;
using NLog;
namespace Goods.System.Social.Network.DAL.Repository.Realization
{
    public class TypeReactionRepository : ITypeReactionRepository
    {
        private readonly IDbContextFactory<UserContext> _contextFactory;
        private readonly ILogger<TypeReactionRepository> _logger;
        public TypeReactionRepository(IDbContextFactory<UserContext> dbContextFactory, ILogger<TypeReactionRepository> logger)
        {
            _logger = logger;
            _contextFactory = dbContextFactory;
        }

        public async Task<List<ReactionType>> GetAllAsync()
        {
            _logger.LogTrace($"Вызван метод GetAllAsync");

            using var db = _contextFactory.CreateDbContext();
            return await db.TypeReaction.ToListAsync();
        }
        public async Task CreateAsync(ReactionType reaction)
        {
            _logger.LogTrace($"Вызван метод CreateAsync с параметрами: reaction: {reaction}");

            using var db = _contextFactory.CreateDbContext();
            await db.TypeReaction.AddAsync(reaction);
            await db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            _logger.LogTrace($"Вызван метод DeleteAsync с параметрами: id: {id}");

            using var db = _contextFactory.CreateDbContext();
            var reaction = await GetAsync(id);
            db.TypeReaction.Remove(reaction);
            await db.SaveChangesAsync();
        }

        public async Task<ReactionType> GetAsync(int id)
        {
            _logger.LogTrace($"Вызван метод GetAsync с параметрами: id: {id}");

            using var db = _contextFactory.CreateDbContext();
            return await db.TypeReaction.FindAsync(id);
        }

        public async Task UpdateAsync(ReactionType reaction)
        {
            _logger.LogTrace($"Вызван метод UpdateAsync с параметрами: reaction: {reaction}");

            using var db = _contextFactory.CreateDbContext();
            db.TypeReaction.Update(reaction);
            await db.SaveChangesAsync();
        }
    }
}