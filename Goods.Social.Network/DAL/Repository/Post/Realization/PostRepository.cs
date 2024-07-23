using Goods.System.Social.Network.DAL.Repository.Interface;
using Goods.System.Social.Network.DAL;
using Microsoft.EntityFrameworkCore;
using Goods.System.Social.Network.DomainModel.Entities;
using Microsoft.Extensions.Logging;
using NLog;
namespace Goods.System.Social.Network.DAL.Repository.Realization
{

    public class PostRepository : IPostRepository
    {
        private readonly IDbContextFactory<UserContext> _contextFactory;
        private readonly ILogger<PostRepository> _logger;
        public PostRepository(IDbContextFactory<UserContext> dbContextFactory, ILogger<PostRepository> logger)
        {
            _logger = logger;
            _contextFactory = dbContextFactory;
        }
        public async Task DeleteAsync(int id)
        {
            _logger.LogTrace($"Вызван метод DeleteAsync с параметрами: id: {id}");

            using var db = _contextFactory.CreateDbContext();
            Post item = await GetAsync(id);
            db.Remove(item);
            db.SaveChanges();
        }
        public async Task<Post> GetAsync(int id)
        {
            _logger.LogTrace($"Вызван метод GetAsync с параметрами: id: {id}");

            using var db = _contextFactory.CreateDbContext();
            return await db.Set<Post>().FindAsync(id);
        }
        public async Task<List<Post>> GetAllAsync()
        {
            _logger.LogTrace($"Вызван метод GetAllAsync");

            using var db = _contextFactory.CreateDbContext();
            return await db.Set<Post>().ToListAsync();
        }
        public void Create(Post item)
        {
            _logger.LogTrace($"Вызван метод Create с параметрами: item: {item}");

            using var db = _contextFactory.CreateDbContext();
            db.Set<Post>().Add(item);
            db.SaveChanges();
        }

        public void Update(Post item)
        {
            _logger.LogTrace($"Вызван метод Update с параметрами: item: {item}");

            using var db = _contextFactory.CreateDbContext();
            db.Update(item);
            db.SaveChanges();
        }

        public async Task<List<Post>> GetByUserAsync(int userId)
        {
            _logger.LogTrace($"Вызван метод GetByUserAsync с параметрами: userId: {userId}");

            using var db = _contextFactory.CreateDbContext();
            return await db.Posts.Where(p => p.UserId == userId).ToListAsync();
        }
    }
}