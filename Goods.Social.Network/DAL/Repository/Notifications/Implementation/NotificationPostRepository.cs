using DAL.Repository.Notifications.Interface;
using DomainModel.Entities.ViewModels;
using Goods.System.Social.Network.DAL;
using Goods.System.Social.Network.DomainModel.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository.Notifications.Implementation
{
    public class NotificationPostRepository : INotificationPostRepository
    {
        private readonly IDbContextFactory<UserContext> _contextFactory;

        public NotificationPostRepository(IDbContextFactory<UserContext> dbContextFactory) { 
            _contextFactory = dbContextFactory;
        }
        public async Task<int> CreateAsync(NotificationPost notificationChatRoom)
        {
            using var db = await _contextFactory.CreateDbContextAsync();
            await db.NotificationPosts.AddAsync(notificationChatRoom);
            await db.SaveChangesAsync();
            return db.NotificationPosts.Max(np => np.Id);
        }

        public async Task DeleteAsync(int id)
        {
            using var db = await _contextFactory.CreateDbContextAsync();
            var notificationPost = await GetAsync(id);
            db.NotificationPosts.Remove(notificationPost);
            await db.SaveChangesAsync();
        }

        public async Task<List<NotificationPost>> GetAllByNotificationAsync(int notificationId)
        {
            using var db = await _contextFactory.CreateDbContextAsync();
            return await db.NotificationPosts.Where(np => np.NotificationId == notificationId).ToListAsync();
        }

        public async Task<NotificationPost> GetAsync(int id)
        {
            using var db = await _contextFactory.CreateDbContextAsync();
            return await db.NotificationPosts.FindAsync(id);
        }

        public async Task UpdateAsync(NotificationPost notificationChatRoom)
        {
            using var db = await _contextFactory.CreateDbContextAsync();
            db.NotificationPosts.Update(notificationChatRoom);
            await db.SaveChangesAsync();
        }
    }
}
