using System.ComponentModel.DataAnnotations;

namespace Goods.System.Social.Network.Microservice.Chats.Entities.ViewModels
{
    /// <summary>
    /// Модель связи чата и юзера
    /// </summary>
    public class ChatRoomUserViewModel
    {
        /// <summary>
        /// Id чата
        /// </summary>
        [Required(ErrorMessage = "Параметр ChatRoomId обязателен")]
        public int ChatRoomId { get; set; }

        /// <summary>
        /// Id юзера
        /// </summary>
        [Required(ErrorMessage = "Параметр UserId обязателен")]
        public int UserId { get; set; }
    }
}
