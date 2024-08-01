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
        /// Добавление человека в чат
        /// </summary>
        /// <param name="chatRoomUserViewModel">Модель связи чата и юзера</param>
        /// <returns></returns>
        /// <response code="200">Всё ок</response>
        /// <response code="401">Ошибка авторизации</response>
        [Authorize]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        public IActionResult InvateUserInChat(ChatRoomUserViewModel chatRoomUserViewModel)
        {
            _logger.LogInformation("Вызван метод InvateUserInChat");
            var chatRoomUser = _mapper.Map<ChatRoomUser>(chatRoomUserViewModel);
            _invateService.InvateUserInChatAsync(chatRoomUser);
            return Ok();
        }

        /// <summary>
        /// Получение юзеров в чате
        /// </summary>
        /// <param name="chatId">Id чата</param>
        /// <returns></returns>
        /// <response code="200">Id созданного чата</response>
        /// <response code="401">Ошибка авторизации</response>
        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(List<UserDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetUsersInChat(int chatId)
        {
            _logger.LogInformation($"Вызван метод GetUserInChat");
            List<User> listUsers = await _invateService.GetUsersInChatAsync(chatId);
            List<UserDTO> userDTOs = new List<UserDTO>();
            listUsers.ForEach(user =>
            {
                userDTOs.Add(_mapper.Map<UserDTO>(user));
            });
            return Ok(userDTOs);
        }

        /// <summary>
        /// Удаление юзера из чата
        /// </summary>
        /// <param name="chatRoomUserViewModel">Модель связи чата и юзера</param>
        /// <returns></returns>
        /// <response code="200">Id созданного чата</response>
        /// <response code="401">Ошибка авторизации</response>
        [Authorize]
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        public IActionResult Delete(ChatRoomUserViewModel chatRoomUserViewModel)
        {
            _logger.LogInformation($"Вызван метод Delete");

            var chatRoomUser = _mapper.Map<ChatRoomUser>(chatRoomUserViewModel);
            _invateService.DeleteAsync(chatRoomUser);
            return Ok();
        }
    }
}