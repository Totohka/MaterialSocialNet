using Goods.System.Social.Network.DAL;
using Goods.System.Social.Network.DAL.Repository.Interface;
using Goods.System.Social.Network.DomainModel.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository.Notifications.Implementation
{
    public class NotificationChatRoomRepository : INotificationChatRoomRepository
    {
        private readonly IDbContextFactory<UserContext> _contextFactory;

        public NotificationChatRoomRepository(IDbContextFactory<UserContext> dbContextFactory) { 
            _contextFactory = dbContextFactory;
        }
        public async Task<int> CreateAsync(NotificationChatRoom notificationChatRoom)
        {
            using var db = await _contextFactory.CreateDbContextAsync();
            await db.NotificationChatRooms.AddAsync(notificationChatRoom);
            await db.SaveChangesAsync();
            return db.NotificationChatRooms.Max(ncr => ncr.Id);
        }

        public async Task DeleteAsync(int id)
        {
            using var db = await _contextFactory.CreateDbContextAsync();
            var notificationChatRoom = await GetAsync(id);
            db.NotificationChatRooms.Remove(notificationChatRoom);
            await db.SaveChangesAsync();
        }

        public async Task<List<NotificationChatRoom>> GetAllByNotificationAsync(int notificationId)
        {
            using var db = await _contextFactory.CreateDbContextAsync();
            return await db.NotificationChatRooms.Where(np => np.NotificationId == notificationId).ToListAsync();
        }

        public async Task<NotificationChatRoom> GetAsync(int id)
        {
            using var db = await _contextFactory.CreateDbContextAsync();
            return await db.NotificationChatRooms.FindAsync(id);
        }

        public async Task UpdateAsync(NotificationChatRoom notificationChatRoom)
        {
            using var db = await _contextFactory.CreateDbContextAsync();
            db.NotificationChatRooms.Update(notificationChatRoom);
            await db.SaveChangesAsync();
        }
    }
}
