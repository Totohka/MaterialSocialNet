namespace Goods.System.Social.Network.DomainModel.Entities
{
    public class ReactionPost
    {
        public int Id { get; set; }
        public int ReactionId { get; set; }
        public Reaction? Reaction { get; set; }
        public int PostId { get; set; }
        public Post? Post { get; set; } 
    }
}
