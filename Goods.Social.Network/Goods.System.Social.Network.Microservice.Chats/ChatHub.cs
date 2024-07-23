using Goods.System.Social.Network.DomainModel.Entities;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DomainServices.Chat.Interface;
using Goods.System.Social.Network.Microservice.Chats.Controllers;

namespace Goods.System.Social.Network.Microservice.Chats
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly IChatService _chatService;
        private readonly ILogger<ChatHub> _logger;
        public ChatHub(IChatService chatService, ILogger<ChatHub> logger) 
        {
            _chatService = chatService;
            _logger = logger;
        }

        public async Task OnConnectedChatAsync(string chatId)
        {
            _logger.LogInformation($"Вызван метод OnConnectedChatAsync");
            await Groups.AddToGroupAsync(Context.ConnectionId, chatId);
            await Clients.Client(Context.ConnectionId).SendAsync("Connect", chatId);
        }

        public async Task OnDisconnectedChatAsync(string chatId)
        {
            _logger.LogInformation($"Вызван метод OnDisconnectedChatAsync");
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatId);
            await Clients.Client(Context.ConnectionId).SendAsync("Disconnect", chatId);
        }

        public async Task Send(string message, string username, string chatId, string userId)
        {
            _logger.LogInformation($"Вызван метод Send");
            await Clients.Group(chatId).SendAsync("Send", message, username, userId);
        }
        public override async Task OnConnectedAsync()
        {
            _logger.LogInformation($"Вызван метод OnConnectedAsync");
            await Clients.All.SendAsync("Notify", $"{Context.ConnectionId} вошел в чат");
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            _logger.LogInformation($"Вызван метод OnDisconnectedAsync");
            await Clients.All.SendAsync("Notify", $"{Context.ConnectionId} покинул в чат");
            await base.OnDisconnectedAsync(exception);
        }
    }
}