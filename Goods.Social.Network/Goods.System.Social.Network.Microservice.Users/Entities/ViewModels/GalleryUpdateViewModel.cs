using Goods.System.Social.Network.DomainModel.Entities;
using Microsoft.AspNetCore.Http;

namespace DomainModel.Entities.ViewModels
{
    /// <summary>
    /// Модель для редактирования фотографии в галерею
    /// </summary>
    public class GalleryUpdateViewModel
    {
        /// <summary>
        /// Фотография
        /// </summary>
        public IFormFile Photo { get; set; }

        /// <summary>
        /// Id юзера
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Id фотографии
        /// </summary>
        public int PhotoId { get; set;}
    }
}
