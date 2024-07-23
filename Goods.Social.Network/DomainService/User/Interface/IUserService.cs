using DomainModel.Entities.ViewModels;
using Goods.System.Social.Network.DomainModel.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Goods.System.Social.Network.DomainServices.Interface
{
    public interface IUserService
    {
        Task<PageUser> GetAllAsync(string search, int number, int id, int who);
        Task<User> GetAsync(int id);
        Task UpdateAsync(User user);
        Task ChangeSettingPrivacyAsync(SettingPrivacy settingPrivacy, int userId);
        Task ChangeSettingNotificationAsync(SettingNotification settingNotification, int userId);
    }
}