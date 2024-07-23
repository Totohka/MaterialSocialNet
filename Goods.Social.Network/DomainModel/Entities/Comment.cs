namespace Goods.System.Social.Network.DomainModel.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; } 
        public List<CommentPost>? CommentPosts { get; set; }
    }
}
