using AutoMapper;
using Goods.System.Social.Network.DomainModel.Entities;
using Goods.System.Social.Network.Microservice.Chats.Entities.DTO;
using Goods.System.Social.Network.Microservice.Chats.Entities.ViewModels;

namespace Goods.System.Social.Network.Microservice.Chats.Infrastructure.Mapper
{
    public class ProfileChat : Profile
    {
        public ProfileChat()
        {
            CreateMap<User, UserDTO>();
            CreateMap<Message, UserMessageDTO>();
            CreateMap<MessageViewModel, Message>()
                .ForMember(dest => dest.ChatRoomId, opt => opt.MapFrom(src => src.chat_room_id))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.user_id))
                .ForMember(dest => dest.Msg, opt => opt.MapFrom(src => src.message))
                .ForMember(dest => dest.DateSend, opt => opt.MapFrom(src => src.date_send));
        }
    }
}
