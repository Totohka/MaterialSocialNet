using AutoMapper;
using DomainModel.Entities.ViewModels;
using DomainServices.Comments.Interface;
using Goods.System.Social.Network.DomainModel.Entities;
using Goods.System.Social.Network.DomainServices.Interface;
using Goods.System.Social.Network.Microservice.Posts.Entities.DTO;
using Goods.System.Social.Network.Microservice.Posts.Entities.Pagination;
using Goods.System.Social.Network.Microservice.Posts.Entities.ViewModel;
using Goods.System.Social.Network.Microservice.Posts.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using NLog;

namespace Goods.System.Social.Network.Microservice.Posts.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : Controller
    {
        private readonly ILogger<PostController> _logger;
        private readonly IReactionEntityService<ReactionPost> _reactionPostService;
        private readonly IPostService _postService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly INotificationPostProducer _notificationPostProducer;
        public PostController(ILogger<PostController> logger, 
                              IUserService userService, 
                              IMapper mapper, 
                              IReactionEntityService<ReactionPost> reactionPostService, 
                              IPostService postService,
                              INotificationPostProducer notificationPostProducer)
        {
            _userService = userService;
            _logger = logger;
            _postService = postService;
            _mapper = mapper;
            _reactionPostService = reactionPostService;
            _notificationPostProducer = notificationPostProducer;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get(int id) 
        {
            _logger.LogInformation($"Вызван метод Get");
            int userId = int.Parse(HttpContext.User.FindFirst(claim => claim.Type == "id").Value);

            var post = await _postService.GetAsync(id);
            PostDTO postDTO = _mapper.Map<PostDTO>(post);
            var user = await _userService.GetAsync(post.UserId);
            postDTO.FirstNameUser = user.FirstName;
            postDTO.LastNameUser = user.LastName;
            postDTO.Rating = await _reactionPostService.GetRatingForEntityAsync(post.Id);
            var reactionsByPost = await _reactionPostService.GetAllByEntityAsync(post.Id, -1);
            var reaction = reactionsByPost.ReactionEntities.Where(r => r.Reaction.UserId == userId).FirstOrDefault();
            if (reaction is not null)
                postDTO.TypeReaction = reaction.Reaction.TypeReaction.Name;
            else postDTO.TypeReaction = "";
            return Ok(postDTO);
        }

        [Authorize]
        [HttpGet("all")]
        public async Task<IActionResult> GetAll(string search = "", int userId = 0, int number = 0)
        {
            _logger.LogInformation($"Вызван метод GetAll");

            int id = int.Parse(HttpContext.User.FindFirst(claim => claim.Type == "id").Value);
            PagePost pagePost = await _postService.GetAllAsync(search, userId, number);
            List<PostDTO> postDTOs = new List<PostDTO>();
            foreach (var post in pagePost.Posts)
            {
                postDTOs.Add(_mapper.Map<PostDTO>(post));
                var user = await _userService.GetAsync(post.UserId);
                postDTOs.Last().FirstNameUser = user.FirstName;
                postDTOs.Last().LastNameUser = user.LastName;
            }
            foreach (var post in postDTOs)
            {
                post.Rating = await _reactionPostService.GetRatingForEntityAsync(post.Id);
                var reactionsByPost = await _reactionPostService.GetAllByEntityAsync(post.Id, -1);
                var reaction = reactionsByPost.ReactionEntities.Where(r => r.Reaction.UserId == id).FirstOrDefault();
                if (reaction is not null)
                    post.TypeReaction = reaction.Reaction.TypeReaction.Name;
                else post.TypeReaction = "";
            }
            PagePostDTO pagePostDTO = new PagePostDTO(pagePost.CountAllPosts, pagePost.PageCount, pagePost.NumberPage, postDTOs);
            return Ok(pagePostDTO);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreatePost([FromForm] PostWithImage postWithImage)
        {
            _logger.LogInformation($"Вызван метод CreatePost");
            if (postWithImage.user_id == int.Parse(HttpContext.User.FindFirst(claim => claim.Type == "id").Value))
            {
                Post post = _mapper.Map<Post>(postWithImage);
                int postId = await _postService.CreateAsync(post, postWithImage.image);
                var pageAllUsers = await _userService.GetAllAsync("", -1, postWithImage.user_id, 1);
                var users = pageAllUsers.Users;
                foreach (var user in users)
                {
                    var msgRMQ = new { AuthorId = postWithImage.user_id, SubscriberId = user.Id, PostId = postId };
                    _notificationPostProducer.SendMessage(msgRMQ);
                }
                return Ok();
            }
            else
            {
                _logger.LogInformation($"Устаревший токен");

                return StatusCode(401);
            }
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdatePost([FromForm] PostWithImageUpdate postWithImageUpdate)
        {
            _logger.LogInformation($"Вызван метод UpdatePost");

            if (int.Parse(HttpContext.User.FindFirst(claim => claim.Type == "id").Value) == postWithImageUpdate.user_id)
            {
                Post post = _mapper.Map<Post>(postWithImageUpdate);
                await _postService.UpdateAsync(post, postWithImageUpdate.image);
                return Ok();
            }
            else
            {
                _logger.LogInformation($"Устаревший токен");

                return StatusCode(401);
            }
        }

        [Authorize]
        [HttpDelete]
        public IActionResult DeletePost(int userId, int postId)
        {
            _logger.LogInformation($"Вызван метод DeletePost");

            if (int.Parse(HttpContext.User.FindFirst(claim => claim.Type == "id").Value) == userId)
            {
                _postService.Delete(userId, postId);
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