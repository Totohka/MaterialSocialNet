using Goods.System.Social.Network.DomainModel.Entities;
using Microsoft.AspNetCore.Http;

namespace Goods.System.Social.Network.DomainServices.Interface
{
    public interface IGalleryService
    {
        public List<string> GetFourPhoto(int userId);
        public List<string> GetByUser(int userId);
        public Task CreateAsync(IFormFile image, int userId);
        public Task UpdateAsync(IFormFile image, int userId, int photoId);
        public void Delete(int photoId, int userId);
    }
}