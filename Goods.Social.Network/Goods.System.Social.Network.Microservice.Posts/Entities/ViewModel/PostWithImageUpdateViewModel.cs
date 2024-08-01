using Goods.System.Social.Network.DomainModel.Entities;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Goods.System.Social.Network.Microservice.Posts.Entities.ViewModel
{
    /// <summary>
    /// Модель для редактирования поста
    /// </summary>
    public class PostWithImageUpdateViewModel
    {
        /// <summary>
        /// Id поста
        /// </summary>
        [Required(ErrorMessage = "Параметр Title обязателен")]
        public int Id { get; set; }

        /// <summary>
        /// Изображение поста
        /// </summary>
        public IFormFile? Image { get; set; } = null;

        /// <summary>
        /// Заголовок
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Параметр Title обязателен")]
        [Range(1, 100, ErrorMessage = "Длина заголовка от {1} до {2} символов")]
        public string Title { get; set; }

        /// <summary>
        /// Id автора
        /// </summary>
        [Required(ErrorMessage = "Параметр UserId обязателен")]
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
        [Range(1, 100, ErrorMessage = "Длина тегов от {1} до {2} символов")]
        public string Tags { get; set; }
    }
}
