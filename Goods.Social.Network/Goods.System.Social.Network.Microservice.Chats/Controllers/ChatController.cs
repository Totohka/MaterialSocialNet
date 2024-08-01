using AutoMapper;
using DomainModel.Entities.ViewModels;
using DomainServices.Chat.Interface;
using Goods.System.Social.Network.DomainModel.Entities;
using Goods.System.Social.Network.Microservice.Chats.Entities.DTO;
using Goods.System.Social.Network.Microservice.Chats.Entities.ViewModels;
using Goods.System.Social.Network.Microservice.Chats.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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
        private readonly IMapper _mapper;

        public ChatController(IChatService chatService, 
                              ILogger<ChatController> logger, 
                              IInviteService inviteService, 
                              IMessageService messageService,
                              INotificationChatRoomProducer notificationChatRoomProducer,
                              IMapper mapper)
        {
            _chatService = chatService;
            _logger = logger;
            _inviteService = inviteService;
            _messageService = messageService;
            _notificationChatRoomProducer = notificationChatRoomProducer;
            _mapper = mapper;
        }

        /// <summary>
        /// �������� �����
        /// </summary>
        /// <param name="chatRoomViewModel">������ ��� �������� ����</param>
        /// <returns></returns>
        /// <response code="200">Id ���������� ����</response>
        /// <response code="401">������ �����������</response>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> AddNewChat(ChatRoomViewModel chatRoomViewModel)
        {   
            _logger.LogInformation($"������ ����� AddNewChat");
            ChatRoom chatRoom = _mapper.Map<ChatRoom>(chatRoomViewModel);
            chatRoom.LastMessage = "��� ����";
            int idNewChat = await _chatService.CreateAsync(chatRoom, chatRoomViewModel.UserId);
            await _messageService.CreateAsync(new Message()
            {
                Msg = "��� ������",
                ChatRoomId = idNewChat,
                DateSend = DateTime.Now,
                UserId = chatRoomViewModel.UserId
            });

            foreach (var userIdByChat in chatRoomViewModel.UserIdsByChat)
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

        /// <summary>
        /// ��������� �����
        /// </summary>
        /// <param name="search">����� �� �������� �����</param>
        /// <param name="number">�������� ��� ���������, �� ��������� ����� 0</param>
        /// <returns></returns>
        /// <response code="200">��� ������</response>
        /// <response code="401">������ �����������</response>
        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(PageChatDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetChats(string search = "", int number = 0)
        {
            _logger.LogInformation("������ ����� GetChats");
            int id = int.Parse(HttpContext.User.FindFirst(claim => claim.Type == "id").Value);
            PageChat pageChat = await _chatService.GetChatsAsync(id, number, search);
            PageChatDTO pageChatDTO = new PageChatDTO();

            pageChatDTO.NumberPage = pageChat.NumberPage;
            pageChatDTO.CountAllChats = pageChat.CountAllChats;
            pageChatDTO.PageCount = pageChat.PageCount;

            List<ChatRoomDTO> chatRoomDTOs = new List<ChatRoomDTO>();

            foreach (var chatRoom in pageChat.ChatRooms)
            {
                chatRoomDTOs.Add(_mapper.Map<ChatRoomDTO>(chatRoom));
            }
            pageChatDTO.ChatRoomDTOs = chatRoomDTOs;

            return Ok(pageChatDTO);
        }

        /// <summary>
        /// ��������� ����
        /// </summary>
        /// <param name="id">Id ����</param>
        /// <returns></returns>
        /// <response code="200">������ ����</response>
        /// <response code="401">������ �����������</response>
        [Authorize]
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(ChatRoomDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> Get(int id)
        {
            _logger.LogInformation($"������ ����� Get");
            var chatRoomDTO = _mapper.Map<ChatRoomDTO>(await _chatService.GetAsync(id));
            return Ok(chatRoomDTO);
        }

        /// <summary>
        /// �������������� ����
        /// </summary>
        /// <param name="chatRoomUpdateViewModel">������ ��� �������� ����</param>
        /// <returns></returns>
        /// <response code="200">��� ������</response>
        /// <response code="401">������ �����������</response>
        [Authorize]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        public IActionResult UpdateChat(ChatRoomUpdateViewModel chatRoomUpdateViewModel)
        {
            _logger.LogInformation($"������ ����� Update");
            ChatRoom chatRoom = _mapper.Map<ChatRoom>(chatRoomUpdateViewModel);
            _chatService.UpdateAsync(chatRoom);
            return Ok();
        }

        /// <summary>
        /// �������� ����
        /// </summary>
        /// <param name="id">Id ����</param>
        /// <returns></returns>
        /// <response code="200">��� �����</response>
        /// <response code="401">������ �����������</response>
        [Authorize]
        [HttpDelete("{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        public IActionResult Delete(int id)
        {
            _logger.LogInformation($"������ ����� Delete");
            _chatService.Delete(id);
            return Ok();
        }
    }
}