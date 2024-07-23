using AutoMapper;
using Goods.System.Social.Network.DomainModel.Entities;
using Goods.System.Social.Network.Microservice.Comments.Entities.DTO;

namespace Goods.System.Social.Network.Microservice.Comments.Infrastructure.Mapper
{
    public class ProfileCommentPost : Profile
    {
        public ProfileCommentPost()
        {
            CreateMap<CommentPost, CommentPostDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Text))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Comment.UserId))
                .ForMember(dest => dest.PostId, opt => opt.MapFrom(src => src.PostId))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Comment.User.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Comment.User.LastName));
        }
    }
}
