using Goods.System.Social.Network.DomainModel.Entities;
using Goods.System.Social.Network.Microservice.Posts.Entities.DTO;
using Microsoft.AspNetCore.Http;

namespace Goods.System.Social.Network.Microservice.Posts.Entities.Pagination
{
    public class PagePostDTO
    {
        public PagePostDTO(int countAllPosts, int pageCount, int numberPage, List<PostDTO> posts) {
            CountAllPosts = countAllPosts;
            Posts = posts;
            PageCount = pageCount;
            NumberPage = numberPage;
        }
        public int CountAllPosts { get; private set; }
        public int PageCount { get; private set; }
        public int NumberPage { get; private set; }
        public List<PostDTO> Posts { get; private set; }
    }
}
