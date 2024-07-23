using Goods.System.Social.Network.DomainModel.Entities;

namespace Goods.System.Social.Network.DAL.Repository.Interface
{
    public interface ISettingPrivacyRepository : IRepository<SettingPrivacy>
    {
        Task<SettingPrivacy> GetByItemAsync(SettingPrivacy settingNotification);
    }
}
