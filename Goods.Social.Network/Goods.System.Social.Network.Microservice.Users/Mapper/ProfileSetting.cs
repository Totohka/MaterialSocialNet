using AutoMapper;
using DomainModel.Entities.ViewModels;
using Goods.System.Social.Network.DomainModel.Entities;
using Goods.System.Social.Network.Microservice.Users.Entities.DTO;
using Goods.System.Social.Network.Microservice.Users.Entities.ViewModels;

namespace Goods.System.Social.Network.Microservice.Users.Mapper
{
    public class ProfileSetting : Profile
    {
        public ProfileSetting() {
            CreateMap<ChangeSettingPrivacyViewModel, SettingPrivacy>()
                .ForMember(dest => dest.ShowPost, opt => opt.MapFrom(src => src.ShowPost))
                .ForMember(dest => dest.ShowDateBirthday, opt => opt.MapFrom(src => src.ShowDateBirthday))
                .ForMember(dest => dest.InvateChats, opt => opt.MapFrom(src => src.InvateChats));
            CreateMap<ChangeSettingNotificationViewModel, SettingNotification>()
                .ForMember(dest => dest.NewSubscibe, opt => opt.MapFrom(src => src.NewSubscribe))
                .ForMember(dest => dest.NewPosts, opt => opt.MapFrom(src => src.NewPosts))
                .ForMember(dest => dest.NewMessage, opt => opt.MapFrom(src => src.NewMessage));
        }
    }
}
