namespace Goods.System.Social.Network.DomainModel.Entities
{
    public class CommentPost
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int CommentId { get; set; }
        public Comment? Comment { get; set; } 
        public int PostId { get; set; }
        public Post? Post { get; set; } 
    }
}
