using Goods.System.Social.Network.DAL.Repository.Interface;
using Goods.System.Social.Network.DomainModel.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Security.Cryptography;
using NLog;
using Microsoft.Extensions.Logging;
//using NLog;

namespace Goods.System.Social.Network.DAL.Repository.Realization
{

    public class UserRepository : IUserRepository
    {
        private readonly IDbContextFactory<UserContext> _contextFactory;
        private readonly ILogger<UserRepository> _logger;
        public UserRepository(IDbContextFactory<UserContext> dbContextFactory, ILogger<UserRepository> logger)
        {
            _contextFactory = dbContextFactory;
            _logger = logger;
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            _logger.LogTrace($"Вызван метод GetByEmailAsync с параметрами: email : {email}");
            using var db = _contextFactory.CreateDbContext();
            var user = await db.Users.Where(p => p.Email == email).Include(u => u.Posts).FirstOrDefaultAsync();
            return user;
        }

        public async Task<User> GetAsync(int id)
        {
            _logger.LogTrace($"Вызван метод GetAsync с параметрами: id : {id}");
            using var db = _contextFactory.CreateDbContext();
            return await db.Users.FindAsync(id);
        }

        public async Task<List<User>> GetAllAsync()
        {
            _logger.LogTrace($"Вызван метод GetAllAsync");
            using var db = _contextFactory.CreateDbContext();
            var users = await db.Users.ToListAsync();
            return users;
        }

        public void Create(User user)
        {
            _logger.LogTrace($"Вызван метод Create с параметрами: user: {user}");
            using var db = _contextFactory.CreateDbContext();
            var sha1 = SHA1.Create();
            var shaPass = sha1.ComputeHash(Encoding.Unicode.GetBytes(user.Password));
            user.Password = Encoding.Unicode.GetString(shaPass);
            db.Users.Add(user);
            db.SaveChanges();
        }

        public void Update(User item)
        {
            _logger.LogTrace($"Вызван метод Update с параметрами: item : {item}");
            using var db = _contextFactory.CreateDbContext();
            db.Update(item);
            db.SaveChanges();
        }

        public async Task DeleteAsync(int id)
        {
            _logger.LogTrace($"Вызван метод DeleteAsync с параметрами: id : {id}");
            using var db = _contextFactory.CreateDbContext();
            User item = await GetAsync(id);
            db.Remove(item);
            db.SaveChanges();
        }
    }
}