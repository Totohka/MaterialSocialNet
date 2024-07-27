
using AutoMapper;
using DomainServices.Comments.Interface;
using Goods.System.Social.Network.DomainModel.Entities;
using Goods.System.Social.Network.Microservice.Comments.Entities.DTO;
using Goods.System.Social.Network.Microservice.Comments.Entities.ViewModel;
using Goods.System.Social.Network.Microservice.Comments.Infrastructure.Mapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Goods.System.Social.Network.Microservice.Comments.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CommentPostController : Controller
    {
        private readonly ILogger<CommentPostController> _logger;
        private readonly ICommentEntityService<CommentPost> _commentPostService;
        private readonly IMapper _mapper;

        public CommentPostController(ILogger<CommentPostController> logger,
                                     ICommentEntityService<CommentPost> commentPostService,
                                     IMapper mapper)
        {
            _logger = logger;
            _commentPostService = commentPostService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetByPost(int postId)
        {
            var commentsByPost = await _commentPostService.GetAllByEntityAsync(postId);
            List<CommentPostDTO> comments = new List<CommentPostDTO>();
            foreach (var comment in commentsByPost)
            {
                comments.Add(_mapper.Map<CommentPostDTO>(comment));
            }
            return Ok(comments);
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment(CommentPostViewModel commentPostViewModel)
        {
            await _commentPostService.CreateAsync(commentPostViewModel.post_id, commentPostViewModel.user_id, commentPostViewModel.text);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateComment(CommentPostUpdateViewModel commentPostUpdateViewModel)
        {
            await _commentPostService.UpdateAsync(commentPostUpdateViewModel.id, commentPostUpdateViewModel.text);
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            await _commentPostService.DeleteAsync(id);
            return Ok();
        }
    }
}