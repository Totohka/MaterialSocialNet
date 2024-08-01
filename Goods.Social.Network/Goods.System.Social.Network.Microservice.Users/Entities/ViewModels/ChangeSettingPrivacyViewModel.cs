namespace Goods.System.Social.Network.Microservice.Users.Entities.ViewModels
{
    /// <summary>
    /// Модель для смены настроек приватности
    /// </summary>
    public class ChangeSettingPrivacyViewModel
    {
        /// <summary>
        /// Id пользователя
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Кому показывать свои посты
        /// </summary>
        public int ShowPost { get; set; }

        /// <summary>
        /// Позволять добавлять себя в чат
        /// </summary>
        public int InvateChats { get; set; }

        /// <summary>
        /// Показывать дату рождения
        /// </summary>
        public int ShowDateBirthday { get; set; }
    }
}
