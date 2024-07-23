namespace Goods.System.Social.Network.Microservice.Chats.Entities.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Avatar { get; set; } = "0.jpeg";
    }
}