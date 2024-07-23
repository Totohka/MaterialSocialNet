using Goods.System.Social.Network.DomainModel.Entities;
using Microsoft.AspNetCore.Http;

namespace DomainModel.Entities.ViewModels
{
    public class PagePost
    {
        public PagePost(int countAllPosts, int pageCount, int numberPage, List<Post> posts) {
            CountAllPosts = countAllPosts;
            Posts = posts;
            PageCount = pageCount;
            NumberPage = numberPage;
        }
        public int CountAllPosts { get; private set; }
        public int PageCount { get; private set; }
        public int NumberPage { get; private set; }
        public List<Post> Posts { get; private set; }
    }
}
