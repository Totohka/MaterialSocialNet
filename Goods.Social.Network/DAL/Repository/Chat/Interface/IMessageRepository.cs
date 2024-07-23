using Goods.System.Social.Network.DomainModel.Entities;

namespace Goods.System.Social.Network.DAL.Repository.Interface
{
    public interface IMessageRepository
    {
        Task<Message> GetAsync(int id);
        Task<List<Message>> GetAllByChatAsync(int chatId);
        int Create(Message message);
        void Update(Message message);
        Task DeleteAsync(int id);
    }
}
