using DomainModel.Entities.ViewModels;
using FluentValidation;
using Goods.System.Social.Network.DAL.Repository.Interface;
using Goods.System.Social.Network.DomainModel.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NLog;
namespace Goods.System.Social.Network.DomainServices.Interface
{
    public class TypeReactionService : ITypeReactionService
    {
        private readonly ITypeReactionRepository _typeReactionRepository;
        private readonly IValidator<ReactionType> _reactionTypeValidate;
        private readonly IValidator<int> _idValidator;
        private readonly ILogger<TypeReactionService> _logger;
        public TypeReactionService(ITypeReactionRepository typeReactionRepository,
                                   IValidator<ReactionType> reactionTypeValidate,
                                   IValidator<int> idValidator,
                                   ILogger<TypeReactionService> logger)
        {
            _logger = logger;
            _reactionTypeValidate = reactionTypeValidate;
            _idValidator = idValidator;
            _typeReactionRepository = typeReactionRepository;
        }

        public async Task CreateAsync(ReactionType typeReaction)
        {
            _logger.LogTrace($"Вызван метод CreateAsync с параметрами: typeReaction: {typeReaction}");
            var result = await _reactionTypeValidate.ValidateAsync(typeReaction);
            if (!result.IsValid) throw new ValidationException(result.Errors);
            await _typeReactionRepository.CreateAsync(typeReaction);
        }

        public async Task DeleteAsync(int id)
        {
            _logger.LogTrace($"Вызван метод DeleteAsync с параметрами: id: {id}");

            var result = await _idValidator.ValidateAsync(id);
            if (!result.IsValid) throw new ValidationException(result.Errors);

            await _typeReactionRepository.DeleteAsync(id);
        }

        public async Task<ReactionType> GetAsync(int id)
        {
            _logger.LogTrace($"Вызван метод GetAsync с параметрами: id: {id}");

            var result = await _idValidator.ValidateAsync(id);
            if (!result.IsValid) throw new ValidationException(result.Errors);

            return await _typeReactionRepository.GetAsync(id);
        }

        public async Task<List<ReactionType>> GetAllAsync()
        {
            _logger.LogTrace($"Вызван метод GetAllAsync");

            return await _typeReactionRepository.GetAllAsync();
        }

        public async Task UpdateAsync(ReactionType typeReaction)
        {
            _logger.LogTrace($"Вызван метод UpdateAsync с параметрами: typeReaction: {typeReaction}");
            var result = await _reactionTypeValidate.ValidateAsync(typeReaction);
            if (!result.IsValid) throw new ValidationException(result.Errors);
            await _typeReactionRepository.UpdateAsync(typeReaction);
        }
    }
}