namespace Goods.System.Social.Network.DomainModel.Entities
{
    public class NotificationPost
    {
        public int Id { get; set; }
        public int NotificationId { get; set; }
        public Notification? Notification { get; set; }
        public int? UserId { get; set; }
        public User? User { get; set; }
        public int? PostId { get; set; }
        public Post? Post { get; set; }
        public bool IsCheck { get; set; }

    }
}
