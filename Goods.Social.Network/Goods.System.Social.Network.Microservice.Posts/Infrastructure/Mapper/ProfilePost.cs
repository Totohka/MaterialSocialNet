using AutoMapper;
using DomainModel.Entities.ViewModels;
using Goods.System.Social.Network.DomainModel.Entities;
using Goods.System.Social.Network.Microservice.Posts.Entities.DTO;
using Goods.System.Social.Network.Microservice.Posts.Entities.ViewModel;

namespace Goods.System.Social.Network.Microservice.Posts.Infrastructure.Mapper
{
    public class ProfilePost : Profile
    {
        public ProfilePost()
        {
            CreateMap<Post, PostDTO>();
            CreateMap<PostWithImage, Post>()
                .ForMember(dest => dest.DateCreate, opt => opt.MapFrom(src => src.DateCreate))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Text))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => ""));

            CreateMap<PostWithImageUpdate, Post>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Text))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags));
        }
    }
}
