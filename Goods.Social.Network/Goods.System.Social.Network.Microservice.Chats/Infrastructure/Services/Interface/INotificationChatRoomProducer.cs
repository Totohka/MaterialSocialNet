namespace Goods.System.Social.Network.Microservice.Chats.Services.Interface
{
    public interface INotificationChatRoomProducer
    {
        public void SendMessage<T>(T message);
    }
}
