using AutoMapper;
using Goods.System.Social.Network.DomainModel.Entities;
using Goods.System.Social.Network.Microservice.Auth.Entities.ViewModel;

namespace Goods.System.Social.Network.Microservice.Auth.Infrastructure.Mapper
{
    public class ProfileUser : Profile
    {
        public ProfileUser()
        {
            CreateMap<UserRegistrationViewModel, User>();
        }
    }
}
