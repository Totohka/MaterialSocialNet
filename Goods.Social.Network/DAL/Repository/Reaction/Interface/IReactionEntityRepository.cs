using DomainModel.Entities.ViewModels;
using Goods.System.Social.Network.DomainModel.Entities;

namespace Goods.System.Social.Network.DAL.Repository.Interface
{
    public interface IReactionEntityRepository<T> where T : class
    {
        Task<T> GetAsync(int id);
        Task<int> GetCountAsync(int entityId);
        Task<List<T>> GetAllByEntityAsync(int entityId, int number);
        Task CreateAsync(T reaction);
        Task UpdateAsync(T reaction);
        Task DeleteAsync(int id);
    }
}
