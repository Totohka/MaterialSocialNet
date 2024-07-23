﻿// <auto-generated />
using System;
using Goods.System.Social.Network.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DAL.Migrations
{
    [DbContext(typeof(UserContext))]
    [Migration("20240322041809_CreateDBWithReaction")]
    partial class CreateDBWithReaction
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Goods.System.Social.Network.DomainModel.Entities.ChatRoom", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("LastMessage")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("ChatRooms");
                });

            modelBuilder.Entity("Goods.System.Social.Network.DomainModel.Entities.ChatRoomUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ChatRoomId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ChatRoomId");

                    b.HasIndex("UserId");

                    b.ToTable("ChatRoomUsers");
                });

            modelBuilder.Entity("Goods.System.Social.Network.DomainModel.Entities.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Goods.System.Social.Network.DomainModel.Entities.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ChatRoomId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateSend")
                        .HasColumnType("datetime2");

                    b.Property<string>("Msg")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ChatRoomId");

                    b.HasIndex("UserId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("Goods.System.Social.Network.DomainModel.Entities.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateCreate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Tags")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(10000)
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("Goods.System.Social.Network.DomainModel.Entities.Reaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("TypeReactionId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TypeReactionId");

                    b.HasIndex("UserId");

                    b.ToTable("Reactions");
                });

            modelBuilder.Entity("Goods.System.Social.Network.DomainModel.Entities.ReactionMessage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("MessageId")
                        .HasColumnType("int");

                    b.Property<int>("ReactionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MessageId");

                    b.HasIndex("ReactionId");

                    b.ToTable("ReactionMessages");
                });

            modelBuilder.Entity("Goods.System.Social.Network.DomainModel.Entities.ReactionPost", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.Property<int>("ReactionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("ReactionId");

                    b.ToTable("ReactionPosts");
                });

            modelBuilder.Entity("Goods.System.Social.Network.DomainModel.Entities.SettingNotification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("NewMessage")
                        .HasColumnType("bit");

                    b.Property<bool>("NewPosts")
                        .HasColumnType("bit");

                    b.Property<bool>("NewSubscibe")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("SettingNotifications");
                });

            modelBuilder.Entity("Goods.System.Social.Network.DomainModel.Entities.SettingPrivacy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("InvateChats")
                        .HasColumnType("int");

                    b.Property<int>("ShowDateBirthday")
                        .HasColumnType("int");

                    b.Property<int>("ShowPost")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("SettingPrivacies");
                });

            modelBuilder.Entity("Goods.System.Social.Network.DomainModel.Entities.TypeReaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("TypeReaction");
                });

            modelBuilder.Entity("Goods.System.Social.Network.DomainModel.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<DateTime>("DateBirthday")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(182)
                        .HasColumnType("nvarchar(182)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<int>("SettingNotificationId")
                        .HasColumnType("int");

                    b.Property<int>("SettingPrivacyId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("SettingNotificationId");

                    b.HasIndex("SettingPrivacyId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Goods.System.Social.Network.DomainModel.Entities.UserFriend", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("UserFriendId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserFriends");
                });

            modelBuilder.Entity("Goods.System.Social.Network.DomainModel.Entities.ChatRoomUser", b =>
                {
                    b.HasOne("Goods.System.Social.Network.DomainModel.Entities.ChatRoom", "ChatRoom")
                        .WithMany("ChatRoomUsers")
                        .HasForeignKey("ChatRoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Goods.System.Social.Network.DomainModel.Entities.User", "User")
                        .WithMany("ChatRooms")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ChatRoom");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Goods.System.Social.Network.DomainModel.Entities.Comment", b =>
                {
                    b.HasOne("Goods.System.Social.Network.DomainModel.Entities.Post", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Goods.System.Social.Network.DomainModel.Entities.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Goods.System.Social.Network.DomainModel.Entities.Message", b =>
                {
                    b.HasOne("Goods.System.Social.Network.DomainModel.Entities.ChatRoom", "ChatRoom")
                        .WithMany("Messages")
                        .HasForeignKey("ChatRoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Goods.System.Social.Network.DomainModel.Entities.User", "User")
                        .WithMany("Messages")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ChatRoom");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Goods.System.Social.Network.DomainModel.Entities.Post", b =>
                {
                    b.HasOne("Goods.System.Social.Network.DomainModel.Entities.User", "User")
                        .WithMany("Posts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Goods.System.Social.Network.DomainModel.Entities.Reaction", b =>
                {
                    b.HasOne("Goods.System.Social.Network.DomainModel.Entities.TypeReaction", "TypeReaction")
                        .WithMany("Reactions")
                        .HasForeignKey("TypeReactionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Goods.System.Social.Network.DomainModel.Entities.User", "User")
                        .WithMany("Reactions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TypeReaction");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Goods.System.Social.Network.DomainModel.Entities.ReactionMessage", b =>
                {
                    b.HasOne("Goods.System.Social.Network.DomainModel.Entities.Message", "Message")
                        .WithMany("ReactionMessages")
                        .HasForeignKey("MessageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Goods.System.Social.Network.DomainModel.Entities.Reaction", "Reaction")
                        .WithMany("ReactionMessages")
                        .HasForeignKey("ReactionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Message");

                    b.Navigation("Reaction");
                });

            modelBuilder.Entity("Goods.System.Social.Network.DomainModel.Entities.ReactionPost", b =>
                {
                    b.HasOne("Goods.System.Social.Network.DomainModel.Entities.Post", "Post")
                        .WithMany("ReactionPosts")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Goods.System.Social.Network.DomainModel.Entities.Reaction", "Reaction")
                        .WithMany("ReactionPosts")
                        .HasForeignKey("ReactionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("Reaction");
                });

            modelBuilder.Entity("Goods.System.Social.Network.DomainModel.Entities.User", b =>
                {
                    b.HasOne("Goods.System.Social.Network.DomainModel.Entities.SettingNotification", "SettingNotification")
                        .WithMany("User")
                        .HasForeignKey("SettingNotificationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Goods.System.Social.Network.DomainModel.Entities.SettingPrivacy", "SettingPrivacy")
                        .WithMany("User")
                        .HasForeignKey("SettingPrivacyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SettingNotification");

                    b.Navigation("SettingPrivacy");
                });

            modelBuilder.Entity("Goods.System.Social.Network.DomainModel.Entities.UserFriend", b =>
                {
                    b.HasOne("Goods.System.Social.Network.DomainModel.Entities.User", "User")
                        .WithMany("Friends")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Goods.System.Social.Network.DomainModel.Entities.ChatRoom", b =>
                {
                    b.Navigation("ChatRoomUsers");

                    b.Navigation("Messages");
                });

            modelBuilder.Entity("Goods.System.Social.Network.DomainModel.Entities.Message", b =>
                {
                    b.Navigation("ReactionMessages");
                });

            modelBuilder.Entity("Goods.System.Social.Network.DomainModel.Entities.Post", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("ReactionPosts");
                });

            modelBuilder.Entity("Goods.System.Social.Network.DomainModel.Entities.Reaction", b =>
                {
                    b.Navigation("ReactionMessages");

                    b.Navigation("ReactionPosts");
                });

            modelBuilder.Entity("Goods.System.Social.Network.DomainModel.Entities.SettingNotification", b =>
                {
                    b.Navigation("User");
                });

            modelBuilder.Entity("Goods.System.Social.Network.DomainModel.Entities.SettingPrivacy", b =>
                {
                    b.Navigation("User");
                });

            modelBuilder.Entity("Goods.System.Social.Network.DomainModel.Entities.TypeReaction", b =>
                {
                    b.Navigation("Reactions");
                });

            modelBuilder.Entity("Goods.System.Social.Network.DomainModel.Entities.User", b =>
                {
                    b.Navigation("ChatRooms");

                    b.Navigation("Comments");

                    b.Navigation("Friends");

                    b.Navigation("Messages");

                    b.Navigation("Posts");

                    b.Navigation("Reactions");
                });
#pragma warning restore 612, 618
        }
    }
}
