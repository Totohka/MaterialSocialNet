using Goods.System.Social.Network.Microservice.Chats.Entities.DTO;

namespace Goods.System.Social.Network.Microservice.Chats.Entities.Pagination
{
    public class PageUserDTO
    {
        public PageUserDTO(int countAllUsers, int pageCount, int numberPage, List<UserDTO> users) { 
            CountAllUsers = countAllUsers;
            Users = users;
            PageCount = pageCount;
            NumberPage = numberPage;
        }
        public int CountAllUsers { get; private set; }
        public int PageCount { get; private set; }
        public int NumberPage { get; private set; }
        public List<UserDTO> Users { get; private set; }
    }
}
