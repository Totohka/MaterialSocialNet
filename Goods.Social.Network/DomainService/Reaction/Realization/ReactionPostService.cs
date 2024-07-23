using DomainModel.Entities.ViewModels;
using DomainServices.Validation;
using FluentValidation;
using Goods.System.Social.Network.DAL.Repository.Interface;
using Goods.System.Social.Network.DomainModel.Entities;
using Microsoft.Extensions.Logging;

namespace Goods.System.Social.Network.DomainServices.Interface
{
    public class ReactionPostService<T> : IReactionEntityService<T> 
        where T : ReactionPost, new()
    {
        private readonly int _countReaction = 20;
        private readonly IReactionEntityRepository<T> _reactionEntityRepository;
        private readonly IReactionRepository _reactionRepository;
        private readonly IValidator<ReactionData> _reactionValidator;
        private readonly IValidator<int> _idValidator;
        private readonly ILogger<ReactionPostService<T>> _logger;
        public ReactionPostService(IReactionEntityRepository<T> reactionEntityRepository,
                                   IReactionRepository reactionRepository,
                                   IValidator<ReactionData> reactionValidator,
                                   IValidator<int> idValidator,
                                   ILogger<ReactionPostService<T>> logger)
        {
            _logger = logger;
            _reactionRepository = reactionRepository;
            _reactionValidator = reactionValidator;
            _idValidator = idValidator;
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
            T reactionPost = new T();
            reaction.UserId = userId;
            reaction.TypeReactionId = typeReactionId;
            _reactionRepository.Create(reaction);

            reaction = await _reactionRepository.GetAsync(userId, typeReactionId);

            List<T> reactionPosts = await _reactionEntityRepository.GetAllByEntityAsync(entityId, -1);

            if (!(reactionPosts.Where(rp => rp.Reaction.UserId == userId).Any()))
            {
                reactionPost.PostId = entityId;
                reactionPost.ReactionId = reaction.Id;
                await _reactionEntityRepository.CreateAsync(reactionPost);
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
            List<T> reactionPosts = await _reactionEntityRepository.GetAllByEntityAsync(entityId, -1);
            T reactionPost = reactionPosts.Where(r => r.ReactionId == reaction.Id).First();
            await _reactionEntityRepository.DeleteAsync(reactionPost.Id);
        }

        public async Task<PageReactionEntity<T>> GetAllByEntityAsync(int postId, int number)
        {
            _logger.LogTrace($"Вызван метод GetAllByEntityAsync с параметрами: postId: {postId}, number: {number}");

            var result = await _idValidator.ValidateAsync(postId);
            if (!result.IsValid) throw new ValidationException(result.Errors);

            List<T> reactions = await _reactionEntityRepository.GetAllByEntityAsync(postId, number);
            int count = await _reactionEntityRepository.GetCountAsync(postId);

            var pageReactionPost = new PageReactionEntity<T>(
                    count,
                    (int)Math.Ceiling((double)count / _countReaction),
                    number, 
                    reactions
                );

            return pageReactionPost;
        }

        public async Task<int> GetRatingForEntityAsync(int postId)
        {
            _logger.LogTrace($"Вызван метод GetRatingForEntityAsync с параметрами: postId: {postId}");

            var result = await _idValidator.ValidateAsync(postId);
            if (!result.IsValid) throw new ValidationException(result.Errors);

            var reactions = await _reactionEntityRepository.GetAllByEntityAsync(postId, -1);
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
            
            ReactionData reactionData = new ReactionData() { 
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
            var reactionPosts = await _reactionEntityRepository.GetAllByEntityAsync(entityId, -1);
            var reactionPost = reactionPosts.Where(r => r.ReactionId == reactionOld.Id).First();
            await _reactionEntityRepository.DeleteAsync(reactionPost.Id);

            await CreateAsync(entityId, userId, typeReactionId);
        }
    }
}