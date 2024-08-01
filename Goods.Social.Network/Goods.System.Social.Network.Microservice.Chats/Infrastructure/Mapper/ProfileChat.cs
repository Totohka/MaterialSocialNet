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
            CreateMap<ChatRoomUserViewModel, ChatRoomUser>();
            CreateMap<ChatRoom, ChatRoomDTO>();
            CreateMap<ChatRoomUpdateViewModel, ChatRoom>();
            CreateMap<ChatRoomViewModel, ChatRoom>();
            CreateMap<User, UserDTO>();
            CreateMap<Message, UserMessageDTO>();
            CreateMap<MessageViewModel, Message>()
                .ForMember(dest => dest.ChatRoomId, opt => opt.MapFrom(src => src.ChatRoomId))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Msg, opt => opt.MapFrom(src => src.Message))
                .ForMember(dest => dest.DateSend, opt => opt.MapFrom(src => src.DateSend));
            CreateMap<MessageUpdateViewModel, Message>();
        }
    }
}
