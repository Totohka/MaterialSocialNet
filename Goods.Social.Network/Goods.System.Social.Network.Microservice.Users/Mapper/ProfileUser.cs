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
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.id))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.first_name))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.last_name))
                .ForMember(dest => dest.DateBirthday, opt => opt.MapFrom(src => src.date_birthday))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.city))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.country))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.status))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.email))
                .ForMember(dest => dest.Avatar, opt => opt.MapFrom(src => src.avatar))
                .ForMember(dest => dest.Background, opt => opt.MapFrom(src => src.background));
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
