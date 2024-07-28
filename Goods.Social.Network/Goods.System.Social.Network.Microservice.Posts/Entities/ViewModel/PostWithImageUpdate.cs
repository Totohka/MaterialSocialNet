using Goods.System.Social.Network.DomainModel.Entities;
using Microsoft.AspNetCore.Http;

namespace Goods.System.Social.Network.Microservice.Posts.Entities.ViewModel
{ 
    public class PostWithImageUpdate
    {
        public int Id { get; set; }
        public IFormFile? Image { get; set; } = null;
        public string Title { get; set; }
        public int UserId { get; set; }
        public string Text { get; set; }
        public string Tags { get; set; }
    }
}
