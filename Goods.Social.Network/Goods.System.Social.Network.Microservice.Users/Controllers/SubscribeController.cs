using AutoMapper;
using DomainModel.Entities.ViewModels;
using Goods.System.Social.Network.DomainModel.Entities;
using Goods.System.Social.Network.DomainServices.Interface;
using Goods.System.Social.Network.Microservice.Users.Entities.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System.Net;

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

        /// <summary>
        /// Получение подписчиков юзера
        /// Сейчас не работает
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Список юзеров</response>
        /// <response code="401">Ошибка авторизации</response>
        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        public async Task<List<UserDTO>> GetSubscribeByUser()
        {
            _logger.LogInformation($"Вызван метод GetSubscribeByUser");

            return new List<UserDTO>(); //нужно доделать
        }

        /// <summary>
        /// Получение типа реакции
        /// </summary>
        /// <param name="userSubscribe">Модель подписки на юзера</param>
        /// <returns></returns>
        /// <response code="200">Всё ок</response>
        /// <response code="401">Ошибка авторизации</response>
        [Authorize]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> AddSubscribe(UserSubscribeViewModel userSubscribe)
        {
            _logger.LogInformation($"Вызван метод AddSubscribe");
            UserFriend userFriend = _mapper.Map<UserFriend>(userSubscribe);
            await _subscribeService.AddSubscribeAsync(userFriend);
            return Ok();
        }

        /// <summary>
        /// Получение типа реакции
        /// </summary>
        /// <param name="userSubscribe">Модель подписки на юзера</param>
        /// <returns></returns>
        /// <response code="200">Всё ок</response>
        /// <response code="401">Ошибка авторизации</response>
        [Authorize]
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> DeleteSubscribe(UserSubscribeViewModel userSubscribe)
        {
            _logger.LogInformation($"Вызван метод DeleteSubscribe");
            UserFriend userFriend = _mapper.Map<UserFriend>(userSubscribe);
            await _subscribeService.DeleteSubscribeAsync(userFriend);
            return Ok();
        }
    }
}
