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
using System.Net;

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

        /// <summary>
        /// �������� ���������
        /// </summary>
        /// <param name="messageViewModel">������ ����� ���� � �����</param>
        /// <returns></returns>
        /// <response code="200">�� ��</response>
        /// <response code="401">������ �����������</response>
        [Authorize]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> AddNewMessage(MessageViewModel messageViewModel)
        {
            _logger.LogInformation($"������ ����� AddNewMessage");
            Message message = _mapper.Map<Message>(messageViewModel);
            int idMessage = await _messageService.CreateAsync(message);
            List<User> usersByChat = await _inviteService.GetUsersInChatAsync(messageViewModel.ChatRoomId);
            usersByChat.Remove(usersByChat.Where(u => u.Id == messageViewModel.UserId).First());

            _logger.LogInformation($"������ ����� AddNewMessage RMQ");

            foreach (var user in usersByChat)
            {
                var msgRMQ = new { ChatRoomId = messageViewModel.ChatRoomId, UserId = user.Id };
                _notificationChatRoomProducer.SendMessage(msgRMQ);
            }
            _logger.LogInformation($"������ ����� AddNewMessage signalR");

            await _chatHub.Clients.Group(messageViewModel.ChatRoomId.ToString()).SendAsync(
                "Receive", 
                messageViewModel.ChatRoomId.ToString(), 
                messageViewModel.Message, 
                messageViewModel.FirstName, 
                messageViewModel.LastName, 
                messageViewModel.UserId.ToString(), 
                messageViewModel.DateSend.ToString("s"), 
                idMessage.ToString()
            );

            _logger.LogInformation($"������������� ����� AddNewMessage");
            return Ok();
        }

        /// <summary>
        /// ��������� �������� ���������
        /// </summary>
        /// <param name="chatId">Id ����</param>
        /// <param name="number">�������� ���������, �� ��������� ����� 0</param>
        /// <returns></returns>
        /// <response code="200">Id ���������� ����</response>
        /// <response code="401">������ �����������</response>
        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(PageMessageDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetMessageByChat(int chatId, int number = 0)
        {
            _logger.LogInformation($"������ ����� GetMessageByChat");
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

        /// <summary>
        /// ��������� ���������
        /// </summary>
        /// <param name="id">Id ���������</param>
        /// <returns></returns>
        /// <response code="200">������ � ������� �� ���������</response>
        /// <response code="401">������ �����������</response>
        [Authorize]
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(UserMessageDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> Get(int id)
        {
            _logger.LogInformation($"������ ����� Get");
            Message message = await _messageService.GetAsync(id);
            var userMessageDTO = _mapper.Map<UserMessageDTO>(message);
            return Ok(userMessageDTO);
        }

        /// <summary>
        /// �������������� ���������
        /// </summary>
        /// <param name="messageUpdateViewModel">������ �������������� ���������</param>
        /// <returns></returns>
        /// <response code="200">�� ��</response>
        /// <response code="401">������ �����������</response>
        [Authorize]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> UpdateMessage(MessageUpdateViewModel messageUpdateViewModel)
        {
            _logger.LogInformation($"������ ����� UpdateMessage");

            var message = _mapper.Map<Message>(messageUpdateViewModel);

            await _messageService.UpdateAsync(message);
            message = await _messageService.GetAsync(message.Id);

            await _chatHub.Clients.Group(message.ChatRoomId.ToString()).SendAsync(
                "UpdateMessage",
                message.Id.ToString(),
                message.Msg,
                message.ChatRoomId.ToString()
            );

            return Ok();
        }

        /// <summary>
        /// �������� ���������
        /// </summary>
        /// <param name="id">Id ���������</param>
        /// <param name="chatId">Id ����</param>
        /// <returns></returns>
        /// <response code="200">Id ���������� ����</response>
        /// <response code="401">������ �����������</response>
        [Authorize]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, int chatId)
        {
            _logger.LogInformation($"������ ����� Delete");
            
            await _messageService.DeleteAsync(id);

            await _chatHub.Clients.Group(chatId.ToString()).SendAsync(
                "DeleteMessage",
                id.ToString(),
                chatId.ToString()
            );

            return Ok();
        }
    }
}