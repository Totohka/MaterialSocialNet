using DomainModel.Entities.ViewModels;
using Goods.System.Social.Network.DomainModel.Entities;
using Goods.System.Social.Network.DomainServices.Interface;
using Goods.System.Social.Network.DomainServices.Realization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System.Diagnostics.Contracts;
using System.Net;

namespace Goods.System.Social.Network.Microservice.Users.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GalleryController : ControllerBase
    {
        private readonly IGalleryService _galleryService;
        private readonly ILogger<GalleryController> _logger;

        public GalleryController(IGalleryService galleryService, ILogger<GalleryController> logger, ILoggerFactory loggerFactory)
        {
            _galleryService = galleryService;
            _logger = logger;
        }

        /// <summary>
        /// Получение типа реакции
        /// </summary>
        /// <param name="userId">Id типа реакции</param>
        /// <param name="take">Id типа реакции</param>
        /// <returns></returns>
        /// <response code="200">Список путей до фоток</response>
        /// <response code="401">Ошибка авторизации</response>
        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(List<string>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        public IActionResult GetPhotos(int userId, int take = 0)
        {
            _logger.LogInformation($"Вызван метод GetPhotos");

            return Ok(_galleryService.GetPhotos(userId, take));
        }

        /// <summary>
        /// Получение типа реакции
        /// </summary>
        /// <param name="id">Id типа реакции</param>
        /// <returns></returns>
        /// <response code="200">Всё ок</response>
        /// <response code="401">Ошибка авторизации</response>
        [Authorize]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> Create([FromForm] GalleryCreateViewModel galleryCreateViewModel)
        {
            _logger.LogInformation($"Вызван метод Create");

            if (int.Parse(HttpContext.User.FindFirst(claim => claim.Type == "id").Value) == galleryCreateViewModel.UserId)
            {
                await _galleryService.CreateAsync(galleryCreateViewModel.Photo, galleryCreateViewModel.UserId);
                return Ok();
            }
            else
            {
                _logger.LogInformation($"Устаревший токен");

                return StatusCode(401);
            }
        }

        /// <summary>
        /// Получение типа реакции
        /// </summary>
        /// <param name="id">Id типа реакции</param>
        /// <returns></returns>
        /// <response code="200">Всё ок</response>
        /// <response code="401">Ошибка авторизации</response>
        [Authorize]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> Update([FromForm] GalleryUpdateViewModel galleryUpdateViewModel)
        {
            _logger.LogInformation($"Вызван метод Update");

            if (int.Parse(HttpContext.User.FindFirst(claim => claim.Type == "id").Value) == galleryUpdateViewModel.UserId)
            {
                await _galleryService.UpdateAsync(galleryUpdateViewModel.Photo, galleryUpdateViewModel.UserId, galleryUpdateViewModel.PhotoId);
                return Ok();
            }
            else
            {
                _logger.LogInformation($"Устаревший токен");

                return StatusCode(401);
            }
        }

        /// <summary>
        /// Получение типа реакции
        /// </summary>
        /// <param name="id">Id типа реакции</param>
        /// <returns></returns>
        /// <response code="200">Всё ок</response>
        /// <response code="401">Ошибка авторизации</response>
        [Authorize]
        [HttpDelete("{imageId:int}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        public IActionResult Delete(int imageId, int userId)
        {
            _logger.LogInformation($"Вызван метод Update");

            if (int.Parse(HttpContext.User.FindFirst(claim => claim.Type == "id").Value) == userId)
            {
                _galleryService.Delete(imageId, userId);
                return Ok();
            }
            else
            {
                _logger.LogInformation($"Устаревший токен");
                return StatusCode(401);
            }
        }
    }
}
