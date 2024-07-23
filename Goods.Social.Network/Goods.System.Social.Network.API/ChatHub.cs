using Goods.System.Social.Network.DomainModel.Entities;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Goods.System.Social.Network.DomainServices.Interface;

namespace Goods.System.Social.Network.API
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly IChatService _chatService; 
        public ChatHub(IChatService chatService) 
        {
            _chatService = chatService;
        }
        public async Task Enter(string username, string groupNameId)
        {
            if (String.IsNullOrEmpty(username))
            {
                await Clients.Caller.SendAsync("Notify", "Авторизуйтесь для входа в чат");
            }
            else
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, groupNameId);
                //await Clients.Group(groupname).SendAsync("Notify", $"{username} вошел в чат");
            }
        }
        public async Task Send(string message, string username, int userId, int chatId)
        {
            await _chatService.SaveMessage(message, userId, chatId);
            await this.Clients.Group(chatId.ToString()).SendAsync("Receive", message, username);
        }
    }
}