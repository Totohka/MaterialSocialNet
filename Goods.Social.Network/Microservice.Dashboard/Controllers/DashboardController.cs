using DomainModel.Entities.ViewModels;
using Goods.System.Social.Network.DomainModel.Entities.Dashboard;
using Goods.System.Social.Network.DomainServices.Interface;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace Microservice.Dashboard.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly ILogger<DashboardController> _logger;
        private readonly IDashboardService _dashboardService;

        public DashboardController(ILogger<DashboardController> logger, IDashboardService dashboardService)
        {
            _logger = logger;
            _dashboardService = dashboardService;
        }

        [HttpGet]
        public async Task<object> Get()
        {
            _logger.LogInformation($"Вызван метод Get");
            object result = new {
                count_users = await _dashboardService.GetCountUsersAsync(),
                day = await _dashboardService.GetCountVisitingToDayAsync(),
                month = await _dashboardService.GetCountVisitingToMounthAsync(),
                year = await _dashboardService.GetCountVisitingToYearAsync()
            };
            return result;
        }

        [HttpPost]
        public IActionResult Post(VisitViewModel visit)
        {
            _logger.LogInformation($"Вызван метод Post");

            _dashboardService.CreateNewVisit(new Visit() { Date = visit.date });
            return Ok();
        }
    }
}

