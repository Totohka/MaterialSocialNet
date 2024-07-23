using DomainModel.Entities.ViewModels;
using Goods.System.Social.Network.DomainModel.Entities;

namespace DAL.Repository.Notifications.Interface
{
    public interface INotificationPostRepository
    {
        Task<NotificationPost> GetAsync(int id);
        Task<List<NotificationPost>> GetAllByNotificationAsync(int notificationId);
        Task<int> CreateAsync(NotificationPost notificationChatRoom);
        Task UpdateAsync(NotificationPost notificationChatRoom);
        Task DeleteAsync(int id);
    }
}
