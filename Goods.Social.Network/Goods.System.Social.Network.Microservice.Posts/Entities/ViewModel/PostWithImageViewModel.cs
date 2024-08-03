using Goods.System.Social.Network.DomainModel.Entities;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Goods.System.Social.Network.Microservice.Posts.Entities.ViewModel
{
    /// <summary>
    /// Модель для создания поста
    /// </summary>
    public class PostWithImageViewModel
    {
        /// <summary>
        /// Изображение
        /// </summary>
        [Required(ErrorMessage = "Параметр Image обязателен")]
        public IFormFile Image { get; set; }

        /// <summary>
        /// Заголовок
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Параметр Title обязателен")]
        [StringLength(100, ErrorMessage = "Длина заголовка от 1 до {1} символов")]
        public string Title { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Параметр DateCreate обязателен")]
        public DateTime DateCreate { get; set; }

        /// <summary>
        /// Id автора
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Параметр UserId обязателен")]
        public int UserId { get; set; }

        /// <summary>
        /// Текст поста
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Параметр Text обязателен")]
        public string Text { get; set; }

        /// <summary>
        /// Теги поста
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Параметр Tags обязателен")]
        [StringLength(100, ErrorMessage = "Длина тега от 1 до {1} символов")]
        public string Tags { get; set; }
    }
}
