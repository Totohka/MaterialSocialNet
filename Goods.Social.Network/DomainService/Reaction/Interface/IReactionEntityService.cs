using DomainModel.Entities.ViewModels;
using Goods.System.Social.Network.DomainModel.Entities;
using Microsoft.AspNetCore.Http;

namespace Goods.System.Social.Network.DomainServices.Interface
{
    public interface IReactionEntityService<T> 
        where T : class //ReactionPost или ReactionMessage
    {
        Task<int> GetRatingForEntityAsync(int entityId);
        Task<PageReactionEntity<T>> GetAllByEntityAsync(int entityId, int number);
        Task CreateAsync(int entityId, int userId, int typeReactionId);
        Task UpdateAsync(int entityId, int userId, int typeReactionId, int typeReactionOldId);
        Task DeleteAsync(int entityId, int userId, int typeReactionId);
    }
}