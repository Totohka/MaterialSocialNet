namespace Goods.System.Social.Network.Microservice.Users.Entities.ViewModels
{
    /// <summary>
    /// Модель для смены настроек уведомлений
    /// </summary>
    public class ChangeSettingNotificationViewModel
    {
        /// <summary>
        /// Id пользователя
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Уведомления о новых сообщениях
        /// </summary>
        public bool NewMessage { get; set; }

        /// <summary>
        /// Уведомления о новых подписках
        /// </summary>
        public bool NewSubscribe { get; set; }

        /// <summary>
        /// Уведомления о новых постах
        /// </summary>
        public bool NewPosts { get; set; }
    }
}
