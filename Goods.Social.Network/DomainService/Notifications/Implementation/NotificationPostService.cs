using DAL.Repository.Notifications.Interface;
using DomainModel.Entities.ViewModels;
using Goods.System.Social.Network.DAL.Repository.Interface;
using Goods.System.Social.Network.DomainModel.Entities;
using Goods.System.Social.Network.DomainServices.Interface;
using Microsoft.AspNetCore.Http;

namespace DomainServices.Notifications.Implementation
{
    public class NotificationPostService : INotificationPostService
    {
        private readonly INotificationPostRepository _notificationPostRepository;
        private readonly INotificationRepository _notificationRepository;

        public NotificationPostService(INotificationPostRepository notificationPostRepository,
                                       INotificationRepository notificationRepository)
        { 
            _notificationPostRepository = notificationPostRepository;
            _notificationRepository = notificationRepository;
        }
        public async Task CreateAsync(bool isCheck, int userId, int userPostId, int postId)
        {
            var notification = new Notification { UserId = userId };
            int id = await _notificationRepository.CreateAsync(notification);
            var notificationPost = new NotificationPost { IsCheck = isCheck, PostId = postId, UserId = userPostId, NotificationId = id };
            await _notificationPostRepository.CreateAsync(notificationPost);
        }

        public Task<List<NotificationPost>> GetAllByNotificationAsync(int notificationId)
        {
            throw new NotImplementedException();
        }

        public Task<NotificationPost> GetAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}