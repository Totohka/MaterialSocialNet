using AutoMapper;
using DomainModel.Entities.ViewModels;
using Goods.System.Social.Network.DomainModel.Entities;
using Goods.System.Social.Network.DomainServices.Interface;
using Goods.System.Social.Network.DomainServices.Realization;
using Goods.System.Social.Network.Microservice.Users.Entities.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
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

        [Authorize]
        [HttpPut("privacy")]
        public async Task<IActionResult> ChangeSettingPrivacy(ChangeSettingPrivacyViewModel changeSettingPrivacy)
        {
            _logger.LogInformation($"Вызван метод ChangeSettingPrivacy");

            if (changeSettingPrivacy.user_id == int.Parse(HttpContext.User.FindFirst(claim => claim.Type == "id").Value))
            {
                var settingPrivacy = _mapper.Map<SettingPrivacy>(changeSettingPrivacy);
                await _userService.ChangeSettingPrivacyAsync(settingPrivacy, changeSettingPrivacy.user_id);
                return Ok(await _jwtService.UpdateTokenAsync(changeSettingPrivacy.user_id));
            }
            else
            {
                _logger.LogInformation($"Устаревший токен");

                return StatusCode(401);
            }

            
        }

        [Authorize]
        [HttpPut("notification")]
        public async Task<IActionResult> ChangeSettingNotification(ChangeSettingNotificationViewModel changeSettingNotification)
        {
            _logger.LogInformation($"Вызван метод ChangeSettingNotification");

            if (changeSettingNotification.user_id == int.Parse(HttpContext.User.FindFirst(claim => claim.Type == "id").Value))
            {
                var settingNotification = _mapper.Map<SettingNotification>(changeSettingNotification);
                await _userService.ChangeSettingNotificationAsync(settingNotification, changeSettingNotification.user_id);
                return Ok(await _jwtService.UpdateTokenAsync(changeSettingNotification.user_id));
            }
            else
            {
                _logger.LogInformation($"Устаревший токен");

                return StatusCode(401);
            }
        }
    }
}
