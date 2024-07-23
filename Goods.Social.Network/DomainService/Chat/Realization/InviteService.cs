using AutoMapper;
using DomainModel.Entities.ViewModels;
using DomainServices.Chat.Interface;
using DomainServices.Validation;
using FluentValidation;
using Goods.System.Social.Network.DAL.Repository.Interface;
using Goods.System.Social.Network.DomainModel.Entities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Logging;
using NLog;

namespace DomainServices.Chat.Realization
{
    public class InviteService : IInviteService
    {
        private readonly IChatRepository _chatRepository;
        private readonly IRepository<ChatRoomUser> _repositoryChatRoomUser;
        private readonly IUserRepository _userRepository;
        private readonly IValidator<ChatRoomUser> _chatRoomUserValidator;
        private readonly IValidator<int> _idValidator;
        private readonly ILogger<InviteService> _logger;
        public InviteService(IRepository<ChatRoomUser> repositoryChatRoomUser,
                           IChatRepository chatRepository,
                           IUserRepository userRepository,
                           IValidator<int> idValidator,
                           IValidator<ChatRoomUser> chatRoomUserValidator,
                           ILogger<InviteService> logger)
        {
            _repositoryChatRoomUser = repositoryChatRoomUser;
            _chatRepository = chatRepository;
            _idValidator = idValidator;
            _chatRoomUserValidator = chatRoomUserValidator;
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task DeleteAsync(ChatRoomUser chatRoomUser)
        {
            _logger.LogTrace($"Вызван метод DeleteAsync с параметрами: chatRoomUser: {chatRoomUser}");

            var result = await _chatRoomUserValidator.ValidateAsync(chatRoomUser);
            if (!result.IsValid) throw new ValidationException(result.Errors);

            var chatUsers = await _repositoryChatRoomUser.GetAllAsync();
            var chatUser = chatUsers.Where(cu => cu.UserId == chatRoomUser.UserId && cu.ChatRoomId == chatRoomUser.ChatRoomId).First();
            _repositoryChatRoomUser.DeleteAsync(chatUser.Id);
        }

        public async Task<List<User>> GetUsersInChatAsync(int chatId)
        {
            _logger.LogTrace($"Вызван метод GetUsersInChatAsync с параметрами: chatId: {chatId}");

            var result = await _idValidator.ValidateAsync(chatId);
            if (!result.IsValid) throw new ValidationException(result.Errors);

            var chatUsers = await _repositoryChatRoomUser.GetAllAsync();
            var chatUsersByChat = chatUsers.Where(cu => cu.ChatRoomId == chatId).ToList();
            List<User> users = new List<User>();
            foreach (var chatUser in chatUsersByChat)
            {
                var user = await _userRepository.GetAsync(chatUser.UserId);
                users.Add(user);
            }
            return users;
        }

        public async Task InvateUserInChatAsync(ChatRoomUser chatRoomUser)
        {
            _logger.LogTrace($"Вызван метод InvateUserInChatAsync с параметрами: chatRoomUser: {chatRoomUser}");

            var result = await _chatRoomUserValidator.ValidateAsync(chatRoomUser);
            if (!result.IsValid) throw new ValidationException(result.Errors);

            var chat = await _chatRepository.GetAsync(chatRoomUser.ChatRoomId);
            if (chat is not null)
            {
                _repositoryChatRoomUser.Create(chatRoomUser);
            }
        }
    }
}