using Goods.Social.Network.DAL;
using Goods.Social.Network.DomainModel.Entities;
using Goods.Social.Network.DomainServices.Interface;
using Goods.Social.Network.DomainServices.Realization;
using Goods.Social.Network.Web.API.Auth.ViewModel;
using Microsoft.AspNetCore.Authorization;
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
    public class ChatController : Controller
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [Authorize]
        [HttpPost]
        public async Task<bool> AddNewChat(string name)
        {
            return await _chatService.Create(name);
        }

        [Authorize]
        [HttpGet]
        public async Task<List<ChatRoom>> GetChats(int userId)
        {
            return await _chatService.GetChats(userId);
        }
    }
}