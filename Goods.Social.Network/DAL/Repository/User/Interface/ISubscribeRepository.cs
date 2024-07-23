using Goods.System.Social.Network.DomainModel.Entities;

namespace Goods.System.Social.Network.DAL.Repository.Interface
{
    public interface ISubscribeRepository
    {
        Task<bool> GetCheckAsync(int id, int idFriend); //ты подписан на человека? 
        Task CreateAsync(UserFriend item);
        Task DeleteAsync(UserFriend item);
    }
}
