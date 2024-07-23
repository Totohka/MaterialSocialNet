using Goods.System.Social.Network.DomainModel.Entities;
using Microsoft.AspNetCore.Http;

namespace DomainModel.Entities.ViewModels
{
    public class GalleryUpdateViewModel
    {
        public IFormFile photo { get; set; }
        public int user_id { get; set; }
        public int photo_id { get; set;}
    }
}
