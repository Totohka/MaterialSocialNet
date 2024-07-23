using Goods.System.Social.Network.DomainServices.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Goods.System.Social.Network.Microservice.Notifications.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : Controller
    {
        private readonly INotificationService _notificationService;
        public WeatherForecastController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }
        [HttpPost]
        public async Task Pos(int userId)
        {
            await _notificationService.CreateAsync(userId);
        }
    }
}