namespace Goods.System.Social.Network.DomainModel.Entities
{
    public class NotificationChatRoom
    {
        public int Id { get; set; }
        public int NotificationId { get; set; }
        public Notification? Notification { get; set; }
        public int ChatRoomId { get; set; }
        public ChatRoom? ChatRoom { get; set; }
        public bool IsCheck { get; set; }

    }
}
