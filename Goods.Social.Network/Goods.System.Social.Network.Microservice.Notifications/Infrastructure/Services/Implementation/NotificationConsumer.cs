using DomainServices.Chat.Interface;
using Goods.System.Social.Network.DomainModel.Entities;
using Goods.System.Social.Network.DomainServices.Interface;
using Goods.System.Social.Network.Microservice.Notifications;
using Goods.System.Social.Network.Microservice.Notifications.Entities.DTO;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using BackgroundService = Microsoft.Extensions.Hosting.BackgroundService;

namespace Goods.System.Social.Network.Microservice.Posts.Services.Implementation
{
    public class NotificationConsumer : BackgroundService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IHubContext<NotificationHub> _notificationHub;
        private readonly INotificationPostService _notificationPostService;
        private readonly INotificationChatRoomService _notificationChatRoomService;
        private readonly INotificationService _notificationService;
        private readonly IUserService _userService;
        private readonly IChatService _chatService;

        public NotificationConsumer(IHubContext<NotificationHub> notificationHub, 
                                    INotificationPostService notificationPostService,
                                    INotificationService notificationService,
                                    IUserService userService,
                                    INotificationChatRoomService notificationChatRoomService,
                                    IChatService chatService)
        {
            _notificationHub = notificationHub;
            _notificationPostService = notificationPostService;
            _notificationService = notificationService;
            _userService = userService;
            var factory = new ConnectionFactory { HostName = "localhost" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "notifications", durable: false, exclusive: false, autoDelete: false, arguments: null);
            _notificationChatRoomService = notificationChatRoomService;
            _chatService = chatService;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += Consume;

            _channel.BasicConsume("notifications", false, consumer);
        
            return Task.CompletedTask;
        }

        private async void Consume(object? ch, BasicDeliverEventArgs eventArgs)
        {
            var content = Encoding.UTF8.GetString(eventArgs.Body.ToArray());
            if (content.Contains("PostId"))
            {
                DataNotificationPost dataNotificationPost = JsonConvert.DeserializeObject<DataNotificationPost>(content);
                Notification notification = _notificationService.GetAllByUser(dataNotificationPost.SubscriberId);
                if (notification is not null && notification.NotificationConnectionId == "Disconnected")
                {
                    await _notificationPostService.CreateAsync(false,
                                                         dataNotificationPost.SubscriberId,
                                                         dataNotificationPost.AuthorId,
                                                         dataNotificationPost.PostId);
                }
                else if (notification is not null)
                {
                    await _notificationPostService.CreateAsync(true,
                                                         dataNotificationPost.SubscriberId,
                                                         dataNotificationPost.AuthorId,
                                                         dataNotificationPost.PostId);
                    var user = await _userService.GetAsync(dataNotificationPost.AuthorId);
                    await _notificationHub.Clients.Client(notification.NotificationConnectionId)
                            .SendAsync("NotificationPost",
                                       user.FirstName,
                                       user.LastName,
                                       user.Id.ToString(),
                                       dataNotificationPost.PostId.ToString());
                }
            }
            else if (content.Contains("ChatRoomId"))
            {
                DataNotificationChatRoom dataNotificationChatRoom = JsonConvert.DeserializeObject<DataNotificationChatRoom>(content);
                Notification notification = _notificationService.GetAllByUser(dataNotificationChatRoom.UserId);
                if (notification is not null && notification.NotificationConnectionId == "Disconnected")
                {
                    await _notificationChatRoomService.CreateAsync(false,
                                                                   dataNotificationChatRoom.UserId,
                                                                   dataNotificationChatRoom.ChatRoomId);
                }
                else if (notification is not null)
                {
                    await _notificationChatRoomService.CreateAsync(true,
                                                                   dataNotificationChatRoom.UserId,
                                                                   dataNotificationChatRoom.ChatRoomId);
                    var chat = await _chatService.GetAsync(dataNotificationChatRoom.ChatRoomId);
                    await _notificationHub.Clients.Client(notification.NotificationConnectionId)
                            .SendAsync("NotificationChat",
                                       chat.Name,
                                       chat.LastMessage,
                                       dataNotificationChatRoom.ChatRoomId.ToString());
                }
            }
            
            _channel.BasicAck(eventArgs.DeliveryTag, false);
        }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }


}
