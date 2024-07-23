namespace Goods.System.Social.Network.DomainModel.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public DateTime DateCreate { get; set; } 
        public int UserId { get; set; }
        public User? User { get; set; } 
        public string Text { get; set; } 
        public string Tags { get; set; } 
        public List<CommentPost>? CommentPosts { get; set;}
        public List<ReactionPost>? ReactionPosts { get; set; }
        public List<NotificationPost>? NotificationPosts { get; set; }
    }
}
