using DomainModel.Entities.ViewModels;
using Goods.System.Social.Network.DomainModel.Entities;
using Microsoft.AspNetCore.Http;

namespace Goods.System.Social.Network.DomainServices.Interface
{
    public interface ITypeReactionService
    {
        Task<ReactionType> GetAsync(int id);
        Task<List<ReactionType>> GetAllAsync();
        Task CreateAsync(ReactionType typeReaction);
        Task UpdateAsync(ReactionType typeReaction);
        Task DeleteAsync(int id);
    }
}