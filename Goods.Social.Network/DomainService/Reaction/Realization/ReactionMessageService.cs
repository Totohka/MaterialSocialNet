using DomainModel.Entities.ViewModels;
using DomainServices.Validation;
using FluentValidation;
using Goods.System.Social.Network.DAL.Repository.Interface;
using Goods.System.Social.Network.DomainModel.Entities;
using Microsoft.Extensions.Logging;
using NLog;

namespace Goods.System.Social.Network.DomainServices.Interface
{
    public class ReactionMessageService<T> : IReactionEntityService<T>
        where T : ReactionMessage, new()
    {
        private readonly int _countReaction = 20;
        private readonly IReactionEntityRepository<T> _reactionEntityRepository;
        private readonly IValidator<ReactionData> _reactionValidator;
        private readonly IReactionRepository _reactionRepository;
        private readonly IValidator<int> _idValidator;
        private readonly ILogger<ReactionMessageService<T>> _logger;
        public ReactionMessageService(IReactionEntityRepository<T> reactionEntityRepository,
                                      IValidator<ReactionData> reactionValidator,
                                      IReactionRepository reactionRepository,
                                      IValidator<int> idValidator,
                                      ILogger<ReactionMessageService<T>> logger)
        {
            _logger = logger;
            _reactionRepository = reactionRepository;
            _idValidator = idValidator;
            _reactionValidator = reactionValidator;
            _reactionEntityRepository = reactionEntityRepository;
        }

        public async Task CreateAsync(int entityId, int userId, int typeReactionId)
        {
            _logger.LogTrace($"Вызван метод CreateAsync с параметрами: entityId: {entityId}, userId: {userId}, typeReactionId: {typeReactionId}");

            ReactionData reactionData = new ReactionData() { entityId = entityId, typeReactionId = typeReactionId, userId = userId };
            var result = await _reactionValidator.ValidateAsync(reactionData);
            if (!result.IsValid)
                throw new ValidationException(result.Errors);

            Reaction reaction = new Reaction();
            T reactionMessage = new T();
            reaction.UserId = userId;
            reaction.TypeReactionId = typeReactionId;
            _reactionRepository.Create(reaction);
            reaction = await _reactionRepository.GetAsync(userId, typeReactionId);

            List<T> reactionMessages = await _reactionEntityRepository.GetAllByEntityAsync(entityId, -1);

            if (!(reactionMessages.Where(rm => rm.Reaction.UserId == userId).Any()))
            {
                reactionMessage.MessageId = entityId;
                reactionMessage.ReactionId = reaction.Id;
                await _reactionEntityRepository.CreateAsync(reactionMessage);
            }
        }
        
        public async Task DeleteAsync(int entityId, int userId, int typeReactionId)
        {
            _logger.LogTrace($"Вызван метод DeleteAsync с параметрами: entityId: {entityId}, userId: {userId}, typeReactionId: {typeReactionId}");

            ReactionData reactionData = new ReactionData() { entityId = entityId, typeReactionId = typeReactionId, userId = userId };
            var result = await _reactionValidator.ValidateAsync(reactionData);
            if (!result.IsValid)
                throw new ValidationException(result.Errors);

            Reaction reaction = await _reactionRepository.GetAsync(userId, typeReactionId);
            var reactionMessages = await _reactionEntityRepository.GetAllByEntityAsync(entityId, -1);
            var reactionMessage = reactionMessages.Where(r => r.ReactionId == reaction.Id).First();
            await _reactionEntityRepository.DeleteAsync(reactionMessage.Id);
        }

        public async Task<PageReactionEntity<T>> GetAllByEntityAsync(int messageId, int number)
        {
            _logger.LogTrace($"Вызван метод GetAllByEntityAsync с параметрами: messageId: {messageId}, number: {number}");

            var result = await _idValidator.ValidateAsync(messageId);
            if (!result.IsValid) throw new ValidationException(result.Errors);

            List<T> reactions = await _reactionEntityRepository.GetAllByEntityAsync(messageId, number);
            int count = await _reactionEntityRepository.GetCountAsync(messageId);
            
            var pageReactionMessage = new PageReactionEntity<T>(
                    count,
                    (int)Math.Ceiling((double)count / _countReaction),
                    number, 
                    reactions
                );

            return pageReactionMessage;
        }

        public async Task<int> GetRatingForEntityAsync(int messageId)
        {
            _logger.LogTrace($"Вызван метод GetRatingForEntityAsync с параметрами: messageId: {messageId}");

            var result = await _idValidator.ValidateAsync(messageId);
            if (!result.IsValid) throw new ValidationException(result.Errors);

            var reactions = await _reactionEntityRepository.GetAllByEntityAsync(messageId, -1);
            int rating = 0;
            foreach (var reaction in reactions)
            {
                if (reaction.Reaction.TypeReaction.Name == "Like")
                    rating++;
                else if (reaction.Reaction.TypeReaction.Name == "Dislike")
                    rating--;
            }
            return rating;
        }

        public async Task UpdateAsync(int entityId, int userId, int typeReactionId, int typeReactionOldId)
        {
            _logger.LogTrace($"Вызван метод UpdateAsync с параметрами: entityId: {entityId}, userId: {userId}, typeReactionId: {typeReactionId}, typeReactionOldId: {typeReactionOldId}");
            
            ReactionData reactionData = new ReactionData()
            {
                entityId = entityId,
                typeReactionId = typeReactionId,
                userId = userId,
                typeReactionOldId = typeReactionOldId,
                IsUpdate = true
            };
            var result = await _reactionValidator.ValidateAsync(reactionData);
            if (!result.IsValid)
                throw new ValidationException(result.Errors);

            Reaction reactionOld = await _reactionRepository.GetAsync(userId, typeReactionOldId);
            var reactionMessages = await _reactionEntityRepository.GetAllByEntityAsync(entityId, -1);
            var reactionMessage = reactionMessages.Where(r => r.ReactionId == reactionOld.Id).First();
            await _reactionEntityRepository.DeleteAsync(reactionMessage.Id);

            await CreateAsync(entityId, userId, typeReactionId);
        }
    }
}