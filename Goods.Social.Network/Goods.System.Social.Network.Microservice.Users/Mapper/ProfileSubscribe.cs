using AutoMapper;
using DomainModel.Entities.ViewModels;
using Goods.System.Social.Network.DomainModel.Entities;
using Goods.System.Social.Network.Microservice.Users.Entities.DTO;

namespace Goods.System.Social.Network.Microservice.Users.Mapper
{
    public class ProfileSubscribe : Profile
    {
        public ProfileSubscribe() {
            CreateMap<UserSubscribeViewModel, UserFriend>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.user_id))
                .ForMember(dest => dest.UserFriendId, opt => opt.MapFrom(src => src.user_friend_id));
        }
    }
}
