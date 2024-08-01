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
        /// Получение рейтинга поста
        /// </summary>
        /// <param name="entityId">Id поста</param>
        /// <returns></returns>
        /// <response code="200">Рейтинг поста</response>
        /// <response code="401">Ошибка авторизации</response>
        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> Get(int entityId)
        {
            _logger.LogInformation($"Вызван метод Get");

            return Ok(await _reactionPostService.GetRatingForEntityAsync(entityId));
        }

        /// <summary>
        /// Получение реакций поста
        /// </summary>
        /// <param name="entityId">Id поста</param>
        /// <param name="number">Параметр пагинации</param>
        /// <returns></returns>
        /// <response code="200">Страница постов DTO</response>
        /// <response code="401">Ошибка авторизации</response>
        [Authorize]
        [HttpGet("all")]
        [ProducesResponseType(typeof(PageReactionEntityDTO<ReactionEntityDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetAllByPost(int entityId, int number = 0)
        {
            _logger.LogInformation($"Вызван метод GetAllByPost");

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
        /// Создание реакции на пост
        /// </summary>
        /// <param name="reactionPostViewModel">Модель реакции поста</param>
        /// <returns></returns>
        /// <response code="200">Всё ок</response>
        /// <response code="401">Ошибка авторизации</response>
        [Authorize]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> Create(ReactionEntityViewModel reactionPostViewModel)
        {
            _logger.LogInformation($"Вызван метод Create");

            await _reactionPostService.CreateAsync(reactionPostViewModel.EntityId, 
                                                   reactionPostViewModel.UserId, 
                                                   reactionPostViewModel.TypeReactionId);
            return Ok();
        }

        /// <summary>
        /// Редактирование реакции поста
        /// </summary>
        /// <param name="reactionPostUpdateViewModel">Модель для редактирования реакции поста</param>
        /// <returns></returns>
        /// <response code="200">Всё ок</response>
        /// <response code="401">Ошибка авторизации</response>
        [Authorize]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> Update(ReactionEntityUpdateViewModel reactionPostUpdateViewModel)
        {
            _logger.LogInformation($"Вызван метод Update");

            await _reactionPostService.UpdateAsync(reactionPostUpdateViewModel.EntityId, 
                                                   reactionPostUpdateViewModel.UserId, 
                                                   reactionPostUpdateViewModel.TypeReactionId, 
                                                   reactionPostUpdateViewModel.TypeReactionOldId);
            return Ok();
        }

        /// <summary>
        /// Удаление реакции поста
        /// </summary>
        /// <param name="reactionPostViewModel">Модель реакции поста</param>
        /// <returns></returns>
        /// <response code="200">Всё ок</response>
        /// <response code="401">Ошибка авторизации</response>
        [Authorize]
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
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