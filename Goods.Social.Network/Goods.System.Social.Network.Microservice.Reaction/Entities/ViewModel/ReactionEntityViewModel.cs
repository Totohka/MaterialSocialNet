using Goods.System.Social.Network.DomainModel.Entities;
using Microsoft.AspNetCore.Http;

namespace DomainModel.Entities.ViewModels
{
    /// <summary>
    /// Модель для создания реакции 
    /// </summary>
    public class ReactionEntityViewModel
    {
        /// <summary>
        /// Id сущности (пост или сообщение или т.д.)
        /// </summary>
        public int EntityId { get; set; }

        /// <summary>
        /// Id типа реакции
        /// </summary>
        public int TypeReactionId { get; set; }

        /// <summary>
        /// Id автора
        /// </summary>
        public int UserId { get; set;}
    }
}
