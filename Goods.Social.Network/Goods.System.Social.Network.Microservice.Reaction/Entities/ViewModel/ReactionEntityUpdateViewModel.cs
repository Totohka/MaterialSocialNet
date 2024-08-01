using Goods.System.Social.Network.DomainModel.Entities;
using Microsoft.AspNetCore.Http;

namespace DomainModel.Entities.ViewModels
{
    /// <summary>
    /// Модель для смены реакции 
    /// </summary>
    public class ReactionEntityUpdateViewModel
    {
        /// <summary>
        /// Id сущности (сообщения или поста или т.д.)
        /// </summary>
        public int EntityId { get; set; }

        /// <summary>
        /// Id типа новой реакции
        /// </summary>
        public int TypeReactionId { get; set; }

        /// <summary>
        /// Id типа старой реакции
        /// </summary>
        public int TypeReactionOldId { get; set; }

        /// <summary>
        /// Id автора
        /// </summary>
        public int UserId { get; set; }
    }
}
