using Goods.System.Social.Network.DomainModel.Entities;

namespace Goods.System.Social.Network.Microservice.Chats.Entities.DTO
{
    /// <summary>
    /// Модель сообщения DTO
    /// </summary>
    public class UserMessageDTO
    {
        /// <summary>
        /// Id сообщения
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Текст сообщения
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// Дата написания сообщения
        /// </summary>
        public DateTime DateSend { get; set; }

        /// <summary>
        /// Модель юзера DTO
        /// </summary>
        public UserDTO UserDTO { get; set; }

        /// <summary>
        /// Все реакции на сообщение
        /// </summary>
        public List<string> TypeReactions { get; set; } = new List<string>();

        /// <summary>
        /// Реакция юзера в твоей учётке
        /// </summary>
        public string TypeReaction { get; set; } = string.Empty;
    }
}