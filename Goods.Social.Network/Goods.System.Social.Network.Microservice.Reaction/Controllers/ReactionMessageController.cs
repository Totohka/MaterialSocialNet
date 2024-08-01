using AutoMapper;
using DomainModel.Entities.ViewModels;
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
    public class ReactionMessageController : Controller
    {
        private readonly IReactionEntityService<ReactionMessage> _reactionMessageService;
        private readonly IMapper _mapper;
        private readonly ILogger<ReactionMessageController> _logger;

        public ReactionMessageController(ILogger<ReactionMessageController> logger, 
                                         IMapper mapper,
                                         IReactionEntityService<ReactionMessage> reactionMessageService)
        {
            _logger = logger;
            _mapper = mapper;
            _reactionMessageService = reactionMessageService;
        }

        /// <summary>
        /// ��������� ��������(��� ��������� ����� �� ������������)
        /// </summary>
        /// <param name="entityId">Id ���������</param>
        /// <returns></returns>
        /// <response code="200">������� ���������</response>
        /// <response code="401">������ �����������</response>
        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> Get(int entityId)
        {
            _logger.LogInformation($"������ ����� Get");
            return Ok(await _reactionMessageService.GetRatingForEntityAsync(entityId));
        }

        /// <summary>
        /// ��������� ������� �� ���������
        /// </summary>
        /// <param name="entityId">Id ���������</param>
        /// <param name="number">�������� ���������</param>
        /// <returns></returns>
        /// <response code="200">�������� ������� DTO �� ����</response>
        /// <response code="401">������ �����������</response>
        [Authorize]
        [HttpGet("all")]
        [ProducesResponseType(typeof(PageReactionEntityDTO<ReactionEntityDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetAllByMessage(int entityId, int number = 0)
        {
            _logger.LogInformation($"������ ����� GetAll");

            PageReactionEntity<ReactionMessage> pageReactionEntity = await _reactionMessageService.GetAllByEntityAsync(entityId, number);
            List<ReactionEntityDTO> reactionDTOs = new List<ReactionEntityDTO>();

            pageReactionEntity.ReactionEntities.ForEach(reactionMessage => {
                reactionDTOs.Add(_mapper.Map<ReactionEntityDTO>(reactionMessage));
            });
            var pageReactionEntityDTO = new PageReactionEntityDTO<ReactionEntityDTO>(pageReactionEntity.CountAllReactionEntities,
                                                                                      pageReactionEntity.PageCount, 
                                                                                      pageReactionEntity.NumberPage, 
                                                                                      reactionDTOs);
            return Ok(pageReactionEntityDTO);
        }

        /// <summary>
        /// �������� ������� �� ���������
        /// </summary>
        /// <param name="reactionMessageViewModel">������ �������</param>
        /// <returns></returns>
        /// <response code="200">�� ��</response>
        /// <response code="401">������ �����������</response>
        [Authorize]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> Create(ReactionEntityViewModel reactionMessageViewModel)
        {
            _logger.LogInformation($"������ ����� Create");

            await _reactionMessageService.CreateAsync(reactionMessageViewModel.EntityId,
                                                      reactionMessageViewModel.UserId,
                                                      reactionMessageViewModel.TypeReactionId);
            return Ok();
        }

        /// <summary>
        /// �������������� ������� �� ���������
        /// </summary>
        /// <param name="reactionMessageUpdateViewModel">������ ������� ��� ��������������</param>
        /// <returns></returns>
        /// <response code="200">�� ��</response>
        /// <response code="401">������ �����������</response>
        [Authorize]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> Update(ReactionEntityUpdateViewModel reactionMessageUpdateViewModel)
        {
            _logger.LogInformation($"������ ����� Update");

            await _reactionMessageService.UpdateAsync(reactionMessageUpdateViewModel.EntityId,
                                                      reactionMessageUpdateViewModel.UserId,
                                                      reactionMessageUpdateViewModel.TypeReactionId,
                                                      reactionMessageUpdateViewModel.TypeReactionOldId);
            return Ok();
        }

        /// <summary>
        /// �������� ������� �� ���������
        /// </summary>
        /// <param name="reactionMessageViewModel">������ �������</param>
        /// <returns></returns>
        /// <response code="200">�� ��</response>
        /// <response code="401">������ �����������</response>
        [Authorize]
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> Delete(ReactionEntityViewModel reactionMessageViewModel)
        {
            _logger.LogInformation($"������ ����� Delete");

            await _reactionMessageService.DeleteAsync(reactionMessageViewModel.EntityId,
                                                      reactionMessageViewModel.UserId,
                                                      reactionMessageViewModel.TypeReactionId);
            return Ok();
        }
    }
}