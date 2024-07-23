using Goods.System.Social.Network.DAL.Repository.Interface;
using Goods.System.Social.Network.DAL;
using Microsoft.EntityFrameworkCore;
using Goods.System.Social.Network.DomainModel.Entities;

namespace Goods.System.Social.Network.DAL.Repository.Realization
{

    public class ReactionEntityRepository<T> : IReactionEntityRepository<T> where T : class
    {
        private readonly int _countReaction = 20;
        private readonly IDbContextFactory<UserContext> _contextFactory;
        private enum Entities
        {
            ReactionPost,
            ReactionMessage
        };
        private readonly Entities _entities;
        public ReactionEntityRepository(IDbContextFactory<UserContext> dbContextFactory)
        {
            _contextFactory = dbContextFactory;
            switch (typeof(T))
            {
                case var type when type == typeof(ReactionMessage):
                    _entities = Entities.ReactionMessage;
                    break;
                case var type when type == typeof(ReactionPost):
                    _entities = Entities.ReactionPost; 
                    break;
                default:
                    break;
            }
        }

        public async Task<int> GetCountAsync(int entityId)
        {
            using var db = _contextFactory.CreateDbContext();
            switch (_entities)
            {
                case Entities.ReactionPost:
                    return await db.ReactionPosts.Where(rp => rp.PostId == entityId).CountAsync();
                case Entities.ReactionMessage:
                    return await db.ReactionMessages.Where(rp => rp.MessageId == entityId).CountAsync();
                default:
                    return 0;
            }
        }

        public async Task<T> GetAsync(int id)
        {
            using var db = _contextFactory.CreateDbContext();
            return await db.Set<T>().FindAsync(id);
        }
        public async Task CreateAsync(T reaction)
        {
            using var db = _contextFactory.CreateDbContext();
            await db.Set<T>().AddAsync(reaction);
            await db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            using var db = _contextFactory.CreateDbContext();
            T reaction = await GetAsync(id);
            db.Set<T>().Remove(reaction);
            await db.SaveChangesAsync();
        }

        public async Task<List<T>> GetAllByEntityAsync(int entityId, int number = 0)
        {
            using var db = _contextFactory.CreateDbContext();
            switch (_entities)
            {
                case Entities.ReactionPost:
                    if (number == -1)
                    {
                        return await db.ReactionPosts.Where(rp => rp.PostId == entityId)
                        .Include(rp => rp.Reaction)
                            .ThenInclude(r => r.TypeReaction)
                        .Include(rp => rp.Reaction)
                            .ThenInclude(r => r.User)
                        .ToListAsync() as List<T>;
                    }
                    else
                    {
                        return await db.ReactionPosts.Where(rp => rp.PostId == entityId)
                            .Skip(number * _countReaction).Take(_countReaction)
                            .Include(rp => rp.Reaction)
                                .ThenInclude(r => r.TypeReaction)
                            .Include(rp => rp.Reaction)
                                .ThenInclude(r => r.User)
                            .ToListAsync() as List<T>;
                            
                    }
                case Entities.ReactionMessage:
                    if (number == -1)
                    {
                        return await db.ReactionMessages.Where(rp => rp.MessageId == entityId)
                        .Include(rp => rp.Reaction)
                            .ThenInclude(r => r.TypeReaction)
                        .Include(rp => rp.Reaction)
                            .ThenInclude(r => r.User)
                        .ToListAsync() as List<T>;
                    }
                    else
                    {
                        return await db.ReactionMessages.Where(rp => rp.MessageId == entityId)
                            .Skip(number * _countReaction).Take(_countReaction)
                            .Include(rp => rp.Reaction)
                                .ThenInclude(r => r.TypeReaction)
                            .Include(rp => rp.Reaction)
                                .ThenInclude(r => r.User)
                            .ToListAsync() as List<T>;
                    }
                default:
                    return new List<T>();
            }            
        }

        public async Task UpdateAsync(T reaction)
        {
            using var db = _contextFactory.CreateDbContext();
            db.Set<T>().Update(reaction);
            await db.SaveChangesAsync();
        }
    }
}