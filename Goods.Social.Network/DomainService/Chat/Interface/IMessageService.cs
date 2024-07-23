using DomainModel.Entities.ViewModels;
using Goods.System.Social.Network.DomainModel.Entities;

namespace DomainServices.Chat.Interface
{
    public interface IMessageService
    {
        Task<Message> GetAsync(int chatId);
        Task<int> CreateAsync(Message message);
        Task<PageMessage> GetMessagesAsync(int chatId, int number);
        Task UpdateAsync(Message message);
        Task DeleteAsync(int messageId);
    }
}