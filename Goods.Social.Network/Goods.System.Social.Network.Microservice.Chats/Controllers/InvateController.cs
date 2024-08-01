using AutoMapper;
using DomainServices.Chat.Interface;
using Goods.System.Social.Network.DomainModel.Entities;
using Goods.System.Social.Network.Microservice.Chats.Entities.DTO;
using Goods.System.Social.Network.Microservice.Chats.Entities.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Goods.System.Social.Network.Microservice.Chats.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvateController : Controller
    {
        private readonly IInviteService _invateService;
        private readonly IMapper _mapper;
        private readonly ILogger<InvateController> _logger;

        public InvateController(IInviteService invateService, IMapper mapper, ILogger<InvateController> logger)
        {
            _invateService = invateService;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// ���������� �������� � ���
        /// </summary>
        /// <param name="chatRoomUserViewModel">������ ����� ���� � �����</param>
        /// <returns></returns>
        /// <response code="200">�� ��</response>
        /// <response code="401">������ �����������</response>
        [Authorize]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        public IActionResult InvateUserInChat(ChatRoomUserViewModel chatRoomUserViewModel)
        {
            _logger.LogInformation("������ ����� InvateUserInChat");
            var chatRoomUser = _mapper.Map<ChatRoomUser>(chatRoomUserViewModel);
            _invateService.InvateUserInChatAsync(chatRoomUser);
            return Ok();
        }

        /// <summary>
        /// ��������� ������ � ����
        /// </summary>
        /// <param name="chatId">Id ����</param>
        /// <returns></returns>
        /// <response code="200">Id ���������� ����</response>
        /// <response code="401">������ �����������</response>
        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(List<UserDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetUsersInChat(int chatId)
        {
            _logger.LogInformation($"������ ����� GetUserInChat");
            List<User> listUsers = await _invateService.GetUsersInChatAsync(chatId);
            List<UserDTO> userDTOs = new List<UserDTO>();
            listUsers.ForEach(user =>
            {
                userDTOs.Add(_mapper.Map<UserDTO>(user));
            });
            return Ok(userDTOs);
        }

        /// <summary>
        /// �������� ����� �� ����
        /// </summary>
        /// <param name="chatRoomUserViewModel">������ ����� ���� � �����</param>
        /// <returns></returns>
        /// <response code="200">Id ���������� ����</response>
        /// <response code="401">������ �����������</response>
        [Authorize]
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        public IActionResult Delete(ChatRoomUserViewModel chatRoomUserViewModel)
        {
            _logger.LogInformation($"������ ����� Delete");

            var chatRoomUser = _mapper.Map<ChatRoomUser>(chatRoomUserViewModel);
            _invateService.DeleteAsync(chatRoomUser);
            return Ok();
        }
    }
}