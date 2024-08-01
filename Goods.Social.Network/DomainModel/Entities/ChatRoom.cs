using System.Text.Json.Serialization;

namespace Goods.System.Social.Network.DomainModel.Entities
{
    public class ChatRoom
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string LastMessage { get; set; }
        public List<ChatRoomUser>? ChatRoomUsers { get; set; }
        public List<Message>? Messages { get; set; }
        public List<NotificationChatRoom>? NotificationChatRooms { get; set; }
    }
}