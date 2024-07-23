//using Moq;
//using Goods.System.Social.Network.DomainModel.Entities;
//using Microsoft.Extensions.Logging;
//using Goods.System.Social.Network.DAL.Repository.Interface;
//using DomainServices.Chat.Realization;

//namespace UserServiceTests
//{
//    public class ChatServiceTests
//    {
//        #region private
//        private ChatRoom GetAllChats()
//        {
//            var chat = new ChatRoom
//            {
//                Id = 1,
//                Name = "Testchat",
//                Description = "I'm a test one",
//                LastMessage = "Hello world"
//            };
//            return chat;
//        }
//        private ChatRoomUser GetChatRoomUser()
//        {
//            var user = new ChatRoomUser
//            {
//                Id = 1,
//                UserId = 1,
//                ChatRoomId = 1
//            };
//            return user;
//        }
//        private Message GetMessages()
//        {
//            var messages = new Message
//            {
//                Id = 1,
//                //IsCheck = true,
//                Msg = "Hello",
//                DateSend = DateTime.Now,
//                //UserId = 1,
//                //ChatRoomId = 1
//            };
//            return messages;
//        }
//        #endregion

//        #region GetAllChats
//        [Fact]
//        public void GetAll_NotEmpty()
//        {
//            #region Arrange
//            var mock = new Mock<IRepository<ChatRoom>>();
//            mock.Setup(x => x.GetAll()).ReturnsAsync(new List<ChatRoom> { GetAllChats(), GetAllChats() });
//            var mockuser = new Mock<IRepository<ChatRoomUser>>();
//            mockuser.Setup(x => x.GetAll()).ReturnsAsync(new List<ChatRoomUser> { GetChatRoomUser(), GetChatRoomUser() });
//            var mockmessage = new Mock<IRepository<Message>>();
//            mockmessage.Setup(x => x.GetAll()).ReturnsAsync(new List<Message> { GetMessages()});
//            var mocklogger = new Mock<ILogger<ChatService>>();
//            ChatService service = new ChatService(mock.Object, mockuser.Object, mockmessage.Object, mocklogger.Object);
//            #endregion

//            #region Act
//            Task<List<ChatRoom>> result = service.GetChats(1);
//            #endregion

//            #region Assert
//            Assert.NotEmpty(result.Result);
//            #endregion
//        }

//        [Fact]
//        public void GetAll_NotNull()
//        {
//            #region Arrange
//            var mock = new Mock<IRepository<ChatRoom>>();
//            mock.Setup(x => x.GetAll()).ReturnsAsync(new List<ChatRoom> { GetAllChats(), GetAllChats() });
//            var mockuser = new Mock<IRepository<ChatRoomUser>>();
//            mockuser.Setup(x => x.GetAll()).ReturnsAsync(new List<ChatRoomUser> { GetChatRoomUser(), GetChatRoomUser() });
//            var mockmessage = new Mock<IRepository<Message>>();
//            mockmessage.Setup(x => x.GetAll()).ReturnsAsync(new List<Message> { GetMessages() });
//            var mocklogger = new Mock<ILogger<ChatService>>();
//            ChatService service = new ChatService(mock.Object, mockuser.Object, mockmessage.Object, mocklogger.Object);
//            #endregion

//            #region Act
//            Task<List<ChatRoom>> result = service.GetChats(1);
//            #endregion

//            #region Assert
//            Assert.NotNull(result.Result);
//            #endregion
//        }
//        #endregion

//        #region CreateChats
//        [Fact]
//        public void CreateChats_true()
//        {
//            #region Arrange
//            var mock = new Mock<IRepository<ChatRoom>>();
//            mock.Setup(x => x.GetAll()).ReturnsAsync(new List<ChatRoom> { GetAllChats(), GetAllChats() });
//            var mockuser = new Mock<IRepository<ChatRoomUser>>();
//            mockuser.Setup(x => x.GetAll()).ReturnsAsync(new List<ChatRoomUser> { GetChatRoomUser(), GetChatRoomUser() });
//            var mockmessage = new Mock<IRepository<Message>>();
//            mockmessage.Setup(x => x.GetAll()).ReturnsAsync(new List<Message> { GetMessages() });
//            var mocklogger = new Mock<ILogger<ChatService>>();
//            ChatService service = new ChatService(mock.Object, mockuser.Object, mockmessage.Object, mocklogger.Object);
//            #endregion

//            #region Act
//            Task<bool> result = service.Create("Testing");
//            #endregion

//            #region Assert
//            Assert.True(result.Result);
//            #endregion
//        }
//        [Fact]
//        public void CreateChats_false()
//        {
//            #region Arrange
//            var mock = new Mock<IRepository<ChatRoom>>();
//            mock.Setup(x => x.GetAll()).ReturnsAsync(new List<ChatRoom> { GetAllChats()});
//            var mockuser = new Mock<IRepository<ChatRoomUser>>();
//            mockuser.Setup(x => x.GetAll()).ReturnsAsync(new List<ChatRoomUser> { GetChatRoomUser() });
//            var mockmessage = new Mock<IRepository<Message>>();
//            mockmessage.Setup(x => x.GetAll()).ReturnsAsync(new List<Message> { GetMessages() });
//            var mocklogger = new Mock<ILogger<ChatService>>();
//            ChatService service = new ChatService(mock.Object, mockuser.Object, mockmessage.Object, mocklogger.Object);
//            #endregion

//            #region Act
//            Task<bool> result = service.Create("Testchat");
//            #endregion

//            #region Assert
//            Assert.False(result.Result);
//            #endregion
//        }
//        #endregion
//    }
//}