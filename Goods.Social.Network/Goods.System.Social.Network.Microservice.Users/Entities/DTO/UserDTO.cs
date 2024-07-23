namespace Goods.System.Social.Network.Microservice.Users.Entities.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateBirthday { get; set; }
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool IsSubscriber { get; set; }
        public string Avatar { get; set; } = "0.jpeg";
        public string Background { get; set; } = "0.jpeg";
    }
}