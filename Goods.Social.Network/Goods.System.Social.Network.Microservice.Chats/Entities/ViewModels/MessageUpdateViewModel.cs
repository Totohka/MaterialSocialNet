using System.ComponentModel.DataAnnotations;

namespace Goods.System.Social.Network.Microservice.Chats.Entities.ViewModels
{
    /// <summary>
    /// Модель для редактирования сообщения
    /// </summary>
    public class MessageUpdateViewModel
    {
        /// <summary>
        /// Id сообщения
        /// </summary>
        [Required(ErrorMessage = "Параметр Id обязателен")]
        public int Id { get; set; }

        /// <summary>
        /// Новый текст сообщения
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Параметр Msg обязателен")]
        [Range(1, 500, ErrorMessage = "Длина названия от {1} до {2} символов")]
        public string Msg { get; set; }
    }
}
