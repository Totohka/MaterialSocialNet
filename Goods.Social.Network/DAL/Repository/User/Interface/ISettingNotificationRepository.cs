using Goods.System.Social.Network.DomainModel.Entities;

namespace Goods.System.Social.Network.DAL.Repository.Interface
{
    public interface ISettingNotificationRepository : IRepository<SettingNotification>
    {
        Task<SettingNotification> GetByItemAsync(SettingNotification settingNotification);
    }
}
