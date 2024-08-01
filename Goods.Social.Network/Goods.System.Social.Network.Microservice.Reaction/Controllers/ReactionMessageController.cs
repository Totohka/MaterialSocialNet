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
        /// Получение рейтинга(для сообщений вроде не используется)
        /// </summary>
        /// <param name="entityId">Id сообщения</param>
        /// <returns></returns>
        /// <response code="200">Рейтинг сообщения</response>
        /// <response code="401">Ошибка авторизации</response>
        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> Get(int entityId)
        {
            _logger.LogInformation($"Вызван метод Get");
            return Ok(await _reactionMessageService.GetRatingForEntityAsync(entityId));
        }

        /// <summary>
        /// Получение реакций на сообщение
        /// </summary>
        /// <param name="entityId">Id сообщения</param>
        /// <param name="number">Параметр пагинации</param>
        /// <returns></returns>
        /// <response code="200">Страница реакций DTO на пост</response>
        /// <response code="401">Ошибка авторизации</response>
        [Authorize]
        [HttpGet("all")]
        [ProducesResponseType(typeof(PageReactionEntityDTO<ReactionEntityDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetAllByMessage(int entityId, int number = 0)
        {
            _logger.LogInformation($"Вызван метод GetAll");

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
        /// Создание реакции на сообщение
        /// </summary>
        /// <param name="reactionMessageViewModel">Модель реакций</param>
        /// <returns></returns>
        /// <response code="200">Всё ок</response>
        /// <response code="401">Ошибка авторизации</response>
        [Authorize]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> Create(ReactionEntityViewModel reactionMessageViewModel)
        {
            _logger.LogInformation($"Вызван метод Create");

            await _reactionMessageService.CreateAsync(reactionMessageViewModel.EntityId,
                                                      reactionMessageViewModel.UserId,
                                                      reactionMessageViewModel.TypeReactionId);
            return Ok();
        }

        /// <summary>
        /// Редактирование реакции на сообщение
        /// </summary>
        /// <param name="reactionMessageUpdateViewModel">Модель реакций для редактирования</param>
        /// <returns></returns>
        /// <response code="200">Всё ок</response>
        /// <response code="401">Ошибка авторизации</response>
        [Authorize]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> Update(ReactionEntityUpdateViewModel reactionMessageUpdateViewModel)
        {
            _logger.LogInformation($"Вызван метод Update");

            await _reactionMessageService.UpdateAsync(reactionMessageUpdateViewModel.EntityId,
                                                      reactionMessageUpdateViewModel.UserId,
                                                      reactionMessageUpdateViewModel.TypeReactionId,
                                                      reactionMessageUpdateViewModel.TypeReactionOldId);
            return Ok();
        }

        /// <summary>
        /// Удаление реакции на сообщение
        /// </summary>
        /// <param name="reactionMessageViewModel">Модель реакции</param>
        /// <returns></returns>
        /// <response code="200">Всё ок</response>
        /// <response code="401">Ошибка авторизации</response>
        [Authorize]
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> Delete(ReactionEntityViewModel reactionMessageViewModel)
        {
            _logger.LogInformation($"Вызван метод Delete");

            await _reactionMessageService.DeleteAsync(reactionMessageViewModel.EntityId,
                                                      reactionMessageViewModel.UserId,
                                                      reactionMessageViewModel.TypeReactionId);
            return Ok();
        }
    }
}