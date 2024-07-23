using Goods.System.Social.Network.DAL;
using Microsoft.EntityFrameworkCore;
using Goods.System.Social.Network.DomainModel.Entities;
using Microsoft.Extensions.Logging;
using NLog;
using DAL.Repository.Comments.Interface;

namespace DAL.Repository.Comments.Implementation
{

    public class CommentRepository : ICommentRepository
    {
        private readonly IDbContextFactory<UserContext> _contextFactory;
        private readonly ILogger<CommentRepository> _logger;
        public CommentRepository(IDbContextFactory<UserContext> dbContextFactory, ILogger<CommentRepository> logger)
        {
            _logger = logger;
            _contextFactory = dbContextFactory;
        }
        public async Task<Comment> GetAsync(int id)
        {
            _logger.LogTrace($"Вызван метод GetAsync с параметрами: id: {id}");

            using var db = _contextFactory.CreateDbContext();
            return await db.Comments.FindAsync(id);
        }
        public void Create(Comment comment)
        {
            _logger.LogTrace($"Вызван метод Create с параметрами: comment: {comment}");

            using var db = _contextFactory.CreateDbContext();
            if (!db.Comments.Where(r => r.UserId == comment.UserId).Any())
            {
                db.Comments.Add(comment);
                db.SaveChanges();
            }
        }

        public async Task DeleteAsync(int id)
        {
            _logger.LogTrace($"Вызван метод DeleteAsync с параметрами: id: {id}");

            using var db = _contextFactory.CreateDbContext();
            Comment comment = await GetAsync(id);
            db.Remove(comment);
            db.SaveChanges();
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            _logger.LogTrace($"Вызван метод GetAllAsync");

            using var db = _contextFactory.CreateDbContext();
            return await db.Comments.ToListAsync();
        }

        public void Update(Comment comment)
        {
            _logger.LogTrace($"Вызван метод Update с параметрами: comment: {comment}");

            using var db = _contextFactory.CreateDbContext();
            db.Comments.Update(comment);
            db.SaveChanges();
        }

        public async Task<Comment> GetByUserAsync(int userId)
        {
            _logger.LogTrace($"Вызван метод GetAllByUser с параметрами: userId: {userId}");

            using var db = _contextFactory.CreateDbContext();
            return await db.Comments.Where(c => c.UserId == userId).FirstAsync();
        }
    }
}