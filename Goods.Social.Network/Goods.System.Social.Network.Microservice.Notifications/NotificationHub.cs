using Microsoft.AspNetCore.SignalR;
using Goods.System.Social.Network.DomainServices.Interface;
using Newtonsoft.Json;

namespace Goods.System.Social.Network.Microservice.Notifications
{
    public class NotificationHub : Hub
    {
        private readonly INotificationService _notificationService; 
        public NotificationHub(INotificationService notificationService) 
        {
            _notificationService = notificationService;
        }

        public async Task OnConnectedSendNotifications(string userId)
        {
            var notification = await _notificationService.GetAllByUserAsync(int.Parse(userId));            
            if (notification is not null)
            {
                foreach (var item in notification.NotificationPosts)
                {
                    item.Notification = null;
                }
                foreach (var item in notification.NotificationChatRooms)
                {
                    item.Notification = null;
                }
                notification.NotificationConnectionId = Context.ConnectionId;
                string json = JsonConvert.SerializeObject(notification);
                await Clients.Client(Context.ConnectionId).SendAsync("SendNotification", json);
                await _notificationService.UpdateAsync(notification);
            }
            else
            {
                await _notificationService.CreateAsync(int.Parse(userId));
                notification = await _notificationService.GetAllByUserAsync(int.Parse(userId));
                notification.NotificationConnectionId = Context.ConnectionId;
                await _notificationService.UpdateAsync(notification);
                await Clients.Client(Context.ConnectionId).SendAsync("SendNotification", "{}");
            }
        }

        public async Task OnDisconnectedSendNotifications(string userId)
        {
            var notification = await _notificationService.GetAllByUserAsync(int.Parse(userId));
            if (notification is not null)
            {
                notification.NotificationConnectionId = "Disconnected";
                await _notificationService.UpdateAsync(notification);
            }
            else
            {
                await _notificationService.CreateAsync(int.Parse(userId));
            }
        }

        //public override async Task OnDisconnectedAsync(Exception exception)
        //{
        //    await Clients.All.SendAsync("Notify", $"{Context.ConnectionId} покинул");
        //    await base.OnDisconnectedAsync(exception);
        //}
    }
}