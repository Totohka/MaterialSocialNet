using DomainModel.Entities.ViewModels;
using Goods.System.Social.Network.DomainServices.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;

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

        [Authorize]
        [HttpGet("{userId:int}")]
        public IActionResult Get(int userId)
        {
            _logger.LogInformation($"Вызван метод Get");

            return Ok(_backgroundService.Get(userId));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] BackgroundViewModel backgroundViewModel)
        {
            _logger.LogInformation($"Вызван метод Create");

            if (int.Parse(HttpContext.User.FindFirst(claim => claim.Type == "id").Value) == backgroundViewModel.user_id)
            {
                await _backgroundService.CreateAsync(backgroundViewModel.photo, backgroundViewModel.user_id);
                return Ok(await _jwtService.UpdateTokenAsync(backgroundViewModel.user_id));
            }
            else
            {
                _logger.LogInformation($"Устаревший токен");

                return StatusCode(401);
            }
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Update([FromForm] BackgroundViewModel backgroundViewModel)
        {
            _logger.LogInformation($"Вызван метод Update");

            if (int.Parse(HttpContext.User.FindFirst(claim => claim.Type == "id").Value) == backgroundViewModel.user_id)
            {
                await _backgroundService.UpdateAsync(backgroundViewModel.photo, backgroundViewModel.user_id);
                return Ok(await _jwtService.UpdateTokenAsync(backgroundViewModel.user_id));
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
