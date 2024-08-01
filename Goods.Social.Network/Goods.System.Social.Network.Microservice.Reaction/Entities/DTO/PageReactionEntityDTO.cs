using Goods.System.Social.Network.DomainModel.Entities;

namespace Goods.System.Social.Network.Microservice.Reaction.Entities.DTO
{
    /// <summary>
    /// Страница реакций DTO
    /// </summary>
    public class PageReactionEntityDTO<T> where T : class
    {
        public PageReactionEntityDTO(int countAllReactionEntities, int pageCount, int numberPage, List<T> reactionEntitiesDTO)
        {
            CountAllReactionEntities = countAllReactionEntities;
            ReactionEntitiesDTO = reactionEntitiesDTO;
            PageCount = pageCount;
            NumberPage = numberPage;
        }
        /// <summary>
        /// Количество реакций в сущности
        /// </summary>
        public int CountAllReactionEntities { get; private set; }

        /// <summary>
        /// Количество страниц
        /// </summary>
        public int PageCount { get; private set; }

        /// <summary>
        /// Номер страницы
        /// </summary>
        public int NumberPage { get; private set; }

        /// <summary>
        /// Список реакций DTO
        /// </summary>
        public List<T> ReactionEntitiesDTO { get; private set; }
    }
}
