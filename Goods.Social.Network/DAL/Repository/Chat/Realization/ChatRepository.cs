using Goods.System.Social.Network.DAL.Repository.Interface;
using Goods.System.Social.Network.DAL;
using Microsoft.EntityFrameworkCore;
using Goods.System.Social.Network.DomainModel.Entities;
using Microsoft.Extensions.Logging;
using NLog;
namespace Goods.System.Social.Network.DAL.Repository.Realization
{

    public class ChatRepository : IChatRepository
    {
        private readonly ILogger<ChatRepository> _logger;
        private readonly IDbContextFactory<UserContext> _contextFactory;
        public ChatRepository(IDbContextFactory<UserContext> dbContextFactory, ILogger<ChatRepository> logger)
        {
            _logger = logger;
            _contextFactory = dbContextFactory;
        }
        public async Task<ChatRoom> GetAsync(int id)
        {
            _logger.LogTrace($"Вызван метод GetAsync с параметрами: id : {id}");
            using var db = _contextFactory.CreateDbContext();
            return await db.ChatRooms.FindAsync(id);
        }
        public async Task<int> CreateAsync(ChatRoom chatRoom, int userId)
        {
            _logger.LogTrace($"Вызван метод Create с параметрами: chatRoom : {userId}");
            using var db = _contextFactory.CreateDbContext();
            var user = await db.Users.FindAsync(userId);
            db.ChatRooms.Add(chatRoom);
            user.ChatRooms = new List<ChatRoomUser>
            {
                new ChatRoomUser() { ChatRoom = chatRoom }
            };
            db.SaveChanges();
            return db.ChatRooms.Max(cr => cr.Id);
        }

        public async void Delete(int id)
        {
            _logger.LogTrace($"Вызван метод Delete с параметрами: id : {id}");
            using var db = _contextFactory.CreateDbContext();
            ChatRoom chatRoom = await GetAsync(id);
            db.Remove(chatRoom);
            db.SaveChanges();
        }

        public async Task<List<ChatRoom>> GetAllAsync()
        {
            _logger.LogTrace($"Вызван метод GetAllAsync");
            using var db = _contextFactory.CreateDbContext();
            var allChats = new List<ChatRoom>();
            allChats.AddRange(db.ChatRooms.Include(cr => cr.Messages).Where(cr => cr.Messages.Count > 0).OrderBy(cr => cr.Messages.Max(m => m.DateSend)));
            allChats.Reverse();
            return allChats;
        }

        public void Update(ChatRoom chatRoom)
        {
            _logger.LogTrace($"Вызван метод Update с параметрами: chatRoom : {chatRoom}");
            using var db = _contextFactory.CreateDbContext();
            db.ChatRooms.Update(chatRoom);
            db.SaveChanges();
        }
    }
}