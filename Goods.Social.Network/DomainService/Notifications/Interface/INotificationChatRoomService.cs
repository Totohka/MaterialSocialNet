using DomainModel.Entities.ViewModels;
using Goods.System.Social.Network.DomainModel.Entities;
using Microsoft.AspNetCore.Http;

namespace Goods.System.Social.Network.DomainServices.Interface
{
    public interface INotificationChatRoomService
    {
        Task<NotificationChatRoom> GetAsync(int id);
        Task<List<NotificationChatRoom>> GetAllByNotificationAsync(int notificationId);
        Task CreateAsync(bool isCheck, int userId, int chatRoomId);
        //Task UpdateAsync(int entityId, int userId, int typeReactionId, int typeReactionOldId);
        //Task DeleteAsync(int entityId, int userId, int typeReactionId);
    }
}