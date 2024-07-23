namespace Goods.System.Social.Network.DomainModel.Entities
{
    public class Reaction
    {
        public int Id { get; set; }
        public int TypeReactionId { get; set; }
        public ReactionType? TypeReaction { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public List<ReactionMessage>? ReactionMessages { get; set; }
        public List<ReactionPost>? ReactionPosts { get; set; }
    }
}
