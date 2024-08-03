using System.ComponentModel.DataAnnotations;

namespace Goods.System.Social.Network.Microservice.Chats.Entities.ViewModels
{
    /// <summary>
    /// Модель для создания чата
    /// </summary>
    public class ChatRoomViewModel
    {
        /// <summary>
        /// Название чата
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Параметр Name обязателен")]
        [StringLength(50, ErrorMessage = "Длина названия  от {1} до {2} символов")]
        public string Name { get; set; }

        /// <summary>
        /// Описание чата
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Параметр Description обязателен")]
        [StringLength(50, ErrorMessage = "Длина названия от 1 до {1} символов")]
        public string Description { get; set; }

        /// <summary>
        /// Id юзера, который создаёт чат
        /// </summary>
        [Required(ErrorMessage = "Параметр UserId обязателен")]
        public int UserId { get; set; }

        /// <summary>
        /// Id всех юзеров, которых нужно пригласить в чат при его создании
        /// Если никого не нужно приглашать, то отправлять список пустым
        /// </summary>
        [Required(ErrorMessage = "Параметр UserIdsByChat обязателен")]
        public List<int> UserIdsByChat {  get; set; }
    }
}
