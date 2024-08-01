using DomainModel.Entities.ViewModels;
using Goods.System.Social.Network.DomainModel.Entities;
using Goods.System.Social.Network.DomainServices.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using NLog;
using System.Net;

namespace Goods.System.Social.Network.Microservice.Users.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvatarController : ControllerBase
    {
        private readonly IAvatarService _avatarService;
        private readonly IJWTService _jwtService;
        private readonly ILogger<AvatarController> _logger;

        public AvatarController(IAvatarService avatarService, IJWTService jWTService, ILogger<AvatarController> logger, ILoggerFactory loggerFactory)
        {
            _avatarService = avatarService;
            _logger = logger;
            _jwtService = jWTService;
        }

        /// <summary>
        /// Получение пути до авы
        /// </summary>
        /// <param name="userId">Id юзера</param>
        /// <returns></returns>
        /// <response code="200">Путь до авы</response>
        /// <response code="401">Ошибка авторизации</response>
        [Authorize]
        [HttpGet("{userId:int}")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        public IActionResult Get(int userId)
        {
            _logger.LogInformation($"Вызван метод Create");

            return Ok(_avatarService.Get(userId));
        }

        /// <summary>
        /// Создание авы
        /// </summary>
        /// <param name="avatarViewModel">Модель для авы</param>
        /// <returns></returns>
        /// <response code="200">Новый JWT токен</response>
        /// <response code="401">Ошибка авторизации</response>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> Create([FromForm] AvatarViewModel avatarViewModel)
        {
            _logger.LogInformation($"Вызван метод Create");

            if (int.Parse(HttpContext.User.FindFirst(claim => claim.Type == "id").Value) == avatarViewModel.UserId)
            {
                await _avatarService.CreateAsync(avatarViewModel.Photo, avatarViewModel.UserId);
                return Ok(await _jwtService.UpdateTokenAsync(avatarViewModel.UserId));
            }
            else
            {
                _logger.LogInformation($"Устаревший токен");

                return StatusCode(401);
            }
        }

        /// <summary>
        /// Редактирование авы
        /// </summary>
        /// <param name="avatarViewModel">Модель для авы</param>
        /// <returns></returns>
        /// <response code="200">Новый JWT токен</response>
        /// <response code="401">Ошибка авторизации</response>
        [Authorize]
        [HttpPut]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> Update([FromForm] AvatarViewModel avatarViewModel)
        {
            _logger.LogInformation($"Вызван метод Update");

            if (int.Parse(HttpContext.User.FindFirst(claim => claim.Type == "id").Value) == avatarViewModel.UserId)
            {
                await _avatarService.UpdateAsync(avatarViewModel.Photo, avatarViewModel.UserId);
                return Ok(await _jwtService.UpdateTokenAsync(avatarViewModel.UserId));
            }
            else
            {
                _logger.LogInformation($"Устаревший токен");

                return StatusCode(401);
            }
            
        }

        /// <summary>
        /// Удаление авы
        /// </summary>
        /// <param name="userId">Id юзера</param>
        /// <returns></returns>
        /// <response code="200">Всё ок</response>
        /// <response code="401">Ошибка авторизации</response>
        [Authorize]
        [HttpDelete("{userId:int}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        public IActionResult Delete(int userId)
        {
            _logger.LogInformation($"Вызван метод Delete");
            if (int.Parse(HttpContext.User.FindFirst(claim => claim.Type == "id").Value) == userId)
            {
                _avatarService.Delete(userId);
                return Ok();
            }
            else
            {
                _logger.LogInformation($"Устаревший токен");

                return StatusCode(401);
            }
        }
    }
}
