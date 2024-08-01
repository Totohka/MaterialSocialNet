using Goods.System.Social.Network.DomainModel.Entities;
using Goods.System.Social.Network.Microservice.Posts.Entities.DTO;
using Microsoft.AspNetCore.Http;

namespace Goods.System.Social.Network.Microservice.Posts.Entities.Pagination
{
    /// <summary>
    /// Страница постов
    /// </summary>
    public class PagePostDTO
    {
        public PagePostDTO(int countAllPosts, int pageCount, int numberPage, List<PostDTO> posts) {
            CountAllPosts = countAllPosts;
            Posts = posts;
            PageCount = pageCount;
            NumberPage = numberPage;
        }

        /// <summary>
        /// Количество постов
        /// </summary>
        public int CountAllPosts { get; private set; }

        /// <summary>
        /// Количество страниц
        /// </summary>
        public int PageCount { get; private set; }

        /// <summary>
        /// Номер страницы
        /// </summary>
        public int NumberPage { get; private set; }

        /// <summary>
        /// Список постов DTO
        /// </summary>
        public List<PostDTO> Posts { get; private set; }
    }
}
