using Goods.System.Social.Network.DomainModel.Entities;

namespace Goods.System.Social.Network.Microservice.Chats.Entities.DTO
{
    public class UserMessageDTO
    {
        public int Id { get; set; }
        public string Msg { get; set; }
        public DateTime DateSend { get; set; }
        public UserDTO UserDTO { get; set; }
        public List<string> TypeReactions { get; set; } = new List<string>();
        public string TypeReaction { get; set; } = string.Empty;
    }
}