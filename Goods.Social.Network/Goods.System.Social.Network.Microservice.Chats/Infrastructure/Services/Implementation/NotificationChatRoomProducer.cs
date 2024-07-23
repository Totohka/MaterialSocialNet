using Goods.System.Social.Network.Microservice.Chats.Services.Interface;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace Goods.System.Social.Network.Microservice.Chats.Services.Implementation
{
    public class NotificationChatRoomProducer : INotificationChatRoomProducer
    {
        public void SendMessage<T>(T message)
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare("notifications", 
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish(exchange: "", 
                routingKey: "notifications", 
                basicProperties: null,
                body: body);
        }
    }
}
