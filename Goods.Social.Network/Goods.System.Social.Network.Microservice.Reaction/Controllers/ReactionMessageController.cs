using AutoMapper;
using DomainModel.Entities.ViewModels;
using DomainServices.Comments.Interface;
using Goods.System.Social.Network.DomainModel.Entities;
using Goods.System.Social.Network.DomainServices.Interface;
using Goods.System.Social.Network.Microservice.Reaction.Entities.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace Goods.System.Social.Network.Microservice.Reaction.Controllers
{
    [ApiController]
    [Route("[controller]")]
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

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get(int messageId)
        {
            _logger.LogInformation($"Вызван метод Get");

            return Ok(await _reactionMessageService.GetRatingForEntityAsync(messageId));
        }

        [Authorize]
        [HttpGet("all")]
        public async Task<IActionResult> GetAllByMessage(int messageId, int number = 0)
        {
            _logger.LogInformation($"Вызван метод GetAll");

            PageReactionEntity<ReactionMessage> pageReactionEntity = await _reactionMessageService.GetAllByEntityAsync(messageId, number);
            List<ReactionMessageDTO> reactionDTOs = new List<ReactionMessageDTO>();

            pageReactionEntity.ReactionEntities.ForEach(reactionMessage => {
                reactionDTOs.Add(_mapper.Map<ReactionMessageDTO>(reactionMessage));
            });
            var pageReactionEntityDTO = new PageReactionEntityDTO<ReactionMessageDTO>(pageReactionEntity.CountAllReactionEntities,
                                                                                      pageReactionEntity.PageCount, 
                                                                                      pageReactionEntity.NumberPage, 
                                                                                      reactionDTOs);
            return Ok(pageReactionEntityDTO);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(ReactionEntityViewModel reactionMessageViewModel)
        {
            _logger.LogInformation($"Вызван метод Create");

            await _reactionMessageService.CreateAsync(reactionMessageViewModel.entity_id,
                                                      reactionMessageViewModel.user_id,
                                                      reactionMessageViewModel.type_reaction_id);
            return Ok();
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Update(ReactionEntityUpdateViewModel reactionMessageUpdateViewModel)
        {
            _logger.LogInformation($"Вызван метод Update");

            await _reactionMessageService.UpdateAsync(reactionMessageUpdateViewModel.entity_id,
                                                      reactionMessageUpdateViewModel.user_id,
                                                      reactionMessageUpdateViewModel.type_reaction_id,
                                                      reactionMessageUpdateViewModel.type_reaction_old_id);
            return Ok();
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> Delete(ReactionEntityViewModel reactionMessageViewModel)
        {
            _logger.LogInformation($"Вызван метод Delete");

            await _reactionMessageService.DeleteAsync(reactionMessageViewModel.entity_id,
                                                      reactionMessageViewModel.user_id,
                                                      reactionMessageViewModel.type_reaction_id);
            return Ok();
        }
    }
}