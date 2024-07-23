using Goods.System.Social.Network.DAL.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NLog;
namespace Goods.System.Social.Network.DAL.Repository.Realization
{

    public class GalleryRepository : IGalleryRepository
    {
        private readonly IDbContextFactory<UserContext> _contextFactory;
        private readonly ILogger<GalleryRepository> _logger;
        public GalleryRepository(IDbContextFactory<UserContext> dbContextFactory, ILogger<GalleryRepository> logger)
        {
            _logger = logger;
            _contextFactory = dbContextFactory;
        }

        public async Task CreateAsync(IFormFile photo, int userId)
        {
            _logger.LogTrace($"Вызван метод CreateAsync с параметрами: photo : -, userId: {userId}");

            string fileFormat = photo.FileName.Substring(photo.FileName.LastIndexOf('.') + 1);
            string fullPath = $"{Directory.GetCurrentDirectory()}/Galleries/{userId}";
            var paths = GetByUser(userId);
            int photoId = 1;
            if (paths.Count != 0)
            {
                List<int> newPath = new List<int>();
                foreach (var path in paths)
                {
                    int start = path.IndexOf('/') + 1;
                    int end = path.IndexOf('.');
                    newPath.Add(int.Parse(path.Substring(start, end - start)));
                }
                photoId = newPath.Max() + 1;
            }
            fullPath += $"/{photoId}.{fileFormat}";
            using (var fileStream = new FileStream(fullPath, FileMode.Create))
            {
                await photo.CopyToAsync(fileStream);
            }
        }

        public void Delete(int photoId, int userId)
        {
            _logger.LogTrace($"Вызван метод Delete с параметрами: photoId : {photoId}, userId: {userId}");

            var paths = GetByUser(userId);
            string path = paths.Where(p => p.IndexOf($"{userId}/{photoId}") != -1).First();
            string fullPath = $"{Directory.GetCurrentDirectory()}/Galleries/{path}";
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }

        public string Get(int photoId, int userId)
        {
            _logger.LogTrace($"Вызван метод Get с параметрами: photoId : {photoId}, userId: {userId}");

            var paths = GetByUser(userId);
            return paths.Where(p => p.IndexOf($"{userId}/{photoId}") != -1).First();
        }

        public List<string> GetByUser(int userId)
        {
            _logger.LogTrace($"Вызван метод GetByUser с параметрами: userId: {userId}");

            string path = $"{Directory.GetCurrentDirectory()}/Galleries/{userId}";
            if (Directory.Exists(path))
            {
                var paths = Directory.GetFiles(path, "*", SearchOption.AllDirectories).ToList();
                for (int i = 0; i < paths.Count; i++)
                {
                    paths[i] = paths[i][(paths[i].LastIndexOf('/') + 1)..];
                    var oldString = "\\";
                    paths[i] = paths[i].Replace(oldString, "/");
                }
                return paths;
            }
            else { 
                Directory.CreateDirectory(path);
                return new List<string>(); 
            }
        }

        public async Task UpdateAsync(IFormFile photo, int userId, int photoId)
        {
            _logger.LogTrace($"Вызван метод UpdateAsync с параметрами: photo : -, userId: {userId}, photoId: {photoId}");

            Delete(photoId, userId);
            string fileFormat = photo.FileName.Substring(photo.FileName.LastIndexOf('.') + 1);
            string fullPath = $"{Directory.GetCurrentDirectory()}/Galleries/{userId}";
            fullPath += $"/{photoId}.{fileFormat}";
            using (var fileStream = new FileStream(fullPath, FileMode.Create))
            {
                await photo.CopyToAsync(fileStream);
                fileStream.Close();
            }
        }
    }
}