namespace Goods.System.Social.Network.DomainModel.Entities
{
    /// <summary>
    /// Тип реакции
    /// </summary>
    public class ReactionType
    {
        /// <summary>
        /// id типа
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название типа
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание типа
        /// </summary>
        public string Description { get; set; }

        public List<Reaction>? Reactions { get; set; }
    }
}
