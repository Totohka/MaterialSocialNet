using Goods.System.Social.Network.DAL.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using FluentValidation;
using DomainModel.Entities;

namespace Goods.System.Social.Network.DomainServices.Interface
{
    public class BackgroundService : IBackgroundService
    {
        private readonly IEnumerable<IPhotoRepository> _photoRepositoies;
        private readonly IValidator<int> _idValidator;
        private readonly ILogger<GalleryService> _logger;
        public BackgroundService(IEnumerable<IPhotoRepository> photoRepositoies, ILogger<GalleryService> logger, IValidator<int> idValidator)
        {
            _logger = logger;
            _photoRepositoies = photoRepositoies;
            _idValidator = idValidator;
        }

        public async Task CreateAsync(IFormFile image, int userId)
        {
            _logger.LogTrace($"Вызван метод CreateAsync с параметрами: image: -, userId: {userId}");

            var result = await _idValidator.ValidateAsync(userId);
            if (!result.IsValid) throw new ValidationException(result.Errors);

            await _photoRepositoies.FirstOrDefault(p => p.FileRepository == FileRepositoryEnum.Background).CreateAsync(image, userId);
        }

        public void Delete(int userId)
        {
            _logger.LogTrace($"Вызван метод Delete с параметрами: userId: {userId}");

            var result = _idValidator.Validate(userId);
            if (!result.IsValid) throw new ValidationException(result.Errors);

            _photoRepositoies.FirstOrDefault(p => p.FileRepository == FileRepositoryEnum.Background).Delete(userId);
        }

        public string Get(int userId)
        {
            _logger.LogTrace($"Вызван метод Get с параметрами: userId: {userId}");

            var result = _idValidator.Validate(userId);
            if (!result.IsValid) throw new ValidationException(result.Errors);

            return _photoRepositoies.FirstOrDefault(p => p.FileRepository == FileRepositoryEnum.Background).Get(userId);
        }

        public async Task UpdateAsync(IFormFile image, int userId)
        {
            _logger.LogTrace($"Вызван метод UpdateAsync с параметрами: image: -, userId: {userId}");

            var result = await _idValidator.ValidateAsync(userId);
            if (!result.IsValid) throw new ValidationException(result.Errors);

            await _photoRepositoies.FirstOrDefault(p => p.FileRepository == FileRepositoryEnum.Background).UpdateAsync(image, userId);
        }
    }
}