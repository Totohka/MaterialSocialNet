﻿using DomainModel.Entities;
using Goods.System.Social.Network.DAL.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NLog;
namespace Goods.System.Social.Network.DAL.Repository.Realization
{

    public class BackgroundRepository : IPhotoRepository
    {
        private readonly IDbContextFactory<UserContext> _contextFactory;
        private readonly ILogger<BackgroundRepository> _logger;
        public FileRepositoryEnum FileRepository { get; set; } = FileRepositoryEnum.Background;
        public BackgroundRepository(IDbContextFactory<UserContext> dbContextFactory, ILogger<BackgroundRepository> logger)
        {
            _logger = logger;
            _contextFactory = dbContextFactory;
        }

        public async Task CreateAsync(IFormFile photo, int userId)
        {
            _logger.LogTrace($"Вызван метод CreateAsync с параметрами: photo : -, userId: {userId}");

            string fileFormat = photo.FileName.Substring(photo.FileName.LastIndexOf('.') + 1);
            string fullPath = $"{Directory.GetCurrentDirectory()}/Backgrounds/{userId}.{fileFormat}";
            using (var fileStream = new FileStream(fullPath, FileMode.Create))
            {
                await photo.CopyToAsync(fileStream);
            }
            using var db = _contextFactory.CreateDbContext();
            var user = await db.Users.FindAsync(userId);
            user.Background = $"{userId}.{fileFormat}";
            db.SaveChanges();
        }

        public void Delete(int userId)
        {
            _logger.LogTrace($"Вызван метод Delete с параметрами: userId: {userId}");

            var paths = Directory.GetFiles($"{Directory.GetCurrentDirectory()}/Backgrounds/", "*", SearchOption.AllDirectories).ToList();
            string path = paths.Where(p => p.IndexOf($"{userId}") != -1).FirstOrDefault("");
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public string Get(int userId)
        {
            _logger.LogTrace($"Вызван метод Get с параметрами: userId: {userId}");

            var paths = Directory.GetFiles($"{Directory.GetCurrentDirectory()}/Backgrounds/", "*", SearchOption.AllDirectories).ToList();
            var path = paths.Where(p => p.IndexOf($"{userId}.") != -1).FirstOrDefault("0.jpeg");
            path = path[(path.LastIndexOf('/') + 1)..];
            var oldString = "\\";
            path = path.Replace(oldString, "/");
            return path;
        }
        public async Task UpdateAsync(IFormFile photo, int userId)
        {
            _logger.LogTrace($"Вызван метод UpdateAsync с параметрами: photo : -, userId: {userId}");

            Delete(userId);
            await CreateAsync(photo, userId);
        }
    }
}