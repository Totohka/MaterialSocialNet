using Goods.System.Social.Network.DomainModel.Entities;
using Microsoft.AspNetCore.Http;

namespace DomainModel.Entities.ViewModels
{
    public class ReactionEntityViewModel
    {
        public int entity_id { get; set; }
        public int type_reaction_id { get; set; }
        public int user_id { get; set;}
    }
}
