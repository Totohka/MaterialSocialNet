using DomainModel.Entities.ViewModels;
using Goods.System.Social.Network.DomainModel.Entities;
using Goods.System.Social.Network.DomainServices.Interface;
using Goods.System.Social.Network.DomainServices.Realization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System.Diagnostics.Contracts;

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

        [Authorize]
        [HttpGet]
        public IActionResult GetPhotos(int userId, int take = 0)
        {
            _logger.LogInformation($"Вызван метод GetPhotos");

            return Ok(_galleryService.GetPhotos(userId, take));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] GalleryCreateViewModel galleryCreateViewModel)
        {
            _logger.LogInformation($"Вызван метод Create");

            if (int.Parse(HttpContext.User.FindFirst(claim => claim.Type == "id").Value) == galleryCreateViewModel.user_id)
            {
                await _galleryService.CreateAsync(galleryCreateViewModel.photo, galleryCreateViewModel.user_id);
                return Ok();
            }
            else
            {
                _logger.LogInformation($"Устаревший токен");

                return StatusCode(401);
            }
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Update([FromForm] GalleryUpdateViewModel galleryUpdateViewModel)
        {
            _logger.LogInformation($"Вызван метод Update");

            if (int.Parse(HttpContext.User.FindFirst(claim => claim.Type == "id").Value) == galleryUpdateViewModel.user_id)
            {
                await _galleryService.UpdateAsync(galleryUpdateViewModel.photo, galleryUpdateViewModel.user_id, galleryUpdateViewModel.photo_id);
                return Ok();
            }
            else
            {
                _logger.LogInformation($"Устаревший токен");

                return StatusCode(401);
            }
        }

        [Authorize]
        [HttpDelete("{imageId:int}")]
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
