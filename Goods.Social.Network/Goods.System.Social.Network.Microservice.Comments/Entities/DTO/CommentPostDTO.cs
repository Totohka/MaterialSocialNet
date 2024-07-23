namespace Goods.System.Social.Network.Microservice.Comments.Entities.DTO
{
    public class CommentPostDTO
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Text { get; set; }
    }
}
