using Goods.System.Social.Network.DomainModel.Entities;
using Goods.System.Social.Network.DomainServices.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System.Net;

namespace Goods.System.Social.Network.Microservice.Reaction.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TypeReactionController : Controller
    {
        private readonly ILogger<TypeReactionController> _logger;
        private readonly ITypeReactionService _typeReactionService;

        public TypeReactionController(ILogger<TypeReactionController> logger, ITypeReactionService typeReactionService)
        {
            _logger = logger;
            _typeReactionService = typeReactionService;
        }

        /// <summary>
        /// Получение типа реакции
        /// </summary>
        /// <param name="id">Id типа реакции</param>
        /// <returns></returns>
        /// <response code="200">Тип реакции</response>
        /// <response code="401">Ошибка авторизации</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(ReactionType), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> Get(int id)
        {
            _logger.LogInformation($"Вызван метод Get");

            return Ok(await _typeReactionService.GetAsync(id));
        }

        /// <summary>
        /// Получение всех типов реакций 
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Список типов реакций</response>
        /// <response code="401">Ошибка авторизации</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<ReactionType>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation($"Вызван метод GetAll");

            return Ok(await _typeReactionService.GetAllAsync());
        }

        /// <summary>
        /// Создание типа реакции
        /// </summary>
        /// <param name="typeReaction">Модель типа реакции</param>
        /// <returns></returns>
        /// <response code="200">Всё ок</response>
        /// <response code="401">Ошибка авторизации</response>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> Create(ReactionType typeReaction)
        {
            _logger.LogInformation($"Вызван метод Create");

            await _typeReactionService.CreateAsync(typeReaction);
            return Ok();
        }

        /// <summary>
        /// Редактирование типа реакции
        /// </summary>
        /// <param name="typeReaction">Модель реакции поста</param>
        /// <returns></returns>
        /// <response code="200">Всё ок</response>
        /// <response code="401">Ошибка авторизации</response>
        [Authorize(Roles = "Admin")]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> Update(ReactionType typeReaction)
        {
            _logger.LogInformation($"Вызван метод Update");

            await _typeReactionService.UpdateAsync(typeReaction);
            return Ok();
        }

        /// <summary>
        /// Удаление типа реакции
        /// </summary>
        /// <param name="id">Id типа реакции</param>
        /// <returns></returns>
        /// <response code="200">Всё ок</response>
        /// <response code="401">Ошибка авторизации</response>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation($"Вызван метод Delete");

            await _typeReactionService.DeleteAsync(id);
            return Ok();
        }
    }
}