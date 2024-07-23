namespace Goods.System.Social.Network.DomainModel.Entities
{
    public class ChatRoomUser
    {
        public int Id { get; set; }
        public int ChatRoomId { get; set; }
        public int UserId { get; set; }
        public ChatRoom? ChatRoom { get; set; }
        public User? User { get; set; }   
    }
}