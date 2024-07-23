using Microsoft.AspNetCore.Http;

namespace Goods.System.Social.Network.DAL.Repository.Interface
{
    public interface IImagePostRepository
    {
        string Get(int userId, int postId);
        List<string> GetByUserId(int userId);
        Task CreateAsync(IFormFile file, int userId, int postId);
        Task UpdateAsync(IFormFile file, int userId, int postId);
        void Delete(int userId, int postId);
    }
}
