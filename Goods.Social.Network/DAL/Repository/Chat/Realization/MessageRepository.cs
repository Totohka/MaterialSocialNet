using Goods.System.Social.Network.DAL.Repository.Interface;
using Goods.System.Social.Network.DAL;
using Microsoft.EntityFrameworkCore;
using Goods.System.Social.Network.DomainModel.Entities;
using Microsoft.Extensions.Logging;
using NLog;
namespace Goods.System.Social.Network.DAL.Repository.Realization
{

    public class MessageRepository : IMessageRepository
    {
        private readonly IDbContextFactory<UserContext> _contextFactory;
        private readonly ILogger<MessageRepository> _logger;
        public MessageRepository(IDbContextFactory<UserContext> dbContextFactory, ILogger<MessageRepository> logger)
        {
            _logger = logger;
            _contextFactory = dbContextFactory;
        }
        public async Task<Message> GetAsync(int id)
        {
            _logger.LogTrace($"Вызван метод GetAsync с параметрами: id : {id}");
            using var db = _contextFactory.CreateDbContext();
            return await db.Messages.FindAsync(id);
        }
        public int Create(Message message)
        {
            _logger.LogTrace($"Вызван метод Create с параметрами: message : {message}");
            using var db = _contextFactory.CreateDbContext();
            db.Messages.Add(message);
            db.SaveChanges();
            int id = db.Messages.Max(m => m.Id);
            return id;
        }

        public async Task DeleteAsync(int id)
        {
            _logger.LogTrace($"Вызван метод DeleteAsync с параметрами: id : {id}");
            using var db = _contextFactory.CreateDbContext();
            Message message = await GetAsync(id);
            db.Remove(message);
            db.SaveChanges();
        }

        public async Task<List<Message>> GetAllByChatAsync(int chatId)
        {
            _logger.LogTrace($"Вызван метод GetAllByChatAsync с параметрами: chatId : {chatId}");
            using var db = _contextFactory.CreateDbContext();
            var messagesByChat = await db.Messages.Where(m => m.ChatRoomId == chatId).Include(m => m.User).ToListAsync();
            return messagesByChat;
        }

        public void Update(Message message)
        {
            _logger.LogTrace($"Вызван метод Update с параметрами: message : {message}");
            using var db = _contextFactory.CreateDbContext();
            db.Messages.Update(message);
            db.SaveChanges();
        }
    }
}