using Goods.System.Social.Network.DomainModel.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Drawing;

namespace Goods.System.Social.Network.DAL
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> dbContextOptions) : base(dbContextOptions) { }
        public DbSet<SettingPrivacy> SettingPrivacies { get; set; }
        public DbSet<SettingNotification> SettingNotifications { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<UserFriend> UserFriends { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<ReactionMessage> ReactionMessages { get; set; }
        public DbSet<ReactionType> TypeReaction { get; set; }
        public DbSet<Reaction> Reactions { get; set; }
        public DbSet<ChatRoom> ChatRooms { get; set; }
        public DbSet<ReactionPost> ReactionPosts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<CommentPost> CommentPosts { get; set; }
        public DbSet<ChatRoomUser> ChatRoomUsers { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<NotificationChatRoom> NotificationChatRooms { get; set; }
        public DbSet<NotificationPost> NotificationPosts { get; set; }

        protected override void ConfigureConventions(ModelConfigurationBuilder builder)
        {
            builder.Properties<DateOnly>().HaveConversion<DateTime>();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SettingPrivacy>(SettingPrivacyConfigure);
            modelBuilder.Entity<SettingNotification>(SettingNotificationConfigure);
            modelBuilder.Entity<Post>(PostConfigure);
            modelBuilder.Entity<UserFriend>(UserFriendConfigure);
            modelBuilder.Entity<User>(UserConfigure);
            modelBuilder.Entity<ChatRoom>(ChatRoomConfigure);
            modelBuilder.Entity<ReactionMessage>(ReactionMessageConfigure);
            modelBuilder.Entity<Reaction>(ReactionConfigure);
            modelBuilder.Entity<Message>(MessageConfigure);
            modelBuilder.Entity<ChatRoomUser>(ChatRoomUserConfigure);
            modelBuilder.Entity<ReactionPost>(ReactionPostConfigure);
            modelBuilder.Entity<ReactionType>(TypeReactionConfigure);
            modelBuilder.Entity<Comment>(CommentConfigure);
            modelBuilder.Entity<CommentPost>(CommentPostConfigure);
            modelBuilder.Entity<Notification>(NotificationConfigure);
            modelBuilder.Entity<NotificationChatRoom>(NotificationChatRoomConfigure);
            modelBuilder.Entity<NotificationPost>(NotificationPostConfigure);
        }

        public void NotificationChatRoomConfigure(EntityTypeBuilder<NotificationChatRoom> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.NotificationId).IsRequired();
            builder.Property(p => p.ChatRoomId).IsRequired();
            builder.Property(p => p.IsCheck).IsRequired();
        }
        public void NotificationPostConfigure(EntityTypeBuilder<NotificationPost> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.NotificationId).IsRequired();
            builder.Property(p => p.UserId);
            builder.Property(p => p.PostId);
            builder.Property(p => p.IsCheck).IsRequired();
            builder.HasOne(r => r.User).WithMany(u => u.NotificationPosts).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(r => r.Post).WithMany(u => u.NotificationPosts).OnDelete(DeleteBehavior.Restrict);
        }
        public void NotificationConfigure(EntityTypeBuilder<Notification> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.UserId).IsRequired();
            builder.Property(p => p.NotificationConnectionId);
        }
        public void ReactionMessageConfigure(EntityTypeBuilder<ReactionMessage> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.MessageId).IsRequired();
            builder.Property(p => p.ReactionId).IsRequired();
            builder.HasOne(r => r.Reaction).WithMany(u => u.ReactionMessages).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(r => r.Message).WithMany(u => u.ReactionMessages).OnDelete(DeleteBehavior.Cascade);
        }
        public void TypeReactionConfigure(EntityTypeBuilder<ReactionType> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(20);
            builder.Property(p => p.Description).IsRequired().HasMaxLength(100);
        }
        public void ReactionConfigure(EntityTypeBuilder<Reaction> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.TypeReactionId).IsRequired();
            builder.Property(p => p.UserId).IsRequired();
        }
        public void ReactionPostConfigure(EntityTypeBuilder<ReactionPost> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.PostId).IsRequired();
            builder.Property(p => p.ReactionId).IsRequired();
            builder.HasOne(r => r.Reaction).WithMany(u => u.ReactionPosts).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(r => r.Post).WithMany(u => u.ReactionPosts).OnDelete(DeleteBehavior.Cascade);
        }
        public void CommentConfigure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.UserId).IsRequired();
        }
        public void CommentPostConfigure(EntityTypeBuilder<CommentPost> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.PostId).IsRequired();
            builder.Property(p => p.CommentId).IsRequired();
            builder.Property(p => p.Text).IsRequired().HasMaxLength(300);
            builder.HasOne(c => c.Post).WithMany(u => u.CommentPosts).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(c => c.Comment).WithMany(u => u.CommentPosts).OnDelete(DeleteBehavior.Restrict);
        }
        public void SettingPrivacyConfigure(EntityTypeBuilder<SettingPrivacy> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.ShowPost).IsRequired();
            builder.Property(p => p.InvateChats).IsRequired();
            builder.Property(p => p.ShowDateBirthday).IsRequired();
        }
        public void SettingNotificationConfigure(EntityTypeBuilder<SettingNotification> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.NewPosts).IsRequired();
            builder.Property(p => p.NewSubscibe).IsRequired();
            builder.Property(p => p.NewMessage).IsRequired();
        }
        public void PostConfigure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Image).IsRequired().HasMaxLength(30);
            builder.Property(p => p.Title).IsRequired().HasMaxLength(100);
            builder.Property(p => p.DateCreate).IsRequired();
            builder.Property(p => p.UserId).IsRequired();
            builder.Property(p => p.Text).IsRequired().HasMaxLength(10000);
            builder.Property(p => p.Tags).IsRequired().HasMaxLength(100);
        }
        public void UserFriendConfigure(EntityTypeBuilder<UserFriend> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.UserId).IsRequired();
            builder.Property(p => p.UserFriendId).IsRequired();
        }
        public void UserConfigure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.FirstName).IsRequired().HasMaxLength(25);
            builder.Property(p => p.LastName).IsRequired().HasMaxLength(25);
            builder.Property(p => p.Password).IsRequired().HasMaxLength(100);
            builder.Property(p => p.DateBirthday).IsRequired();
            builder.Property(p => p.Country).IsRequired().HasMaxLength(25);
            builder.Property(p => p.City).IsRequired().HasMaxLength(25);
            builder.Property(p => p.Phone).IsRequired().HasMaxLength(11);
            builder.Property(p => p.Status).IsRequired().HasMaxLength(100);
            builder.Property(p => p.SettingNotificationId).IsRequired();
            builder.Property(p => p.SettingPrivacyId).IsRequired();
            builder.Property(p => p.Email).IsRequired().HasMaxLength(182);
            builder.Property(p => p.Avatar).IsRequired().HasMaxLength(20);
            builder.Property(p => p.Background).IsRequired().HasMaxLength(20);
        }
        public void ChatRoomConfigure(EntityTypeBuilder<ChatRoom> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(50);
            builder.Property(p => p.Description).IsRequired().HasMaxLength(100);
            builder.Property(p => p.LastMessage).IsRequired().HasMaxLength(500);
        }
        public void MessageConfigure(EntityTypeBuilder<Message> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Msg).IsRequired().HasMaxLength(500);
            builder.Property(p => p.DateSend).IsRequired();
            builder.Property(p => p.ChatRoomId).IsRequired();
            builder.Property(p => p.UserId).IsRequired();
        }
        public void ChatRoomUserConfigure(EntityTypeBuilder<ChatRoomUser> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.UserId).IsRequired();
            builder.Property(p => p.ChatRoomId).IsRequired();
        }
    }
}
