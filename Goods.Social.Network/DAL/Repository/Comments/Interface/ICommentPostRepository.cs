using Goods.System.Social.Network.DomainModel.Entities;

namespace DAL.Repository.Comments.Interface
{
    public interface ICommentPostRepository
    {
        Task<CommentPost> GetAsync(int id);
        Task<List<CommentPost>> GetAllAsync();
        Task<List<CommentPost>> GetAllByPostAsync(int postId);
        void Create(CommentPost commentPost);
        void Update(CommentPost commentPost);
        Task DeleteAsync(int id);
    }
}
