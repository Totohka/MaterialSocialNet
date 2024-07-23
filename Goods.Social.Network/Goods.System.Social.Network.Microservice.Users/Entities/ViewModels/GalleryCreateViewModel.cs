using Goods.System.Social.Network.DomainModel.Entities;
using Microsoft.AspNetCore.Http;

namespace DomainModel.Entities.ViewModels
{
    public class GalleryCreateViewModel
    {
        public IFormFile photo { get; set; }
        public int user_id { get; set; }
    }
}
