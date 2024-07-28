using Goods.System.Social.Network.DomainModel.Entities;
using Microsoft.AspNetCore.Http;

namespace DomainModel.Entities.ViewModels
{
    public class GalleryUpdateViewModel
    {
        public IFormFile Photo { get; set; }
        public int UserId { get; set; }
        public int PhotoId { get; set;}
    }
}
