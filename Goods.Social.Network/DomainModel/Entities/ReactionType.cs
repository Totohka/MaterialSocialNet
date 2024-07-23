namespace Goods.System.Social.Network.DomainModel.Entities
{
    public class ReactionType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Reaction>? Reactions { get; set; }
    }
}
