using DomainModel.Entities.ViewModels;
using Goods.System.Social.Network.DomainServices.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using NLog;

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

        [Authorize]
        [HttpGet("{userId:int}")]
        public IActionResult Get(int userId)
        {
            _logger.LogInformation($"Вызван метод Create");

            return Ok(_avatarService.Get(userId));
        }

        [Authorize]
        [HttpPost]
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

        [Authorize]
        [HttpPut]
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

        [Authorize]
        [HttpDelete("{userId:int}")]
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
