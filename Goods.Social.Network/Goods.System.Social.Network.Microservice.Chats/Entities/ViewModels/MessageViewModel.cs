using System.ComponentModel.DataAnnotations;

namespace Goods.System.Social.Network.Microservice.Chats.Entities.ViewModels
{
    /// <summary>
    /// Модель для создания сообщения
    /// </summary>
    public class MessageViewModel
    {
        /// <summary>
        /// Текст сообщения
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Параметр Message обязателен")]
        [Range(1, 500, ErrorMessage = "Длина сообщения от 1 до 500 символов")]
        public string Message { get; set; }

        /// <summary>
        /// Дата написания сообщения
        /// </summary>
        [Required(ErrorMessage = "Параметр DateSend обязателен")]
        public DateTime DateSend { get; set; }

        /// <summary>
        /// Id чата
        /// </summary>
        [Required(ErrorMessage = "Параметр ChatRoomId обязателен")]
        public int ChatRoomId { get; set; }

        /// <summary>
        /// Id юзера автора
        /// </summary>
        [Required(ErrorMessage = "Параметр UserId обязателен")]
        public int UserId { get; set; }

        /// <summary>
        /// Имя юзера
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Параметр FirstName обязателен")]
        [Range(1, 25, ErrorMessage = "Длина имени от {1} до {2} символов")]
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия юзера
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Параметр LastName обязателен")]
        [Range(1, 25, ErrorMessage = "Длина фамилии от {1} до {2} символов")]
        public string LastName { get; set; }
    }
}
