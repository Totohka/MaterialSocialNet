using Moq;
using Goods.System.Social.Network.DomainServices.Realization;
using Goods.System.Social.Network.DAL.Repository;
using Goods.System.Social.Network.DomainModel.Entities;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using Goods.System.Social.Network.DAL.Repository.Interface;
using DomainServices.Validation;
using FluentValidation;
using Goods.System.Social.Network.DAL.Repository.Realization;
using NLog;

namespace UserServiceTests
{
    public class UserServiceTests
    {
        #region private
        private List<User> GetAllUsers()
        {
            return new List<User>()
            {
                new User
                {
                    Id = 1,
                    FirstName = "TestF1",
                    LastName = "TestL1",
                    Email = "test1@mail.ru",
                    Password = "123"
                },
                new User
                {
                    Id = 2,
                    FirstName = "TestF2",
                    LastName = "TestL2",
                    Email = "test2@mail.ru",
                    Password = "123"
                },
                new User
                {
                    Id = 3,
                    FirstName = "TestF3",
                    LastName = "TestL3",
                    Email = "test3@mail.ru",
                    Password = "123"
                }
            };
        }

        private User GetFakeUser(int id)
        {
            var user = new User
            {
                Id = id,
                FirstName = "FakeUser",
                LastName = "FakeUserLastName",
                Email = "fake@mail.ru",
                Password = "123"
            };
            return user;
        }
        #endregion

        #region CheckValidationInGetUser

        [Theory]
        [InlineData(-2, false)]
        [InlineData(-1, false)]
        [InlineData(0, false)]
        [InlineData(1, true)]
        [InlineData(2, true)]
        public async void CheckValidationInGetUser(int id, bool result)
        {

            #region Arrange

            var mockUserRepository = new Mock<IUserRepository>();
            var mockSettingPrivacyRepository = new Mock<ISettingPrivacyRepository>();
            var mockSettingNotificationRepository = new Mock<ISettingNotificationRepository>();
            var mockSubscribeRepository = new Mock<ISubscribeRepository>();
            var mockValidatorUser = new Mock<IValidator<User>>();
            var validatorInt = new IdValidator();
            var mockValidatorSettingPrivacy = new Mock<IValidator<SettingPrivacy>>();
            var mockLogger = new Mock<ILogger<UserService>>();
            
            mockUserRepository.Setup(x => x.GetAsync(id)).ReturnsAsync(GetFakeUser(id));

            UserService service = new UserService(
                mockUserRepository.Object,
                mockSettingPrivacyRepository.Object,
                mockSettingNotificationRepository.Object,
                mockSubscribeRepository.Object,
                mockValidatorUser.Object,
                validatorInt,
                mockValidatorSettingPrivacy.Object,
                mockLogger.Object
            );

            #endregion

            #region Act

            var answer = service.GetAsync(id);

            #endregion

            #region Assert

            Assert.Equal(answer.Exception is null, result);

            #endregion
        }

        #endregion

        #region CheckInGetUserIsNotNull

        [Theory]
        [InlineData(1, true)]
        [InlineData(2, true)]
        public async void CheckInGetUserIsNotNull(int id, bool result)
        {

            #region Arrange

            var mockUserRepository = new Mock<IUserRepository>();
            var mockSettingPrivacyRepository = new Mock<ISettingPrivacyRepository>();
            var mockSettingNotificationRepository = new Mock<ISettingNotificationRepository>();
            var mockSubscribeRepository = new Mock<ISubscribeRepository>();
            var mockValidatorUser = new Mock<IValidator<User>>();
            var validatorInt = new IdValidator();
            var mockValidatorSettingPrivacy = new Mock<IValidator<SettingPrivacy>>();
            var mockLogger = new Mock<ILogger<UserService>>();

            mockUserRepository.Setup(x => x.GetAsync(id)).ReturnsAsync(GetFakeUser(id));

            UserService service = new UserService(
                mockUserRepository.Object,
                mockSettingPrivacyRepository.Object,
                mockSettingNotificationRepository.Object,
                mockSubscribeRepository.Object,
                mockValidatorUser.Object,
                validatorInt,
                mockValidatorSettingPrivacy.Object,
                mockLogger.Object
            );

            #endregion

            #region Act

            var answer = service.GetAsync(id);

            #endregion

            #region Assert

            Assert.Equal(answer.Result is not null, result);

            #endregion
        }

        #endregion

        #region CheckInGetUserIsNotNull

        [Theory]
        [InlineData(1, true)]
        [InlineData(2, true)]
        public async void CheckInGetAllAsyncUserIsNotNull(int id, bool result)
        {

            #region Arrange

            var mockUserRepository = new Mock<IUserRepository>();
            var mockSettingPrivacyRepository = new Mock<ISettingPrivacyRepository>();
            var mockSettingNotificationRepository = new Mock<ISettingNotificationRepository>();
            var mockSubscribeRepository = new Mock<ISubscribeRepository>();
            var mockValidatorUser = new Mock<IValidator<User>>();
            var validatorInt = new IdValidator();
            var mockValidatorSettingPrivacy = new Mock<IValidator<SettingPrivacy>>();
            var mockLogger = new Mock<ILogger<UserService>>();

            mockUserRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(GetAllUsers());

            UserService service = new UserService(
                mockUserRepository.Object,
                mockSettingPrivacyRepository.Object,
                mockSettingNotificationRepository.Object,
                mockSubscribeRepository.Object,
                mockValidatorUser.Object,
                validatorInt,
                mockValidatorSettingPrivacy.Object,
                mockLogger.Object
            );

            #endregion

            #region Act

            var answer = service.GetAllAsync("", 0, id, 0);

            #endregion

            #region Assert

            Assert.Equal(answer.Result is not null, result);

            #endregion
        }

        #endregion
    }
}