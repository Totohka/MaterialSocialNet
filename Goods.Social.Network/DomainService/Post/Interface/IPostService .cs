using DomainModel.Entities.ViewModels;
using Goods.System.Social.Network.DomainModel.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Goods.System.Social.Network.DomainServices.Interface
{
    public interface IPostService
    {
        Task<Post> GetAsync(int id);
        Task<List<Post>> GetByUserAsync(int userId);
        Task<PagePost> GetAllAsync(string search = "", int userId = 0, int number = 0);
        Task<int> CreateAsync(Post post, IFormFile image);
        void Delete(int userId, int postId);
        Task UpdateAsync(Post post, IFormFile image);
    }
}