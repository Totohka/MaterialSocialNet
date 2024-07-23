using DomainModel.Entities.ViewModels;
using Goods.System.Social.Network.DomainModel.Entities;

namespace Goods.System.Social.Network.DAL.Repository.Interface
{
    public interface IReactionRepository
    {
        Task<Reaction> GetAsync(int userId, int typeId);
        Task<List<Reaction>> GetAllByUserAsync(int userId);
        void Create(Reaction reaction);
        void Update(Reaction reaction);
        Task DeleteAsync(int userId, int typeId);
    }
}
