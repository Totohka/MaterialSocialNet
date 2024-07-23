using AutoMapper;
using DomainModel.Entities.ViewModels;
using Goods.System.Social.Network.DomainModel.Entities;
using Goods.System.Social.Network.DomainServices.Interface;
using Goods.System.Social.Network.Microservice.Users.Entities.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace Goods.System.Social.Network.Microservice.Users.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscribeController : Controller
    {
        private readonly ISubscribeService _subscribeService;
        private readonly ILogger<SubscribeController> _logger;
        private readonly IMapper _mapper;

        public SubscribeController(ISubscribeService subscribeService, IMapper mapper, ILogger<SubscribeController> logger, ILoggerFactory loggerFactory)
        {
            _subscribeService = subscribeService;
            _mapper = mapper;
            _logger = logger;
        }

        [Authorize]
        [HttpGet]
        public async Task<List<UserDTO>> GetSubscribeByUser()
        {
            _logger.LogInformation($"Вызван метод GetSubscribeByUser");

            return new List<UserDTO>(); //нужно доделать
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddSubscribe(UserSubscribeViewModel userSubscribe)
        {
            _logger.LogInformation($"Вызван метод AddSubscribe");
            UserFriend userFriend = _mapper.Map<UserFriend>(userSubscribe);
            await _subscribeService.AddSubscribeAsync(userFriend);
            return Ok();
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteSubscribe(UserSubscribeViewModel userSubscribe)
        {
            _logger.LogInformation($"Вызван метод DeleteSubscribe");
            UserFriend userFriend = _mapper.Map<UserFriend>(userSubscribe);
            await _subscribeService.DeleteSubscribeAsync(userFriend);
            return Ok();
        }
    }
}
