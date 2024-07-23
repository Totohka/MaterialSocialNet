using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Goods.System.Social.Network.Microservice.Chats
{
    [Authorize]
    public class NotificationHub : Hub
    {
        //private readonly IChatService _chatService; 
        //public NotificationHub(IChatService chatService) 
        //{
        //    _chatService = chatService;
        //}

        //public async Task OnConnectedChatAsync(string chatId)
        //{
        //    await Groups.AddToGroupAsync(Context.ConnectionId, chatId);
        //    await Clients.Client(Context.ConnectionId).SendAsync("Connect", chatId);
        //}

        //public async Task OnDisconnectedChatAsync(string chatId)
        //{
        //    await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatId);
        //    await Clients.Client(Context.ConnectionId).SendAsync("Disconnect", chatId);
        //}

        //public async Task Send(string message, string username, string chatId, string userId)
        //{
        //    await Clients.Group(chatId).SendAsync("Send", message, username, userId);
        //}
        //public override async Task OnConnectedAsync()
        //{
        //    await Clients.All.SendAsync("Notify", $"{Context.ConnectionId} вошел в чат");
        //    await base.OnConnectedAsync();
        //}
        //public override async Task OnDisconnectedAsync(Exception exception)
        //{
        //    await Clients.All.SendAsync("Notify", $"{Context.ConnectionId} покинул в чат");
        //    await base.OnDisconnectedAsync(exception);
        //}
    }
}