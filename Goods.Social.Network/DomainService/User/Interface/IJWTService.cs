using Goods.System.Social.Network.DomainModel.Entities;

namespace Goods.System.Social.Network.DomainServices.Interface
{
    public interface IJWTService
    {
        public Task<string> RegistrationAsync(User user);
        public Task<string> AuthAsync(string email, string password);
        public Task<string> UpdateTokenAsync(int userId);
    }
}
