namespace Goods.System.Social.Network.Microservice.Users.Entities.DTO
{
    /// <summary>
    /// Модель пользователя DTO
    /// </summary>
    public class UserDTO
    {
        /// <summary>
        /// Id юзера
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTime DateBirthday { get; set; }

        /// <summary>
        /// Город
        /// </summary>
        public string City { get; set; } = string.Empty;

        /// <summary>
        /// Страна
        /// </summary>
        public string Country { get; set; } = string.Empty;

        /// <summary>
        /// Статус
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// Электронная почта
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Подписан ли твой юзера на этого
        /// </summary>
        public bool IsSubscriber { get; set; }

        /// <summary>
        /// Путь до аватара
        /// </summary>
        public string Avatar { get; set; } = "0.jpeg";

        /// <summary>
        /// Путь до фона
        /// </summary>
        public string Background { get; set; } = "0.jpeg";
    }
}