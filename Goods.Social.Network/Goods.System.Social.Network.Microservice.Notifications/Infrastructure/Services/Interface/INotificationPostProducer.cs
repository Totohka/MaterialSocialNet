namespace Goods.System.Social.Network.Microservice.Posts.Services.Interface
{
    public interface INotificationPostProducer
    {
        public void SendMessage<T>(T message);
    }
}
