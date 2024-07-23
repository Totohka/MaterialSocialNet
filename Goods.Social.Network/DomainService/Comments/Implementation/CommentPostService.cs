using DAL.Repository.Comments.Interface;
using DomainServices.Comments.Interface;
using Goods.System.Social.Network.DomainModel.Entities;

namespace DomainServices.Comments.Implementation
{
    public class CommentPostService<T> : ICommentEntityService<T>
        where T : CommentPost
    {
        private readonly ICommentPostRepository _commentPostRepository;
        private readonly ICommentRepository _commentRepository;
        public CommentPostService(ICommentPostRepository commentPostRepository, 
                                  ICommentRepository commentRepository)
        {
            _commentPostRepository = commentPostRepository;
            _commentRepository = commentRepository;
        }
        public async Task CreateAsync(int entityId, int userId, string text)
        {
            Comment comment = new Comment { UserId = userId };
            _commentRepository.Create(comment);
            comment = await _commentRepository.GetByUserAsync(userId);
            CommentPost commentPost = new CommentPost { CommentId = comment.Id, PostId = entityId, Text = text };
            _commentPostRepository.Create(commentPost);
        }

        public async Task DeleteAsync(int id)
        {
            await _commentPostRepository.DeleteAsync(id);
        }

        public async Task<List<T>> GetAllByEntityAsync(int postId)
        {
            return await _commentPostRepository.GetAllByPostAsync(postId) as List<T>;
        }

        public async Task UpdateAsync(int id, string text)
        {
            var commentPost = await _commentPostRepository.GetAsync(id);
            commentPost.Text = text;
            _commentPostRepository.Update(commentPost);
        }
    }
}
