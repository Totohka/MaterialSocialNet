using Goods.System.Social.Network.DomainModel.Entities;

namespace Goods.System.Social.Network.DAL.Repository.Interface
{
    public interface IPostRepository
    {
        Task<Post> GetAsync(int id);
        Task<List<Post>> GetByUserAsync(int userId);
        Task<List<Post>> GetAllAsync();
        void Create(Post item);
        void Update(Post item);
        Task DeleteAsync(int id);
    }
}
