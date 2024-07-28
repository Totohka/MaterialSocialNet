namespace Goods.System.Social.Network.Microservice.Users.Entities.ViewModels
{
    public class ChangeSettingNotificationViewModel
    {
        public int UserId { get; set; }
        public bool NewMessage { get; set; }
        public bool NewSubscribe { get; set; }
        public bool NewPosts { get; set; }
    }
}
