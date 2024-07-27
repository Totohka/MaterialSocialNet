using AutoMapper;
using DomainServices.Chat.Interface;
using Goods.System.Social.Network.DomainModel.Entities;
using Goods.System.Social.Network.Microservice.Chats.Entities.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        [Authorize]
        [HttpPost]
        public IActionResult InvateUserInChat(ChatRoomUser chatRoomUser)
        {
            _logger.LogInformation("Вызван метод InvateUserInChat");
            _invateService.InvateUserInChatAsync(chatRoomUser);
            return Ok();
        }

        [Authorize]
        [HttpGet]
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

        [Authorize]
        [HttpDelete]
        public IActionResult Delete(ChatRoomUser chatRoomUser)
        {
            _logger.LogInformation($"Вызван метод Delete");
            _invateService.DeleteAsync(chatRoomUser);
            return Ok();
        }
    }
}