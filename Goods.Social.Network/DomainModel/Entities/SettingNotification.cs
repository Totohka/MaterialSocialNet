namespace Goods.System.Social.Network.DomainModel.Entities
{
    public class SettingNotification
    {
        public int Id { get; set; }
        public bool NewMessage { get; set; } = true;
        public bool NewSubscibe { get; set; } = true;
        public bool NewPosts { get; set; } = true;
        public List<User> User { get; set; } 
    }
}
