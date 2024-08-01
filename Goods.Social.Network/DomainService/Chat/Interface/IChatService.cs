using DomainModel.Entities.ViewModels;
using Goods.System.Social.Network.DomainModel.Entities;

namespace DomainServices.Chat.Interface
{
    public interface IChatService
    {
        Task<ChatRoom> GetAsync(int chatId);
        Task<int> CreateAsync(ChatRoom chatRoom, int userId);
        Task<PageChat> GetChatsAsync(int userId, int number, string search);
        Task UpdateAsync(ChatRoom chatRoom);
        void Delete(int chatId);
    }
}