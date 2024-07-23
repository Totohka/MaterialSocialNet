using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Goods.System.Social.Network.DomainModel.Entities
{
    public class Notification
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public string? NotificationConnectionId { get; set; }
        public List<NotificationPost>? NotificationPosts { get; set; }
        public List<NotificationChatRoom>? NotificationChatRooms { get; set; }
    }
}
