
using AutoMapper;
using DomainServices.Comments.Interface;
using Goods.System.Social.Network.DomainModel.Entities;
using Goods.System.Social.Network.Microservice.Comments.Entities.DTO;
using Goods.System.Social.Network.Microservice.Comments.Entities.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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

        /// <summary>
        /// ��������� ������������ ��� ������� �����
        /// </summary>
        /// <param name="postId">Id �����</param>
        /// <returns></returns>
        /// <response code="200">������ ������������ DTO</response>
        /// <response code="401">������ �����������</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<CommentPostDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
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

        /// <summary>
        /// �������� �����������
        /// </summary>
        /// <param name="commentPostViewModel">������ ��� �������� �����������</param>
        /// <returns></returns>
        /// <response code="200">Id ���������� ��������</response>
        /// <response code="401">������ �����������</response>
        [HttpPost]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> CreateComment(CommentPostViewModel commentPostViewModel)
        {
            int commentId = await _commentPostService.CreateAsync(commentPostViewModel.PostId, commentPostViewModel.UserId, commentPostViewModel.Text);
            return Ok(commentId);
        }

        /// <summary>
        /// �������������� �����������
        /// </summary>
        /// <param name="commentPostUpdateViewModel">������ ��� �������� ����</param>
        /// <returns></returns>
        /// <response code="200">�� ��</response>
        /// <response code="401">������ �����������</response>
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> UpdateComment(CommentPostUpdateViewModel commentPostUpdateViewModel)
        {
            await _commentPostService.UpdateAsync(commentPostUpdateViewModel.Id, commentPostUpdateViewModel.Text);
            return Ok();
        }

        /// <summary>
        /// �������� �����������
        /// </summary>
        /// <param name="id">Id �����������</param>
        /// <returns></returns>
        /// <response code="200">�� ��</response>
        /// <response code="401">������ �����������</response>
        [HttpDelete("{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> DeleteComment(int id)
        {
            await _commentPostService.DeleteAsync(id);
            return Ok();
        }
    }
}