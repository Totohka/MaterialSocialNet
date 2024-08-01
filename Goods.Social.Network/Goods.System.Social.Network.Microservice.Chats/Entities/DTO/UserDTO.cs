namespace Goods.System.Social.Network.Microservice.Chats.Entities.DTO
{
    /// <summary>
    /// Модель юзера DTO
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
        /// Путь до аватара
        /// </summary>
        public string Avatar { get; set; } = "0.jpeg";
    }
}