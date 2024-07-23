using DAL.Repository.Notifications.Interface;
using DomainModel.Entities.ViewModels;
using Goods.System.Social.Network.DAL.Repository.Interface;
using Goods.System.Social.Network.DomainModel.Entities;
using Goods.System.Social.Network.DomainServices.Interface;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;

namespace DomainServices.Notifications.Implementation
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationPostRepository _notificationPostRepository;
        private readonly INotificationChatRoomRepository _notificationChatRoomRepository;
        private readonly INotificationRepository _notificationRepository;

        public NotificationService(INotificationPostRepository notificationPostRepository,
                                   INotificationRepository notificationRepository,
                                   INotificationChatRoomRepository notificationChatRoomRepository)
        { 
            _notificationPostRepository = notificationPostRepository;
            _notificationRepository = notificationRepository;
            _notificationChatRoomRepository = notificationChatRoomRepository;
        }

        public async Task CreateAsync(int userId)
        {
            var notification = new Notification { UserId = userId, NotificationConnectionId = "Disconnected" };
            await _notificationRepository.CreateAsync(notification);
        }

        public Notification GetAllByUser(int userId)
        {
            return _notificationRepository.GetAllByUser(userId);
        }

        public async Task<Notification> GetAllByUserAsync(int userId)
        {
            return await _notificationRepository.GetAllByUserAsync(userId);
        }

        public Task<NotificationPost> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(Notification notification)
        {
            await _notificationRepository.UpdateAsync(notification);

            notification = await _notificationRepository.GetAsync(notification.Id);
            if (notification.NotificationConnectionId != "Disconnected")
            {
                foreach (var item in notification.NotificationPosts)
                {
                    item.IsCheck = true;
                    await _notificationPostRepository.UpdateAsync(item);
                }
                foreach (var item in notification.NotificationChatRooms)
                {
                    item.IsCheck = true;
                    await _notificationChatRoomRepository.UpdateAsync(item);
                }
            }
        }
    }
}