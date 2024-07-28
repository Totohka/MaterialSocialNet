using AutoMapper;
using DomainModel.Entities.ViewModels;
using Goods.System.Social.Network.DomainModel.Entities;
using Goods.System.Social.Network.Microservice.Users.Entities.DTO;

namespace Goods.System.Social.Network.Microservice.Users.Mapper
{
    public class ProfileUser : Profile
    {
        public ProfileUser() {
            CreateMap<User, UserDTO>();
            CreateMap<ChangeUserViewModel, User>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.DateBirthday, opt => opt.MapFrom(src => src.DateBirthday))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Avatar, opt => opt.MapFrom(src => src.Avatar))
                .ForMember(dest => dest.Background, opt => opt.MapFrom(src => src.Background));
            CreateMap<User, User>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName != string.Empty))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName != string.Empty))
                .ForMember(dest => dest.DateBirthday, opt => opt.MapFrom(src => src.DateBirthday != DateTime.MinValue))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City != string.Empty))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country != string.Empty))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status != string.Empty))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email != string.Empty))
                .ForMember(dest => dest.Avatar, opt => opt.MapFrom(src => src.Avatar != string.Empty))
                .ForMember(dest => dest.Background, opt => opt.MapFrom(src => src.Background != string.Empty));
        }
    }
}
