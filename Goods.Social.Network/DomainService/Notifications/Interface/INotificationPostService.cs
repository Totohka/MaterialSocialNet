using DomainModel.Entities.ViewModels;
using Goods.System.Social.Network.DomainModel.Entities;
using Microsoft.AspNetCore.Http;

namespace Goods.System.Social.Network.DomainServices.Interface
{
    public interface INotificationPostService
    {
        Task<NotificationPost> GetAsync(int id);
        Task<List<NotificationPost>> GetAllByNotificationAsync(int notificationId);
        Task CreateAsync(bool isCheck, int userId, int userPostId, int postId);
        //Task UpdateAsync(int entityId, int userId, int typeReactionId, int typeReactionOldId);
        //Task DeleteAsync(int entityId, int userId, int typeReactionId);
    }
}