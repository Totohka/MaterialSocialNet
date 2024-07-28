namespace Goods.System.Social.Network.Microservice.Chats.Entities.ViewModels
{
    public class MessageViewModel
    {
        public string Message { get; set; }
        public DateTime DateSend { get; set; }
        public int ChatRoomId { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
