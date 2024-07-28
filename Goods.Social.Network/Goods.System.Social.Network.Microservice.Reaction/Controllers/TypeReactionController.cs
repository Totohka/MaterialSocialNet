using Goods.System.Social.Network.DomainModel.Entities;
using Goods.System.Social.Network.DomainServices.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;

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

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            _logger.LogInformation($"Вызван метод Get");

            return Ok(await _typeReactionService.GetAsync(id));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation($"Вызван метод GetAll");

            return Ok(await _typeReactionService.GetAllAsync());
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(ReactionType typeReaction)
        {
            _logger.LogInformation($"Вызван метод Create");

            await _typeReactionService.CreateAsync(typeReaction);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> Update(ReactionType typeReaction)
        {
            _logger.LogInformation($"Вызван метод Update");

            await _typeReactionService.UpdateAsync(typeReaction);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation($"Вызван метод Delete");

            await _typeReactionService.DeleteAsync(id);
            return Ok();
        }
    }
}