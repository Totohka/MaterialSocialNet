using DomainModel.Entities.ViewModels;
using Goods.System.Social.Network.DomainModel.Entities;

namespace Goods.System.Social.Network.DAL.Repository.Interface
{
    public interface INotificationRepository
    {
        Task<Notification> GetAsync(int id);
        Task<Notification> GetAllByUserAsync(int userId);
        Notification GetAllByUser(int userId);
        Task<int> CreateAsync(Notification notification);
        Task UpdateAsync(Notification notification);
        Task DeleteAsync(int id);
    }
}
