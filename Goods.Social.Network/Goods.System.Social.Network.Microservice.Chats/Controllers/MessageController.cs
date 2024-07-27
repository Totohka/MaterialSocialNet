using AutoMapper;
using DomainModel.Entities.ViewModels;
using DomainServices.Chat.Interface;
using Goods.System.Social.Network.DomainModel.Entities;
using Goods.System.Social.Network.DomainServices.Interface;
using Goods.System.Social.Network.Microservice.Chats.Entities.DTO;
using Goods.System.Social.Network.Microservice.Chats.Entities.Pagination;
using Goods.System.Social.Network.Microservice.Chats.Entities.ViewModels;
using Goods.System.Social.Network.Microservice.Chats.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Goods.System.Social.Network.Microservice.Chats.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : Controller
    {
        private readonly IMessageService _messageService;
        private readonly IInviteService _inviteService;
        private readonly IHubContext<ChatHub> _chatHub;
        private readonly IReactionEntityService<ReactionMessage> _reactionService;
        private readonly IMapper _mapper;
        private readonly ILogger<MessageController> _logger;
        private readonly INotificationChatRoomProducer _notificationChatRoomProducer;
        public MessageController(IReactionEntityService<ReactionMessage> reactionService, 
                                 IMessageService messageService,
                                 IMapper mapper,
                                 IHubContext<ChatHub> chatHub,
                                 ILogger<MessageController> logger,
                                 INotificationChatRoomProducer notificationChatRoomProducer,
                                 IInviteService inviteService)
        {
            _messageService = messageService;
            _reactionService = reactionService;
            _mapper = mapper;
            _chatHub = chatHub;
            _logger = logger;
            _notificationChatRoomProducer = notificationChatRoomProducer;
            _inviteService = inviteService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddNewMessage(MessageViewModel messageViewModel)
        {
            _logger.LogInformation($"Вызван метод AddNewMessage");
            Message message = _mapper.Map<Message>(messageViewModel);
            int idMessage = await _messageService.CreateAsync(message);
            List<User> usersByChat = await _inviteService.GetUsersInChatAsync(messageViewModel.chat_room_id);
            usersByChat.Remove(usersByChat.Where(u => u.Id == messageViewModel.user_id).First());

            _logger.LogInformation($"Вызван метод AddNewMessage RMQ");

            foreach (var user in usersByChat)
            {
                var msgRMQ = new { ChatRoomId = messageViewModel.chat_room_id, UserId = user.Id };
                _notificationChatRoomProducer.SendMessage(msgRMQ);
            }
            _logger.LogInformation($"Вызван метод AddNewMessage signalR");

            await _chatHub.Clients.Group(messageViewModel.chat_room_id.ToString()).SendAsync(
                "Receive", 
                messageViewModel.chat_room_id.ToString(), 
                messageViewModel.message, 
                messageViewModel.first_name, 
                messageViewModel.last_name, 
                messageViewModel.user_id.ToString(), 
                messageViewModel.date_send.ToString("s"), 
                idMessage.ToString()
            );

            _logger.LogInformation($"Заканчивается метод AddNewMessage");
            return Ok();
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetMessageByChat(int chatId, int number = 0)
        {
            _logger.LogInformation($"Вызван метод GetMessageByChat");
            int userId = int.Parse(HttpContext.User.FindFirst(claim => claim.Type == "id").Value);
            PageMessage pageMessage = await _messageService.GetMessagesAsync(chatId, number);
            List<UserMessageDTO> messageDTOs = new List<UserMessageDTO>();
            foreach (var message in pageMessage.Messages)
            {
                messageDTOs.Add(_mapper.Map<UserMessageDTO>(message));
                messageDTOs.Last().UserDTO = _mapper.Map<UserDTO>(message.User);
                var reactionsByMessage = await _reactionService.GetAllByEntityAsync(messageDTOs.Last().Id, -1);
                messageDTOs.Last().TypeReactions = reactionsByMessage.ReactionEntities.Select(r => r.Reaction.TypeReaction.Name).ToList();
                var reaction = reactionsByMessage.ReactionEntities.Where(r => r.Reaction.UserId == userId).FirstOrDefault();
                if (reaction is not null)
                    messageDTOs.Last().TypeReaction = reaction.Reaction.TypeReaction.Name;
            }
            PageMessageDTO pageMessageDTO = new PageMessageDTO(pageMessage.CountAllMessages, pageMessage.PageCount, pageMessage.NumberPage, messageDTOs);
            return Ok(pageMessageDTO);
        }

        [Authorize]
        [HttpGet("{messageId:int}")]
        public async Task<IActionResult> Get(int messageId)
        {
            _logger.LogInformation($"Вызван метод Get");
            Message message = await _messageService.GetAsync(messageId);
            var userMessageDTO = _mapper.Map<UserMessageDTO>(message);
            return Ok(userMessageDTO);
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateMessage(Message message)
        {
            _logger.LogInformation($"Вызван метод UpdateMessage");
            _messageService.UpdateAsync(message);

            await _chatHub.Clients.Group(message.ChatRoomId.ToString()).SendAsync(
                "UpdateMessage",
                message.Id.ToString(),
                message.Msg,
                message.ChatRoomId.ToString()
            );

            return Ok();
        }

        [Authorize]
        [HttpDelete("{messageId:int}")]
        public async Task<IActionResult> Delete(int messageId, int chatId)
        {
            _logger.LogInformation($"Вызван метод Delete");
            
            await _messageService.DeleteAsync(messageId);

            await _chatHub.Clients.Group(chatId.ToString()).SendAsync(
                "DeleteMessage",
                messageId.ToString(),
                chatId.ToString()
            );

            return Ok();
        }
    }
}