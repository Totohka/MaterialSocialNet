using AutoMapper;
using DomainModel.Entities.ViewModels;
using Goods.System.Social.Network.DomainModel.Entities;
using Goods.System.Social.Network.Microservice.Reaction.Entities.DTO;

namespace Goods.System.Social.Network.Microservice.Reaction.Infrastructure.Mapper
{
    public class ProfileReaction : Profile
    {
        public ProfileReaction() {
            CreateMap<ReactionPost, ReactionEntityDTO>()
                .ForMember(dest => dest.ReactionEntityId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.EntityId, opt => opt.MapFrom(src => src.PostId))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Reaction.UserId))
                .ForMember(dest => dest.FirstNameUser, opt => opt.MapFrom(src => src.Reaction.User.FirstName))
                .ForMember(dest => dest.LastNameUser, opt => opt.MapFrom(src => src.Reaction.User.LastName))
                .ForMember(dest => dest.TypeReactionId, opt => opt.MapFrom(src => src.Reaction.TypeReactionId))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Reaction.TypeReaction.Name));


            CreateMap<ReactionMessage, ReactionEntityDTO>()
                .ForMember(dest => dest.ReactionEntityId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.EntityId, opt => opt.MapFrom(src => src.MessageId))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Reaction.UserId))
                .ForMember(dest => dest.FirstNameUser, opt => opt.MapFrom(src => src.Reaction.User.FirstName))
                .ForMember(dest => dest.LastNameUser, opt => opt.MapFrom(src => src.Reaction.User.LastName))
                .ForMember(dest => dest.TypeReactionId, opt => opt.MapFrom(src => src.Reaction.TypeReactionId))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Reaction.TypeReaction.Name));           
        }
    }
}
