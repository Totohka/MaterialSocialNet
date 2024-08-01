namespace Goods.System.Social.Network.Microservice.Posts.Entities.DTO
{
    /// <summary>
    /// Пост DTO
    /// </summary>
    public class PostDTO
    {
        /// <summary>
        /// Id поста
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Путь до фотографии
        /// </summary>
        public string Image { get; set; } = string.Empty;
        /// <summary>
        /// Заголовок
        /// </summary>
        public string Title { get; set; } = string.Empty;
        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime DateCreate { get; set; }
        /// <summary>
        /// Текст коммента
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// Имя автора
        /// </summary>
        public string FirstNameUser { get; set; } = string.Empty;
        /// <summary>
        /// Фамилия автора
        /// </summary>
        public string LastNameUser { get; set; } = string.Empty;
        /// <summary>
        /// Текст поста
        /// </summary>
        public string Text { get; set; } = string.Empty;
        /// <summary>
        /// Теги
        /// </summary>
        public string Tags { get; set; } = string.Empty;
        /// <summary>
        /// Рейтинг поста
        /// </summary>
        public int Rating { get; set; }
        /// <summary>
        /// Тип реакции, которую поставил твой юзер
        /// </summary>
        public string TypeReaction { get; set; } = string.Empty;
    }
}