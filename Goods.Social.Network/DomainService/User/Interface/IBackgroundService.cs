using Goods.System.Social.Network.DomainModel.Entities;
using Microsoft.AspNetCore.Http;

namespace Goods.System.Social.Network.DomainServices.Interface
{
    public interface IBackgroundService
    {
        public string Get(int userId);
        public Task CreateAsync(IFormFile image, int userId);
        public Task UpdateAsync(IFormFile image, int userId);
        public void Delete(int userId);
    }
}