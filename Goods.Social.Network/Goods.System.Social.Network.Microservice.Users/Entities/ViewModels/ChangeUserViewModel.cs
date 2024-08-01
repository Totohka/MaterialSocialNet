namespace DomainModel.Entities.ViewModels
{
    /// <summary>
    /// Модель для смены данных о юзере
    /// </summary>
    public class ChangeUserViewModel
    {
        /// <summary>
        /// Id юзера
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Электронная почта
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Город
        /// </summary>
        public string City { get; set; } = string.Empty;

        /// <summary>
        /// Страна
        /// </summary>
        public string Country { get; set; } = string.Empty;

        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTime DateBirthday { get; set; } = DateTime.MinValue;

        /// <summary>
        /// Статус
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// Телефон
        /// </summary>
        public string Phone { get; set; } = string.Empty;

        /// <summary>
        /// Ава (я щас понял что я не понимаю что это)
        /// </summary>
        public string Avatar { get; set;} = string.Empty;

        /// <summary>
        /// Фон (я щас понял что я не понимаю что это)
        /// </summary>
        public string Background { get; set;} = string.Empty;
    }
}
