using Goods.System.Social.Network.DomainModel.Entities;
using Microsoft.AspNetCore.Http;

namespace DomainModel.Entities.ViewModels
{
    /// <summary>
    /// Модель для аватарки
    /// </summary>
    public class AvatarViewModel
    {
        /// <summary>
        /// Фотография в jpeg
        /// </summary>
        public IFormFile Photo { get; set; }

        /// <summary>
        /// Id пользователя
        /// </summary>
        public int UserId { get; set; }
    }
}
