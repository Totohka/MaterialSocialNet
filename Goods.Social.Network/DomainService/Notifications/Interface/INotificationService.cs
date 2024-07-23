using DomainModel.Entities.ViewModels;
using Goods.System.Social.Network.DomainModel.Entities;
using Microsoft.AspNetCore.Http;

namespace Goods.System.Social.Network.DomainServices.Interface
{
    public interface INotificationService
    {
        Task<NotificationPost> GetAsync(int id);
        Task<Notification> GetAllByUserAsync(int userId);
        Notification GetAllByUser(int userId);
        Task CreateAsync(int userId);
        Task UpdateAsync(Notification notification);
        //Task DeleteAsync(int entityId, int userId, int typeReactionId);
    }
}