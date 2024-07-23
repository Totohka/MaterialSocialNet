using DAL.Repository.Notifications.Interface;
using DomainModel.Entities.ViewModels;
using Goods.System.Social.Network.DAL.Repository.Interface;
using Goods.System.Social.Network.DomainModel.Entities;
using Goods.System.Social.Network.DomainServices.Interface;
using Microsoft.AspNetCore.Http;

namespace DomainServices.Notifications.Implementation
{
    public class NotificationChatRoomService : INotificationChatRoomService
    {
        private readonly INotificationChatRoomRepository _notificationChatRoomRepository;
        private readonly INotificationRepository _notificationRepository;

        public NotificationChatRoomService(INotificationChatRoomRepository notificationChatRoomRepository,
                                           INotificationRepository notificationRepository)
        {
            _notificationChatRoomRepository = notificationChatRoomRepository;
            _notificationRepository = notificationRepository;
        }
        public async Task CreateAsync(bool isCheck, int userId, int chatRoomId)
        {
            var notification = new Notification { UserId = userId };
            int id = await _notificationRepository.CreateAsync(notification);
            var notificationsChat = await _notificationChatRoomRepository.GetAllByNotificationAsync(id);
            var notificationChat = notificationsChat.Where(nc => nc.ChatRoomId == chatRoomId).FirstOrDefault();
            if (notificationChat is null) {
                var notificationChatRoom = new NotificationChatRoom { ChatRoomId = chatRoomId, IsCheck = isCheck, NotificationId = id };
                await _notificationChatRoomRepository.CreateAsync(notificationChatRoom);
            }
            else
            {
                notificationChat.IsCheck = isCheck;
                await _notificationChatRoomRepository.UpdateAsync(notificationChat);
            }
        }

        public Task<List<NotificationChatRoom>> GetAllByNotificationAsync(int notificationId)
        {
            throw new NotImplementedException();
        }

        public Task<NotificationChatRoom> GetAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}