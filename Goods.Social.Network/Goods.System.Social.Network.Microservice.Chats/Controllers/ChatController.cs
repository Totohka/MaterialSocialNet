using DomainModel.Entities.ViewModels;
using DomainServices.Chat.Interface;
using Goods.System.Social.Network.DomainModel.Entities;
using Goods.System.Social.Network.Microservice.Chats.Entities.ViewModels;
using Goods.System.Social.Network.Microservice.Chats.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Goods.System.Social.Network.Microservice.Chats.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : Controller
    {
        private readonly IChatService _chatService;
        private readonly IMessageService _messageService;
        private readonly IInviteService _inviteService;
        private readonly ILogger<ChatController> _logger;
        private readonly INotificationChatRoomProducer _notificationChatRoomProducer;

        public ChatController(IChatService chatService, 
                              ILogger<ChatController> logger, 
                              IInviteService inviteService, 
                              IMessageService messageService,
                              INotificationChatRoomProducer notificationChatRoomProducer)
        {
            _chatService = chatService;
            _logger = logger;
            _inviteService = inviteService;
            _messageService = messageService;
            _notificationChatRoomProducer = notificationChatRoomProducer;
        }

        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> AddNewChat(ChatRoomViewModel chatRoom, int userId)
        {   
            _logger.LogInformation($"Вызван метод AddNewChat");
            chatRoom.LastMessage = "Чат пуст";
            int idNewChat = await _chatService.CreateAsync(chatRoom, userId);
            await _messageService.CreateAsync(new Message()
            {
                Msg = "Чат создан",
                ChatRoomId = idNewChat,
                DateSend = DateTime.Now,
                UserId = userId
            });

            foreach (var userIdByChat in chatRoom.UserIdsByChat)
            {
                await _inviteService.InvateUserInChatAsync(new ChatRoomUser() { 
                    UserId = userIdByChat,
                    ChatRoomId = idNewChat
                });
                var msgRMQ = new { ChatRoomId = idNewChat, UserId = userIdByChat };
                _notificationChatRoomProducer.SendMessage(msgRMQ);
            }

            return Ok(idNewChat);
        }

        [Authorize]
        [HttpGet("all")]
        public async Task<IActionResult> GetChats(string search = "", int number = 0)
        {
            _logger.LogInformation("Вызван метод GetChats");
            int id = int.Parse(HttpContext.User.FindFirst(claim => claim.Type == "id").Value);
            return Ok(await _chatService.GetChatsAsync(id, number, search));
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get(int chatId)
        {
            _logger.LogInformation($"Вызван метод Get");
            return Ok(await _chatService.GetAsync(chatId));
        }

        [Authorize]
        [HttpPut]
        public IActionResult UpdateChat(ChatRoom chatRoom)
        {
            _logger.LogInformation($"Вызван метод Update");
            _chatService.Update(chatRoom);
            return Ok();
        }

        [Authorize]
        [HttpDelete]
        public IActionResult Delete(int chatId)
        {
            _logger.LogInformation($"Вызван метод Delete");
            _chatService.Delete(chatId);
            return Ok();
        }
    }
}