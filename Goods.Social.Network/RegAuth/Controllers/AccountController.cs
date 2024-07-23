using Goods.Social.Network.DAL;
using Goods.Social.Network.DomainModel.Entities;
using Goods.Social.Network.DomainServices.Interface;
using Goods.Social.Network.DomainServices.Realization;
using Goods.Social.Network.Web.API.Auth.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;

namespace Goods.Social.Network.Web.API.Auth.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<bool> Registration(JsonDocument jsonUser)
        {
            return await _userService.Registration(jsonUser);
        }


        [HttpPost("/token")]
        public async Task<IActionResult> Token(string username, string password)
        {
            var response = await _userService.Token(username, password);
            return Json(response);
        }

    }
}