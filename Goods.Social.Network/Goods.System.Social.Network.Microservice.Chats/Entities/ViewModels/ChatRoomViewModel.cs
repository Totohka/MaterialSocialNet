using Goods.System.Social.Network.DomainModel.Entities;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Goods.System.Social.Network.Microservice.Chats.Entities.ViewModels
{
    public class ChatRoomViewModel : ChatRoom
    {
        [SwaggerSchema(ReadOnly = true)]
        public int Id { get; set; } = 0;

        [SwaggerSchema(ReadOnly = true)]
        public List<ChatRoomUser>? ChatRoomUsers { get; set; } = null;

        [SwaggerSchema(ReadOnly = true)]
        public List<Message>? Messages { get; set; } = null;

        [SwaggerSchema(ReadOnly = true)]
        public List<NotificationChatRoom>? NotificationChatRooms { get; set; } = null;

        public List<int> UserIdsByChat {  get; set; }
    }
}
