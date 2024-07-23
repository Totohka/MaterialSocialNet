namespace Goods.System.Social.Network.Microservice.Posts.Entities.DTO
{
    public class PostDTO
    {
        public int Id { get; set; }
        public string Image { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public DateTime DateCreate { get; set; }
        public int UserId { get; set; }
        public string FirstNameUser { get; set; } = string.Empty;
        public string LastNameUser { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public string Tags { get; set; } = string.Empty;
        public int Rating { get; set; }
        public string TypeReaction { get; set; } = string.Empty;
    }
}