using DomainModel.Entities.ViewModels;
using Goods.System.Social.Network.DomainModel.Entities;

namespace Goods.System.Social.Network.DAL.Repository.Interface
{
    public interface INotificationChatRoomRepository
    {
        Task<NotificationChatRoom> GetAsync(int id);
        Task<List<NotificationChatRoom>> GetAllByNotificationAsync(int notificationId);
        Task<int> CreateAsync(NotificationChatRoom notificationChatRoom);
        Task UpdateAsync(NotificationChatRoom notificationChatRoom);
        Task DeleteAsync(int id);
    }
}
