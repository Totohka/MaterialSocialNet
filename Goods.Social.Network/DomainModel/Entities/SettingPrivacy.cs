namespace Goods.System.Social.Network.DomainModel.Entities
{
    public class SettingPrivacy
    {
        public int Id { get; set; }
        public int ShowPost { get; set; } = 1;
        public int InvateChats { get; set; } = 1;
        public int ShowDateBirthday { get; set; } = 1;
        public List<User> User { get; set; } 
    }
}
