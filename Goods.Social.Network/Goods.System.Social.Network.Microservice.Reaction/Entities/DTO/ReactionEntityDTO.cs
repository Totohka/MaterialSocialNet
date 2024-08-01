namespace Goods.System.Social.Network.Microservice.Reaction.Entities.DTO
{
    /// <summary>
    /// Реакция на сущность
    /// </summary>
    public class ReactionEntityDTO
    {
        /// <summary>
        /// Id самой реакции на сущность
        /// </summary>
        public int ReactionEntityId { get; set; }

        /// <summary>
        /// Id сущности
        /// </summary>
        public int EntityId { get; set; }

        /// <summary>
        /// Id типа реакции
        /// </summary>
        public int TypeReactionId { get; set; }

        /// <summary>
        /// Тип реакции
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Id автора
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Имя автора
        /// </summary>
        public string FirstNameUser { get; set; }

        /// <summary>
        /// Фамилия автора
        /// </summary>
        public string LastNameUser { get; set; }
    }
}