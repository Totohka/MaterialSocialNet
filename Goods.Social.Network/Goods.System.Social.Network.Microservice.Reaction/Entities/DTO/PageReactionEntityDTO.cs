using Goods.System.Social.Network.DomainModel.Entities;

namespace Goods.System.Social.Network.Microservice.Reaction.Entities.DTO
{
    public class PageReactionEntityDTO<T> where T : class
    {
        public PageReactionEntityDTO(int countAllReactionEntities, int pageCount, int numberPage, List<T> reactionEntitiesDTO)
        {
            CountAllReactionEntities = countAllReactionEntities;
            ReactionEntitiesDTO = reactionEntitiesDTO;
            PageCount = pageCount;
            NumberPage = numberPage;
        }
        public int CountAllReactionEntities { get; private set; }
        public int PageCount { get; private set; }
        public int NumberPage { get; private set; }
        public List<T> ReactionEntitiesDTO { get; private set; }
    }
}
