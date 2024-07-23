using Goods.System.Social.Network.DomainModel.Entities;

namespace DAL.Repository.Comments.Interface
{
    public interface ICommentRepository
    {
        Task<Comment> GetAsync(int id);
        Task<List<Comment>> GetAllAsync();
        Task<Comment> GetByUserAsync(int userId);
        void Create(Comment comment);
        void Update(Comment comment);
        Task DeleteAsync(int id);
    }
}
