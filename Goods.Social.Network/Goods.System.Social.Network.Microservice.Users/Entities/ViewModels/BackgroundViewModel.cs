using Goods.System.Social.Network.DomainModel.Entities;
using Microsoft.AspNetCore.Http;

namespace DomainModel.Entities.ViewModels
{
    public class BackgroundViewModel
    {
        public IFormFile Photo { get; set; }
        public int UserId { get; set; }
    }
}
