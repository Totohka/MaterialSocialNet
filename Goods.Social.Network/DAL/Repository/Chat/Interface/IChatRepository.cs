using Goods.System.Social.Network.DomainModel.Entities;

namespace Goods.System.Social.Network.DAL.Repository.Interface
{
    public interface IChatRepository
    {
        Task<ChatRoom> GetAsync(int id);
        Task<List<ChatRoom>> GetAllAsync();
        Task<int> CreateAsync(ChatRoom chatRoom, int userId);
        void Update(ChatRoom chatRoom);
        void Delete(int id);
    }
}
