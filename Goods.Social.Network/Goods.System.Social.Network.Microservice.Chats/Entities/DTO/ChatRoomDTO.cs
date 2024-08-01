namespace Goods.System.Social.Network.Microservice.Chats.Entities.DTO
{
    /// <summary>
    /// Модель для редактирования чата
    /// </summary>
    public class ChatRoomDTO
    {
        /// <summary>
        /// Id чата
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название чата
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание чата
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Последнее сообщение чата
        /// </summary>
        public string LastMessage { get; set; }
    }
}
