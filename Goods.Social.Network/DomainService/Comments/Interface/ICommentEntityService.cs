using DomainModel.Entities.ViewModels;
using Goods.System.Social.Network.DomainModel.Entities;
using Microsoft.AspNetCore.Http;

namespace DomainServices.Comments.Interface
{
    public interface ICommentEntityService<T>
        where T : class //CommentPost
    {
        Task<List<T>> GetAllByEntityAsync(int entityId);
        Task<int> CreateAsync(int entityId, int userId, string text);
        Task UpdateAsync(int id, string text);
        Task DeleteAsync(int id);
    }
}