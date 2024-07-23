namespace Goods.System.Social.Network.Microservice.Chats.Entities.ViewModels
{
    public class MessageViewModel
    {
        public string message { get; set; }
        public DateTime date_send { get; set; }
        public int chat_room_id { get; set; }
        public int user_id { get; set; }
        public string first_name { get; set;}
        public string last_name { get; set; }
    }
}
