using Goods.System.Social.Network.DomainModel.Entities;
using Microsoft.AspNetCore.Http;

namespace DomainModel.Entities.ViewModels
{
    public class ReactionEntityViewModel
    {
        public int EntityId { get; set; }
        public int TypeReactionId { get; set; }
        public int UserId { get; set;}
    }
}
