namespace Goods.System.Social.Network.Microservice.Users.Entities.ViewModels
{
    public class ChangeSettingNotificationViewModel
    {
        public int user_id { get; set; }
        public bool new_message { get; set; }
        public bool new_subscribe { get; set; }
        public bool new_posts { get; set; }
    }
}
