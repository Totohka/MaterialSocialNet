using AutoMapper;
using DomainModel.Entities.ViewModels;
using DomainServices.Comments.Interface;
using Goods.System.Social.Network.DomainModel.Entities;
using Goods.System.Social.Network.DomainServices.Interface;
using Goods.System.Social.Network.Microservice.Reaction.Entities.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System.Net;

namespace Goods.System.Social.Network.Microservice.Reaction.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReactionPostController : Controller
    {
        private readonly IReactionEntityService<ReactionPost> _reactionPostService;
        private readonly IMapper _mapper;
        private readonly ILogger<ReactionPostController> _logger;

        public ReactionPostController(ILogger<ReactionPostController> logger, 
            IMapper mapper,
            IReactionEntityService<ReactionPost> reactionPostService)
        {
            _logger = logger;
            _reactionPostService = reactionPostService;
            _mapper = mapper;
        }

        /// <summary>
        /// ��������� �������� �����
        /// </summary>
        /// <param name="entityId">Id �����</param>
        /// <returns></returns>
        /// <response code="200">������� �����</response>
        /// <response code="401">������ �����������</response>
        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> Get(int entityId)
        {
            _logger.LogInformation($"������ ����� Get");

            return Ok(await _reactionPostService.GetRatingForEntityAsync(entityId));
        }

        /// <summary>
        /// ��������� ������� �����
        /// </summary>
        /// <param name="entityId">Id �����</param>
        /// <param name="number">�������� ���������</param>
        /// <returns></returns>
        /// <response code="200">�������� ������ DTO</response>
        /// <response code="401">������ �����������</response>
        [Authorize]
        [HttpGet("all")]
        [ProducesResponseType(typeof(PageReactionEntityDTO<ReactionEntityDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetAllByPost(int entityId, int number = 0)
        {
            _logger.LogInformation($"������ ����� GetAllByPost");

            PageReactionEntity<ReactionPost> pageReactionEntity = await _reactionPostService.GetAllByEntityAsync(entityId, number);
            List<ReactionEntityDTO> reactionDTOs = new List<ReactionEntityDTO>();

            pageReactionEntity.ReactionEntities.ForEach(reactionPost => {
                reactionDTOs.Add(_mapper.Map<ReactionEntityDTO>(reactionPost));
            });
            var pageReactionEntityDTO = new PageReactionEntityDTO<ReactionEntityDTO>(pageReactionEntity.CountAllReactionEntities,
                                                                                      pageReactionEntity.PageCount,
                                                                                      pageReactionEntity.NumberPage,
                                                                                      reactionDTOs);
            return Ok(pageReactionEntityDTO);
        }

        /// <summary>
        /// �������� ������� �� ����
        /// </summary>
        /// <param name="reactionPostViewModel">������ ������� �����</param>
        /// <returns></returns>
        /// <response code="200">�� ��</response>
        /// <response code="401">������ �����������</response>
        [Authorize]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> Create(ReactionEntityViewModel reactionPostViewModel)
        {
            _logger.LogInformation($"������ ����� Create");

            await _reactionPostService.CreateAsync(reactionPostViewModel.EntityId, 
                                                   reactionPostViewModel.UserId, 
                                                   reactionPostViewModel.TypeReactionId);
            return Ok();
        }

        /// <summary>
        /// �������������� ������� �����
        /// </summary>
        /// <param name="reactionPostUpdateViewModel">������ ��� �������������� ������� �����</param>
        /// <returns></returns>
        /// <response code="200">�� ��</response>
        /// <response code="401">������ �����������</response>
        [Authorize]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> Update(ReactionEntityUpdateViewModel reactionPostUpdateViewModel)
        {
            _logger.LogInformation($"������ ����� Update");

            await _reactionPostService.UpdateAsync(reactionPostUpdateViewModel.EntityId, 
                                                   reactionPostUpdateViewModel.UserId, 
                                                   reactionPostUpdateViewModel.TypeReactionId, 
                                                   reactionPostUpdateViewModel.TypeReactionOldId);
            return Ok();
        }

        /// <summary>
        /// �������� ������� �����
        /// </summary>
        /// <param name="reactionPostViewModel">������ ������� �����</param>
        /// <returns></returns>
        /// <response code="200">�� ��</response>
        /// <response code="401">������ �����������</response>
        [Authorize]
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> Delete(ReactionEntityViewModel reactionPostViewModel)
        {
            _logger.LogInformation($"������ ����� Delete");

            await _reactionPostService.DeleteAsync(reactionPostViewModel.EntityId, 
                                                   reactionPostViewModel.UserId, 
                                                   reactionPostViewModel.TypeReactionId);
            return Ok();
        }
    }
}