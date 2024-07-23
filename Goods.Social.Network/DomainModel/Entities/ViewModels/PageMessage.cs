using Goods.System.Social.Network.DomainModel.Entities;

namespace DomainModel.Entities.ViewModels
{
    public class PageMessage
    {
        public PageMessage(int countAllMessages, int pageCount, int numberPage, List<Message> messages) { 
            CountAllMessages = countAllMessages;
            Messages = messages;
            PageCount = pageCount;
            NumberPage = numberPage;
        }
        public int CountAllMessages { get; private set; }
        public int PageCount { get; private set; }
        public int NumberPage { get; private set; }
        public List<Message> Messages { get; private set; }
    }
}
