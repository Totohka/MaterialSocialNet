using Goods.System.Social.Network.DomainModel.Entities;
using Microsoft.AspNetCore.Http;

namespace Goods.System.Social.Network.DAL.Repository.Interface
{
    public interface IGalleryRepository
    {
        string Get(int photoId, int userId);
        List<string> GetByUser(int userId);
        Task CreateAsync(IFormFile photo, int userId);
        Task UpdateAsync(IFormFile photo, int userId, int photoId);
        void Delete(int photoId, int userId);
    }
}
