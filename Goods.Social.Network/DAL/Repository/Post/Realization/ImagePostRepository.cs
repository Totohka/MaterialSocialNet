using Goods.System.Social.Network.DAL.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NLog; 
namespace Goods.System.Social.Network.DAL.Repository.Realization
{

    public class ImagePostRepository : IImagePostRepository
    {
        private readonly IDbContextFactory<UserContext> _contextFactory;
        private readonly ILogger<ImagePostRepository> _logger;
        public ImagePostRepository(IDbContextFactory<UserContext> dbContextFactory, ILogger<ImagePostRepository> logger)
        {
            _logger = logger;
            _contextFactory = dbContextFactory;
        }

        public async Task CreateAsync(IFormFile file, int userId, int postId)
        {
            _logger.LogTrace($"Вызван метод CreateAsync с параметрами: file: -, userId: {userId}, postId: {postId}");

            var uploadPath = $"{Directory.GetCurrentDirectory()}/PostsData/{userId}";
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }
            string fullPath = $"{uploadPath}/{postId}.jpeg";
            using (var fileStream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
        }

        public void Delete(int userId, int postId)
        {
            _logger.LogTrace($"Вызван метод Delete с параметрами: userId: {userId}, postId: {postId}");

            string fullPath = $"{Directory.GetCurrentDirectory()}/PostsData/{userId}/{postId}.jpeg";
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }

        public string Get(int userId, int id)
        {
            _logger.LogTrace($"Вызван метод Get с параметрами: userId: {userId}, id: {id}");

            var paths = Directory.GetFiles($"{Directory.GetCurrentDirectory()}/PostsData/{userId}/", "*", SearchOption.AllDirectories).ToList();
            var path = paths.Where(p => p.IndexOf($"/{userId}/{id}.") != -1).FirstOrDefault();
            string fileFormat = path.Substring(path.LastIndexOf('.') + 1);
            return $"{userId}/{id}.{fileFormat}";
        }

        public List<string> GetByUserId(int userId)
        {
            _logger.LogTrace($"Вызван метод GetByUserId с параметрами: userId: {userId}");

            string path = $"{Directory.GetCurrentDirectory()}/PostsData/{userId}";
            var paths = Directory.GetFiles(path, "*", SearchOption.AllDirectories).ToList();
            for (int i = 0; i < paths.Count; i++)
            {
                paths[i] = paths[i][(paths[i].LastIndexOf('/') + 1)..];
                var oldString = "\\";
                paths[i] = paths[i].Replace(oldString, "/");
            }
            return paths;
        }   

        public async Task UpdateAsync(IFormFile file, int userId, int postId)
        {
            _logger.LogTrace($"Вызван метод UpdateAsync с параметрами: file: -, userId: {userId}, postId: {postId}");

            Delete(postId, userId);
            await CreateAsync(file, userId, postId);
        }
    }
}