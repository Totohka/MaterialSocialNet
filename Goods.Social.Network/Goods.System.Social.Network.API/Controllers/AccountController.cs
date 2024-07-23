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
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IUserService userService, ILogger<AccountController> logger, ILoggerFactory loggerFactory)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpPost("/Registration")]
        public async Task<object> Registration(JsonDocument jsonUser)
        {
            
            User user = await _userService.Registration(jsonUser);
            var response = await _userService.Token(user.Email, user.Password);
            var json = Json(response);
            var responce = $"Action => {nameof(HttpClient)}. New Name => {user.Email}. IP = {Request.HttpContext.Connection.RemoteIpAddress}";
            _logger.LogInformation(responce);
            return json;
        }


        [HttpPost("/token")]
        public async Task<object> Token(string email, string password)
        {
            var response = await _userService.Token(email, password);
            return Json(response);
        }

    }
}