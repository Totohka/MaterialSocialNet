using Goods.System.Social.Network.DomainModel.Entities;
using System.ComponentModel.DataAnnotations;

namespace Goods.System.Social.Network.Microservice.Auth.Entities.ViewModel
{
    /// <summary>
    /// Сущность регистрируемого юзера
    /// </summary>
    public class UserRegistrationViewModel
    {
        /// <summary>
        /// Имя
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Имя обязательно")]
        [Range(1, 25, ErrorMessage = "Длина имени от 1 до 25 символов")]
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Фамилия обязательна")]
        [Range(1, 25, ErrorMessage = "Длина фамилии от 1 до 25 символов")]
        public string LastName { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Пароль обязателен")]
        [Range(1, 100, ErrorMessage = "Длина пароля от 1 до 100 символов")]
        public string Password { get; set; }

        /// <summary>
        /// Дата рождения
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Дата рождения обязательна")]
        public DateTime DateBirthday { get; set; }

        /// <summary>
        /// Электронная почта
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Электронная почта обязательна")]
        [Range(1, 182, ErrorMessage = "Длина email от 1 до 182 символов")]
        public string Email { get; set; }
    }
}
