using Goods.System.Social.Network.DomainModel.Entities;
using Goods.System.Social.Network.DomainServices.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Net;
using Azure.Core;

namespace Goods.System.Social.Network.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserInformationController : Controller
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserInformationController> _logger;

        public UserInformationController(IUserService userService, ILogger<UserInformationController> logger, ILoggerFactory loggerFactory)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpGet("/users")]
        public async Task<object> InformationAboutAnotherUsers()
        {

            var users = await _userService.GetAllUsers();
            var json = Json(users);
            return json;
        }


        //[HttpPost("/token")]
        //public async Task<object> Token(string email, string password)
        //{
        //    var response = await _userService.Token(email, password);
        //    return Json(response);
        //}

    }
}
