using Goods.System.Social.Network.DomainModel.Entities;

namespace DomainModel.Entities.ViewModels
{
    public class PageReactionEntity<T> where T : class
    {
        public PageReactionEntity(int countAllReactionEntities, int pageCount, int numberPage, List<T> reactionEntities) {
            CountAllReactionEntities = countAllReactionEntities;
            ReactionEntities = reactionEntities;
            PageCount = pageCount;
            NumberPage = numberPage;
        }
        public int CountAllReactionEntities { get; private set; }
        public int PageCount { get; private set; }
        public int NumberPage { get; private set; }
        public List<T> ReactionEntities { get; private set; }
    }
}
