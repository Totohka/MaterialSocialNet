using System.ComponentModel.DataAnnotations;

namespace Goods.System.Social.Network.Microservice.Comments.Entities.ViewModel
{
    /// <summary>
    /// Модель для редактирования коммента
    /// </summary>
    public class CommentPostUpdateViewModel
    {
        /// <summary>
        /// Id коммента
        /// </summary>
        [Required(ErrorMessage = "Параметр Id обязателен")]
        public int Id { get; set; }

        /// <summary>
        /// Текст коммента
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Параметр Text обязателен")]
        [Range(1, 300, ErrorMessage = "Длина комментария от 1 до 500 символов")]
        public string Text { get; set; }
    }
}
