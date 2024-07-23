using DomainModel.Entities.ViewModels;
using Goods.System.Social.Network.DomainModel.Entities;

namespace DomainServices.Chat.Interface
{
    public interface IInviteService
    {
        Task InvateUserInChatAsync(ChatRoomUser chatRoomUser);
        Task<List<User>> GetUsersInChatAsync(int chatId);
        Task DeleteAsync(ChatRoomUser chatRoomUser);
    }
}