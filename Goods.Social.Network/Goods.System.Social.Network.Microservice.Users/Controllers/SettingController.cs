using AutoMapper;
using DomainModel.Entities.ViewModels;
using Goods.System.Social.Network.DomainModel.Entities;
using Goods.System.Social.Network.DomainServices.Interface;
using Goods.System.Social.Network.DomainServices.Realization;
using Goods.System.Social.Network.Microservice.Users.Entities.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System.Net;
using System.Text.Json;

namespace Goods.System.Social.Network.Microservice.Users.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SettingController : Controller
    {
        private readonly IUserService _userService;
        private readonly ILogger<SettingController> _logger;
        private readonly IJWTService _jwtService;
        private readonly IMapper _mapper;

        public SettingController(IUserService userService, IMapper mapper, ILogger<SettingController> logger, ILoggerFactory loggerFactory, IJWTService jwtService)
        {
            _userService = userService;
            _logger = logger;
            _mapper = mapper;
            _jwtService = jwtService;
        }

        /// <summary>
        /// Изменение параметров приватности
        /// </summary>
        /// <param name="changeSettingPrivacy">Модель изменения параметров приватности</param>
        /// <returns></returns>
        /// <response code="200">Новый JWT токен</response>
        /// <response code="401">Ошибка авторизации</response>
        [Authorize]
        [HttpPut("privacy")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> ChangeSettingPrivacy(ChangeSettingPrivacyViewModel changeSettingPrivacy)
        {
            _logger.LogInformation($"Вызван метод ChangeSettingPrivacy");

            if (changeSettingPrivacy.UserId == int.Parse(HttpContext.User.FindFirst(claim => claim.Type == "id").Value))
            {
                var settingPrivacy = _mapper.Map<SettingPrivacy>(changeSettingPrivacy);
                await _userService.ChangeSettingPrivacyAsync(settingPrivacy, changeSettingPrivacy.UserId);
                return Ok(await _jwtService.UpdateTokenAsync(changeSettingPrivacy.UserId));
            }
            else
            {
                _logger.LogInformation($"Устаревший токен");

                return StatusCode(401);
            }

            
        }

        /// <summary>
        /// Изменение параметров уведомлений
        /// </summary>
        /// <param name="changeSettingNotification">Модель изменения параметров уведомлений</param>
        /// <returns></returns>
        /// <response code="200">Новый JWT токен</response>
        /// <response code="401">Ошибка авторизации</response>
        [Authorize]
        [HttpPut("notification")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> ChangeSettingNotification(ChangeSettingNotificationViewModel changeSettingNotification)
        {
            _logger.LogInformation($"Вызван метод ChangeSettingNotification");

            if (changeSettingNotification.UserId == int.Parse(HttpContext.User.FindFirst(claim => claim.Type == "id").Value))
            {
                var settingNotification = _mapper.Map<SettingNotification>(changeSettingNotification);
                await _userService.ChangeSettingNotificationAsync(settingNotification, changeSettingNotification.UserId);
                return Ok(await _jwtService.UpdateTokenAsync(changeSettingNotification.UserId));
            }
            else
            {
                _logger.LogInformation($"Устаревший токен");

                return StatusCode(401);
            }
        }
    }
}
