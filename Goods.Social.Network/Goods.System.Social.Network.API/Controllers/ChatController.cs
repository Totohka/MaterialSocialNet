using Goods.System.Social.Network.DomainModel.Entities;
using Goods.System.Social.Network.DomainServices.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Goods.System.Social.Network.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : Controller
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        //[Authorize]
        [HttpPost("/createChat")]
        public async Task<bool> AddNewChat(string name)
        {
            return await _chatService.Create(name);
        }


        [HttpGet("/getChats")]
        public async Task<List<ChatRoom>> GetChats(int userId)
        {
            return await _chatService.GetChats(userId);
        }

        //[Route("getMessages")]
        //[Authorize]
        [HttpGet("/getMessages")]
        public async Task<List<Message>> GetMessages(int number, int chatId)
        {
            return await _chatService.GetFifthMessage(number, chatId);
        }

        //[Route("test")]
        //[Authorize]
        [HttpGet("/test")]
        public async Task Test(string message, int userId, int chatId)
        {
            await _chatService.SaveMessage(message, userId, chatId);
        }
    }
}