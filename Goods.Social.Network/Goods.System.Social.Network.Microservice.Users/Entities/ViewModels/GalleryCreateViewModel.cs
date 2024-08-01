namespace DomainModel.Entities.ViewModels
{
    /// <summary>
    /// Модель для загрузки фотографии в галерею
    /// </summary>
    public class GalleryCreateViewModel
    {
        /// <summary>
        /// Фотография
        /// </summary>
        public IFormFile Photo { get; set; }

        /// <summary>
        /// Id юзера
        /// </summary>
        public int UserId { get; set; }
    }
}
