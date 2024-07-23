using Goods.System.Social.Network.DAL.Repository.Interface;
using DomainModel.Entities.ViewModels;
using Goods.System.Social.Network.DAL;
using Goods.System.Social.Network.DomainModel.Entities;
using Goods.System.Social.Network.DomainServices.Interface;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using NLog;
using FluentValidation;
using DomainServices.Validation;

namespace Goods.System.Social.Network.DomainServices.Realization
{
    public class SubscribeService : ISubscribeService
    {
        private readonly ISubscribeRepository _subscribeRepository;
        private readonly IValidator<UserFriend> _userFriendValidator;
        private readonly IValidator<int> _idValidator;
        private readonly ILogger<SubscribeService> _logger;
        public SubscribeService(ISubscribeRepository subscribeRepository,
                                IValidator<UserFriend> userFriendValidator,
                                IValidator<int> idValidator,
                                ILogger<SubscribeService> logger)
        {
            _logger = logger;
            _subscribeRepository = subscribeRepository;
            _userFriendValidator = userFriendValidator;
            _idValidator = idValidator;
        }

        public async Task<bool> GetCheckSubscriberAsync(int id, int idFriend)
        {
            _logger.LogTrace($"Вызван метод GetCheckSubscriberAsync с параметрами: id: {id}, idFriend: {idFriend}");

            var result = await _idValidator.ValidateAsync(id);
            if (!result.IsValid) throw new ValidationException(result.Errors);
            result = await _idValidator.ValidateAsync(idFriend);
            if (!result.IsValid) throw new ValidationException(result.Errors);

            return await _subscribeRepository.GetCheckAsync(id, idFriend);
        }

        public async Task AddSubscribeAsync(UserFriend userFriend)
        {
            _logger.LogTrace($"Вызван метод AddSubscribe с параметрами: userFriend: {userFriend}");
            var result = await _userFriendValidator.ValidateAsync(userFriend);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
            await _subscribeRepository.CreateAsync(userFriend);
        }

        public async Task DeleteSubscribeAsync(UserFriend userFriend)
        {
            _logger.LogTrace($"Вызван метод DeleteSubscribeAsync с параметрами: userFriend: {userFriend}");
            var result = await _userFriendValidator.ValidateAsync(userFriend);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
            await _subscribeRepository.DeleteAsync(userFriend);
        }
    }
}