namespace Goods.System.Social.Network.Microservice.Comments.Entities.DTO
{
    /// <summary>
    /// Комментарий к посту
    /// </summary>
    public class CommentPostDTO
    {
        /// <summary>
        /// Id комментария
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Id поста
        /// </summary>
        public int PostId { get; set; }

        /// <summary>
        /// Id юзера, написавшего коммент
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Имя юзера
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия юзера
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Текст коммента
        /// </summary>
        public string Text { get; set; }
    }
}
