using Goods.System.Social.Network.DAL.Repository.Interface;
using Goods.System.Social.Network.DomainModel.Entities;
using Goods.System.Social.Network.DomainServices.Realization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using NLog;
using FluentValidation;

namespace Goods.System.Social.Network.DomainServices.Interface
{
    public class GalleryService : IGalleryService
    {
        private readonly IGalleryRepository _galleryRepository;
        private readonly IValidator<int> _idValidator;
        private readonly ILogger<GalleryService> _logger;
        public GalleryService(IGalleryRepository galleryRepository, ILogger<GalleryService> logger, IValidator<int> idValidator)
        {
            _galleryRepository = galleryRepository;
            _logger = logger;
            _idValidator = idValidator;
        }

        public async Task CreateAsync(IFormFile image, int userId)
        {
            _logger.LogTrace($"Вызван метод CreateAsync с параметрами: image: -, userId: {userId}");
            var result = await _idValidator.ValidateAsync(userId);
            if (!result.IsValid) throw new ValidationException(result.Errors);

            await _galleryRepository.CreateAsync(image, userId);
        }

        public void Delete(int photoId, int userId)
        {
            _logger.LogTrace($"Вызван метод Delete с параметрами: photoId: {photoId}, userId: {userId}");

            var result = _idValidator.Validate(userId);
            if (!result.IsValid) throw new ValidationException(result.Errors);
            result = _idValidator.Validate(photoId);
            if (!result.IsValid) throw new ValidationException(result.Errors);

            _galleryRepository.Delete(photoId, userId);
        }

        public List<string> GetPhotos(int userId, int take = 0)
        {
            _logger.LogTrace($"Вызван метод GetPhotos с параметрами: userId: {userId}, take: {take}");

            var result = _idValidator.Validate(userId);
            if (!result.IsValid) throw new ValidationException(result.Errors);

            var pathPhotoByUser = _galleryRepository.GetByUser(userId, take);
            pathPhotoByUser.Reverse();
            return pathPhotoByUser;
        }

        public async Task UpdateAsync(IFormFile image, int userId, int photoId)
        {
            _logger.LogTrace($"Вызван метод CreateAsync с параметрами: image: -, userId: {userId}, photoId: {photoId}");

            var result = await _idValidator.ValidateAsync(userId);
            if (!result.IsValid) throw new ValidationException(result.Errors);
            result = await _idValidator.ValidateAsync(photoId);
            if (!result.IsValid) throw new ValidationException(result.Errors);

            await _galleryRepository.UpdateAsync(image, userId, photoId);
        }
    }
}