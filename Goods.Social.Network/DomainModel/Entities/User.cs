namespace Goods.System.Social.Network.DomainModel.Entities
{
    public class User
    {
        public int Id { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password {  get; set; }
        public DateTime DateBirthday { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; } 
        public string Status { get; set; } 
        public string Avatar { get; set; } = "0.jpeg";
        public string Background { get; set; } = "0.jpeg";
        public int SettingNotificationId { get; set; } = 1;
        public SettingNotification? SettingNotification { get; set; }
        public int SettingPrivacyId { get; set; } = 1;
        public SettingPrivacy? SettingPrivacy { get; set; }
        public string Email { get; set; }
        public List<UserFriend>? Friends { get; set; }
        public List<Post>? Posts { get; set; } 
        public List<ChatRoomUser>? ChatRooms { get; set; }
        public List<Comment>? Comments { get; set; } 
        public List<Reaction>? Reactions { get; set; }
        public List<Message>? Messages { get; set; }
        public List<NotificationPost>? NotificationPosts { get; set; }
    }
}