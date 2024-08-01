using DomainModel.Entities.ViewModels;
using Goods.System.Social.Network.DomainServices.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System.Net;

namespace Goods.System.Social.Network.Microservice.Users.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BackgroundController : ControllerBase
    {
        private readonly IBackgroundService _backgroundService;
        private readonly IJWTService _jwtService;
        private readonly ILogger<BackgroundController> _logger;

        public BackgroundController(IBackgroundService backgroundService, IJWTService jWTService, ILogger<BackgroundController> logger, ILoggerFactory loggerFactory)
        {
            _backgroundService = backgroundService;
            _logger = logger;
            _jwtService = jWTService;
        }

        /// <summary>
        /// Получение пути до фона
        /// </summary>
        /// <param name="userId">Id юзера</param>
        /// <returns></returns>
        /// <response code="200">Путь до фона</response>
        /// <response code="401">Ошибка авторизации</response>
        [Authorize]
        [HttpGet("{userId:int}")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        public IActionResult Get(int userId)
        {
            _logger.LogInformation($"Вызван метод Get");

            return Ok(_backgroundService.Get(userId));
        }

        /// <summary>
        /// Создание фона
        /// </summary>
        /// <param name="backgroundViewModel">Модель для фона</param>
        /// <returns></returns>
        /// <response code="200">Новый JWT токен</response>
        /// <response code="401">Ошибка авторизации</response>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> Create([FromForm] BackgroundViewModel backgroundViewModel)
        {
            _logger.LogInformation($"Вызван метод Create");

            if (int.Parse(HttpContext.User.FindFirst(claim => claim.Type == "id").Value) == backgroundViewModel.UserId)
            {
                await _backgroundService.CreateAsync(backgroundViewModel.Photo, backgroundViewModel.UserId);
                return Ok(await _jwtService.UpdateTokenAsync(backgroundViewModel.UserId));
            }
            else
            {
                _logger.LogInformation($"Устаревший токен");

                return StatusCode(401);
            }
        }

        /// <summary>
        /// Редактирование фона
        /// </summary>
        /// <param name="backgroundViewModel">Модель для фона</param>
        /// <returns></returns>
        /// <response code="200">Новый JWT токен</response>
        /// <response code="401">Ошибка авторизации</response>
        [Authorize]
        [HttpPut]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> Update([FromForm] BackgroundViewModel backgroundViewModel)
        {
            _logger.LogInformation($"Вызван метод Update");

            if (int.Parse(HttpContext.User.FindFirst(claim => claim.Type == "id").Value) == backgroundViewModel.UserId)
            {
                await _backgroundService.UpdateAsync(backgroundViewModel.Photo, backgroundViewModel.UserId);
                return Ok(await _jwtService.UpdateTokenAsync(backgroundViewModel.UserId));
            }
            else
            {
                _logger.LogInformation($"Устаревший токен");

                return StatusCode(401);
            }
            
        }

        /// <summary>
        /// Удаление фона
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
                _backgroundService.Delete(userId);
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
