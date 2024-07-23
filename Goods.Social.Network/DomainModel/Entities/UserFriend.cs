namespace Goods.System.Social.Network.DomainModel.Entities
{
    public class UserFriend
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int UserFriendId { get; set; }
    }
}