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
        /// ��������� ���� �������
        /// </summary>
        /// <param name="id">Id ���� �������</param>
        /// <returns></returns>
        /// <response code="200">��� �������</response>
        /// <response code="401">������ �����������</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(ReactionType), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> Get(int id)
        {
            _logger.LogInformation($"������ ����� Get");

            return Ok(await _typeReactionService.GetAsync(id));
        }

        /// <summary>
        /// ��������� ���� ����� ������� 
        /// </summary>
        /// <returns></returns>
        /// <response code="200">������ ����� �������</response>
        /// <response code="401">������ �����������</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<ReactionType>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation($"������ ����� GetAll");

            return Ok(await _typeReactionService.GetAllAsync());
        }

        /// <summary>
        /// �������� ���� �������
        /// </summary>
        /// <param name="typeReaction">������ ���� �������</param>
        /// <returns></returns>
        /// <response code="200">�� ��</response>
        /// <response code="401">������ �����������</response>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> Create(ReactionType typeReaction)
        {
            _logger.LogInformation($"������ ����� Create");

            await _typeReactionService.CreateAsync(typeReaction);
            return Ok();
        }

        /// <summary>
        /// �������������� ���� �������
        /// </summary>
        /// <param name="typeReaction">������ ������� �����</param>
        /// <returns></returns>
        /// <response code="200">�� ��</response>
        /// <response code="401">������ �����������</response>
        [Authorize(Roles = "Admin")]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> Update(ReactionType typeReaction)
        {
            _logger.LogInformation($"������ ����� Update");

            await _typeReactionService.UpdateAsync(typeReaction);
            return Ok();
        }

        /// <summary>
        /// �������� ���� �������
        /// </summary>
        /// <param name="id">Id ���� �������</param>
        /// <returns></returns>
        /// <response code="200">�� ��</response>
        /// <response code="401">������ �����������</response>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation($"������ ����� Delete");

            await _typeReactionService.DeleteAsync(id);
            return Ok();
        }
    }
}