using DomainModel.Entities.ViewModels;
using Goods.System.Social.Network.DomainModel.Entities;

namespace Goods.System.Social.Network.DAL.Repository.Interface
{
    public interface ITypeReactionRepository
    {
        Task<ReactionType> GetAsync(int id);
        Task<List<ReactionType>> GetAllAsync();
        Task CreateAsync(ReactionType reaction);
        Task UpdateAsync(ReactionType reaction);
        Task DeleteAsync(int id);
    }
}
