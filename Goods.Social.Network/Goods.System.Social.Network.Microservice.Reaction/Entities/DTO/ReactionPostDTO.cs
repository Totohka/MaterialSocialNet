namespace Goods.System.Social.Network.Microservice.Reaction.Entities.DTO
{
    public class ReactionPostDTO
    {
        public int ReactionPostId { get; set; }
        public int PostId { get; set; }
        public int TypeReactionId { get; set; }
        public string Type { get; set; }
        public int UserId { get; set; }
        public string FirstNameUser { get; set; }
        public string LastNameUser { get; set; }
    }
}