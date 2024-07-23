namespace Goods.System.Social.Network.DomainModel.Entities
{
    public class ReactionMessage
    {
        public int Id { get; set; }
        public int ReactionId { get; set; }
        public Reaction? Reaction { get; set; }
        public int MessageId { get; set; }
        public Message? Message { get; set; } 
    }
}
