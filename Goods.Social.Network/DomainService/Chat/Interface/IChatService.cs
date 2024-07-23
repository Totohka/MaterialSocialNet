using DomainModel.Entities.ViewModels;
using Goods.System.Social.Network.DomainModel.Entities;

namespace DomainServices.Chat.Interface
{
    public interface IChatService
    {
        Task<ChatRoom> GetAsync(int chatId);
        Task<int> CreateAsync(ChatRoom chatRoom, int userId);
        Task<PageChatViewModel> GetChatsAsync(int userId, int number, string search);
        void Update(ChatRoom chatRoom);
        void Delete(int chatId);
    }
}