using Goods.System.Social.Network.DomainModel.Entities;
using Microsoft.AspNetCore.Http;

namespace DomainModel.Entities.ViewModels
{
    public class PageUser
    {
        public PageUser(int countAllUsers, int pageCount, int numberPage, List<User> users) { 
            CountAllUsers = countAllUsers;
            Users = users;
            PageCount = pageCount;
            NumberPage = numberPage;
        }
        public int CountAllUsers { get; private set; }
        public int PageCount { get; private set; }
        public int NumberPage { get; private set; }
        public List<User> Users { get; private set; }
    }
}
