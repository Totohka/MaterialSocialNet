using Goods.System.Social.Network.DAL;
using Goods.System.Social.Network.DAL.Repository.Interface;
using Goods.System.Social.Network.DomainModel.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository.Notifications.Implementation
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly IDbContextFactory<UserContext> _contextFactory;

        public NotificationRepository(IDbContextFactory<UserContext> dbContextFactory) { 
            _contextFactory = dbContextFactory;
        }
        public async Task<int> CreateAsync(Notification notification)
        {
            using var db = await _contextFactory.CreateDbContextAsync();
            var notificationByUser = await db.Notifications.Where(n => n.UserId == notification.UserId).FirstOrDefaultAsync();
            if (notificationByUser is not null)
                return notificationByUser.Id;
            await db.Notifications.AddAsync(notification);
            await db.SaveChangesAsync();
            notificationByUser = await GetAllByUserAsync(notification.UserId);
            return notificationByUser.Id;
        }

        public async Task DeleteAsync(int id)
        {
            using var db = await _contextFactory.CreateDbContextAsync();
            var notification = await GetAsync(id);
            db.Notifications.Remove(notification);
            await db.SaveChangesAsync();
        }

        public Notification GetAllByUser(int userId)
        {
            using var db = _contextFactory.CreateDbContext();
            return db.Notifications.Where(n => n.UserId == userId).Include(n => n.NotificationChatRooms.Where(ncr => ncr.IsCheck == false)).Include(n => n.NotificationPosts.Where(np => np.IsCheck == false)).FirstOrDefault();
        }

        public async Task<Notification> GetAllByUserAsync(int userId)
        {
            using var db = await _contextFactory.CreateDbContextAsync();
            return await db.Notifications.Where(n => n.UserId == userId).Include(n => n.NotificationChatRooms.Where(ncr => ncr.IsCheck == false)).Include(n => n.NotificationPosts.Where(np => np.IsCheck == false)).FirstOrDefaultAsync();
        }

        public async Task<Notification> GetAsync(int id)
        {
            using var db = await _contextFactory.CreateDbContextAsync();
            return await db.Notifications.Where(n => n.Id == id).Include(n => n.NotificationChatRooms.Where(ncr => ncr.IsCheck == false)).Include(n => n.NotificationPosts.Where(np => np.IsCheck == false)).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(Notification notification)
        {
            using var db = await _contextFactory.CreateDbContextAsync();
            db.Notifications.Update(notification);
            await db.SaveChangesAsync();
        }
    }
}
