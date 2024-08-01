using Goods.System.Social.Network.DomainModel.Entities;
using Microsoft.AspNetCore.Http;

namespace DomainModel.Entities.ViewModels
{
    /// <summary>
    /// Модель для фона
    /// </summary>
    public class BackgroundViewModel
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
