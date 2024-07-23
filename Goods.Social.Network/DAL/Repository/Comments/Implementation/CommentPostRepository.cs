using Goods.System.Social.Network.DAL;
using Microsoft.EntityFrameworkCore;
using Goods.System.Social.Network.DomainModel.Entities;
using Microsoft.Extensions.Logging;
using NLog;
using DAL.Repository.Comments.Interface;

namespace DAL.Repository.Comments.Implementation
{

    public class CommentPostRepository : ICommentPostRepository
    {
        private readonly IDbContextFactory<UserContext> _contextFactory;
        private readonly ILogger<CommentPostRepository> _logger;
        public CommentPostRepository(IDbContextFactory<UserContext> dbContextFactory, ILogger<CommentPostRepository> logger)
        {
            _logger = logger;
            _contextFactory = dbContextFactory;
        }

        public void Create(CommentPost commentPost)
        {
            _logger.LogTrace($"Вызван метод Create с параметрами: commentPost: {commentPost}");

            using var db = _contextFactory.CreateDbContext();
            db.CommentPosts.Add(commentPost);
            db.SaveChanges();
        }

        public async Task DeleteAsync(int id)
        {
            _logger.LogTrace($"Вызван метод DeleteAsync с параметрами: id: {id}");

            using var db = _contextFactory.CreateDbContext();
            CommentPost commentPost = await GetAsync(id);
            db.Remove(commentPost);
            await db.SaveChangesAsync();
        }

        public async Task<List<CommentPost>> GetAllAsync()
        {
            _logger.LogTrace($"Вызван метод GetAllAsync");

            using var db = _contextFactory.CreateDbContext();
            return await db.CommentPosts.ToListAsync();
        }

        public async Task<List<CommentPost>> GetAllByPostAsync(int postId)
        {
            _logger.LogTrace($"Вызван метод GetAllByUser с параметрами: postId: {postId}");

            using var db = _contextFactory.CreateDbContext();
            return await db.CommentPosts.Where(cp => cp.PostId == postId).Include(cp => cp.Comment).ThenInclude(c => c.User).ToListAsync();
        }

        public async Task<CommentPost> GetAsync(int id)
        {
            _logger.LogTrace($"Вызван метод GetAsync с параметрами: id: {id}");

            using var db = _contextFactory.CreateDbContext();
            return await db.CommentPosts.FindAsync(id);
        }

        public void Update(CommentPost commentPost)
        {
            _logger.LogTrace($"Вызван метод Update с параметрами: comment: {commentPost}");

            using var db = _contextFactory.CreateDbContext();
            db.CommentPosts.Update(commentPost);
            db.SaveChanges();
        }
    }
}