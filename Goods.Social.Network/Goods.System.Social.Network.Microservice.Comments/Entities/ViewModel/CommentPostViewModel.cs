using System.ComponentModel.DataAnnotations;

namespace Goods.System.Social.Network.Microservice.Comments.Entities.ViewModel
{
    /// <summary>
    /// Модель для создания коммента
    /// </summary>
    public class CommentPostViewModel
    {
        /// <summary>
        /// Id поста
        /// </summary>
        [Required(ErrorMessage = "Параметр PostId обязателен")]
        public int PostId { get; set; }

        /// <summary>
        /// Id юзера
        /// </summary>
        [Required(ErrorMessage = "Параметр UserId обязателен")]
        public int UserId { get; set; }

        /// <summary>
        /// Текст коммента
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Параметр Text обязателен")]
        [Range(1, 300, ErrorMessage = "Длина комментария от {1} до {2} символов")]
        public string Text { get; set; }
    }
}
