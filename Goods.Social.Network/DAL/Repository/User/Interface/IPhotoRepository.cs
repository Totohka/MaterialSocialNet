using DomainModel.Entities;
using Goods.System.Social.Network.DomainModel.Entities;
using Microsoft.AspNetCore.Http;

namespace Goods.System.Social.Network.DAL.Repository.Interface
{
    public interface IPhotoRepository
    {
        public FileRepositoryEnum FileRepository { get; set; }
        string Get(int userId);
        Task CreateAsync(IFormFile photo, int userId);
        Task UpdateAsync(IFormFile photo, int userId);
        void Delete(int userId);
    }
}
