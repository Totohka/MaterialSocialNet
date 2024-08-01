namespace DomainModel.Entities.ViewModels
{
    /// <summary>
    /// Модель для подписки
    /// </summary>
    public class UserSubscribeViewModel
    {
        /// <summary>
        /// Твой юзер Id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Id юзера на которого хочешь подписаться
        /// </summary>
        public int UserFriendId { get; set; }
    }
}
