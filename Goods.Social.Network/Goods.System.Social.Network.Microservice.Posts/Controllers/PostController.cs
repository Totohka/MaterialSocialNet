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
using System.Net;

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
        /// <summary>
        /// ��������� �����
        /// </summary>
        /// <param name="id">Id �����</param>
        /// <returns></returns>
        /// <response code="200">���� DTO</response>
        /// <response code="401">������ �����������</response>
        [Authorize]
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(PostDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> Get(int id) 
        {
            _logger.LogInformation($"������ ����� Get");
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
        /// <summary>
        /// ��������� ������
        /// </summary>
        /// <param name="search">������ ������</param>
        /// <param name="userId">��������� ���� ����� ����� ������������ �����, ����� �� ���������� ��� ���������� ������ 0</param>
        /// <param name="number">�������� ���������</param>
        /// <returns></returns>
        /// <response code="200">�������� ������ DTO</response>
        /// <response code="401">������ �����������</response>
        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(PagePostDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetAll(string search = "", int userId = 0, int number = 0)
        {
            _logger.LogInformation($"������ ����� GetAll");

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
        /// <summary>
        /// �������� �����
        /// </summary>
        /// <param name="postWithImage">������ ��� �������� �����</param>
        /// <returns></returns>
        /// <response code="200">Id ���������� �����</response>
        /// <response code="401">������ �����������</response>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> CreatePost([FromForm] PostWithImageViewModel postWithImage)
        {
            _logger.LogInformation($"������ ����� CreatePost");
            if (postWithImage.UserId == int.Parse(HttpContext.User.FindFirst(claim => claim.Type == "id").Value))
            {
                Post post = _mapper.Map<Post>(postWithImage);
                int postId = await _postService.CreateAsync(post, postWithImage.Image);
                var pageAllUsers = await _userService.GetAllAsync("", -1, postWithImage.UserId, 1);
                var users = pageAllUsers.Users;
                foreach (var user in users)
                {
                    var msgRMQ = new { AuthorId = postWithImage.UserId, SubscriberId = user.Id, PostId = postId };
                    _notificationPostProducer.SendMessage(msgRMQ);
                }
                return Ok(postId);
            }
            else
            {
                _logger.LogInformation($"���������� �����");

                return StatusCode(401);
            }
        }
        /// <summary>
        /// �������� �����
        /// </summary>
        /// <param name="postWithImageUpdate">������ ��� �������������� �����</param>
        /// <returns></returns>
        /// <response code="200">�� ��</response>
        /// <response code="401">������ �����������</response>
        [Authorize]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> UpdatePost([FromForm] PostWithImageUpdateViewModel postWithImageUpdate)
        {
            _logger.LogInformation($"������ ����� UpdatePost");

            if (int.Parse(HttpContext.User.FindFirst(claim => claim.Type == "id").Value) == postWithImageUpdate.UserId)
            {
                Post post = _mapper.Map<Post>(postWithImageUpdate);
                await _postService.UpdateAsync(post, postWithImageUpdate.Image);
                return Ok();
            }
            else
            {
                _logger.LogInformation($"���������� �����");

                return StatusCode(401);
            }
        }

        /// <summary>
        /// �������� �����
        /// </summary>
        /// <param name="id">Id �����</param>
        /// <param name="userId">Id �����</param>
        /// <returns></returns>
        /// <response code="200">�� ��</response>
        /// <response code="401">������ �����������</response>
        [Authorize]
        [HttpDelete("{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        public IActionResult DeletePost(int userId, int id)
        {
            _logger.LogInformation($"������ ����� DeletePost");

            if (int.Parse(HttpContext.User.FindFirst(claim => claim.Type == "id").Value) == userId)
            {
                _postService.Delete(userId, id);
                return Ok();
            }
            else
            {
                _logger.LogInformation($"���������� �����");

                return StatusCode(401);
            }
        }
    }
}