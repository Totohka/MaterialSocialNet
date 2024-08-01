using System.ComponentModel.DataAnnotations;

namespace Goods.System.Social.Network.Microservice.Chats.Entities.ViewModels
{
    /// <summary>
    /// Модель для редактирования чата
    /// </summary>
    public class ChatRoomUpdateViewModel
    {
        /// <summary>
        /// Id чата
        /// </summary>
        [Required(ErrorMessage = "Параметр Id обязателен")]
        public int Id { get; set; }

        /// <summary>
        /// Название чата
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Параметр Name обязателен")]
        [Range(1, 50, ErrorMessage = "Длина названия от {1} до {2} символов")]
        public string Name { get; set; }

        /// <summary>
        /// Описание чата
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Параметр Description обязателен")]
        [Range(1, 100, ErrorMessage = "Длина описания от {1} до {2} символов")]
        public string Description { get; set; }
    }
}
