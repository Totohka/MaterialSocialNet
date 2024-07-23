using DomainModel.Entities.ViewModels;
using Goods.System.Social.Network.DomainModel.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Goods.System.Social.Network.DomainServices.Interface
{
    public interface ISubscribeService
    {
        Task<bool> GetCheckSubscriberAsync(int id, int idFriend);
        Task AddSubscribeAsync(UserFriend userFriend);
        Task DeleteSubscribeAsync(UserFriend userFriend);
    }
}