using Microsoft.AspNetCore.Mvc;

namespace Goods.System.Social.Network.Microservice.Notification.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationPostController : ControllerBase
    {

        private readonly ILogger<NotificationPostController> _logger;

        public NotificationPostController(ILogger<NotificationPostController> logger)
        {
            _logger = logger;
        }
    }
}