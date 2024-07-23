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
                .ForMember(dest => dest.DateCreate, opt => opt.MapFrom(src => src.date_create))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.text))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.user_id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.title))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.tags))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => ""));

            CreateMap<PostWithImageUpdate, Post>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.id))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.text))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.user_id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.title))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.tags));
        }
    }
}
