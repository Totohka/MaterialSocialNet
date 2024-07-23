using Goods.System.Social.Network.DomainModel.Entities;

namespace DomainModel.Entities.ViewModels
{
    public class PageChatViewModel
    {
        public PageChatViewModel(int countAllChats, int pageCount, int numberPage, List<ChatRoom> chatRooms) { 
            CountAllChats = countAllChats;
            ChatRooms = chatRooms;
            PageCount = pageCount;
            NumberPage = numberPage;
        }
        public int CountAllChats { get; private set; }
        public int PageCount { get; private set; }
        public int NumberPage { get; private set; }
        public List<ChatRoom> ChatRooms { get; private set; }
    }
}
