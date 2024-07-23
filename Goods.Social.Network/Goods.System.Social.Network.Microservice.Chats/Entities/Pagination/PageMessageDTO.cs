
using Goods.System.Social.Network.Microservice.Chats.Entities.DTO;

namespace Goods.System.Social.Network.Microservice.Chats.Entities.Pagination
{
    public class PageMessageDTO
    {
        public PageMessageDTO(int countAllMessages, int pageCount, int numberPage, List<UserMessageDTO> messages) { 
            CountAllMessages = countAllMessages;
            Messages = messages;
            PageCount = pageCount;
            NumberPage = numberPage;
        }
        public int CountAllMessages { get; private set; }
        public int PageCount { get; private set; }
        public int NumberPage { get; private set; }
        public List<UserMessageDTO> Messages { get; private set; }
    }
}
