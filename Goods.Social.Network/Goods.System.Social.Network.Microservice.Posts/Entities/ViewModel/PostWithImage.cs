using Goods.System.Social.Network.DomainModel.Entities;
using Microsoft.AspNetCore.Http;

namespace Goods.System.Social.Network.Microservice.Posts.Entities.ViewModel
{
    public class PostWithImage
    {
        public IFormFile image { get; set; }
        public string title { get; set; }
        public DateTime date_create { get; set; }
        public int user_id { get; set; }
        public string text { get; set; }
        public string tags { get; set; }
    }
}
