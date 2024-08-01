using Goods.System.Social.Network.Microservice.Users.Entities.DTO;

namespace Goods.System.Social.Network.Microservice.Users.Entities.Pagination
{
    public class PageUserDTO
    {
        public PageUserDTO(int countAllUsers, int pageCount, int numberPage, List<UserDTO> users) { 
            CountAllUsers = countAllUsers;
            Users = users;
            PageCount = pageCount;
            NumberPage = numberPage;
        }

        /// <summary>
        /// Количество всех юзеров
        /// </summary>
        public int CountAllUsers { get; private set; }

        /// <summary>
        /// Количество страниц
        /// </summary>
        public int PageCount { get; private set; }

        /// <summary>
        /// Номер страницы
        /// </summary>
        public int NumberPage { get; private set; }

        /// <summary>
        /// Список юзеров
        /// </summary>
        public List<UserDTO> Users { get; private set; }
    }
}
