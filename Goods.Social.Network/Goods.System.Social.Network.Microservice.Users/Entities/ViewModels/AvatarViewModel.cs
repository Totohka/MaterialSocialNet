using Goods.System.Social.Network.DomainModel.Entities;
using Microsoft.AspNetCore.Http;

namespace DomainModel.Entities.ViewModels
{
    public class AvatarViewModel
    {
        public IFormFile Photo { get; set; }
        public int UserId { get; set; }
    }
}
