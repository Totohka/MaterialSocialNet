using System.ComponentModel.DataAnnotations.Schema;

namespace Goods.System.Social.Network.DomainModel.Entities
{
    public class Message
    {
        public int Id { get; set; }
        public string Msg { get; set; }
        public DateTime DateSend { get; set; }
        public int ChatRoomId { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public ChatRoom? ChatRoom { get; set; }
        public List<ReactionMessage>? ReactionMessages { get; set; }
    }
}