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

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get(int postId)
        {
            _logger.LogInformation($"Вызван метод Get");

            return Ok(await _reactionPostService.GetRatingForEntityAsync(postId));
        }

        [Authorize]
        [HttpGet("all")]
        public async Task<IActionResult> GetAllByPost(int postId, int number = 0)
        {
            _logger.LogInformation($"Вызван метод GetAllByPost");

            PageReactionEntity<ReactionPost> pageReactionEntity = await _reactionPostService.GetAllByEntityAsync(postId, number);
            List<ReactionPostDTO> reactionDTOs = new List<ReactionPostDTO>();

            pageReactionEntity.ReactionEntities.ForEach(reactionPost => {
                reactionDTOs.Add(_mapper.Map<ReactionPostDTO>(reactionPost));
            });
            var pageReactionEntityDTO = new PageReactionEntityDTO<ReactionPostDTO>(pageReactionEntity.CountAllReactionEntities,
                                                                                      pageReactionEntity.PageCount,
                                                                                      pageReactionEntity.NumberPage,
                                                                                      reactionDTOs);
            return Ok(pageReactionEntityDTO);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(ReactionEntityViewModel reactionPostViewModel)
        {
            _logger.LogInformation($"Вызван метод Create");

            await _reactionPostService.CreateAsync(reactionPostViewModel.EntityId, 
                                                   reactionPostViewModel.UserId, 
                                                   reactionPostViewModel.TypeReactionId);
            return Ok();
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Update(ReactionEntityUpdateViewModel reactionPostUpdateViewModel)
        {
            _logger.LogInformation($"Вызван метод Update");

            await _reactionPostService.UpdateAsync(reactionPostUpdateViewModel.EntityId, 
                                                   reactionPostUpdateViewModel.UserId, 
                                                   reactionPostUpdateViewModel.TypeReactionId, 
                                                   reactionPostUpdateViewModel.TypeReactionOldId);
            return Ok();
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> Delete(ReactionEntityViewModel reactionPostViewModel)
        {
            _logger.LogInformation($"Вызван метод Delete");

            await _reactionPostService.DeleteAsync(reactionPostViewModel.EntityId, 
                                                   reactionPostViewModel.UserId, 
                                                   reactionPostViewModel.TypeReactionId);
            return Ok();
        }
    }
}