using Goods.System.Social.Network.DomainModel.Entities;

namespace Goods.System.Social.Network.Microservice.Chats.Entities.DTO
{
    /// <summary>
    /// Модель для пагинации чатов
    /// </summary>
    public class PageChatDTO
    {
        /// <summary>
        /// Количество всех чатов
        /// </summary>
        public int CountAllChats { get; set; }

        /// <summary>
        /// Количество всех страниц
        /// </summary>
        public int PageCount { get; set; }

        /// <summary>
        /// Номер текущей страницы
        /// </summary>
        public int NumberPage { get; set; }

        /// <summary>
        /// Список DTO чатов
        /// </summary>
        public List<ChatRoomDTO> ChatRoomDTOs { get; set; }
    }
}
