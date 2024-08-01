using DomainModel.Entities.ViewModels;
using DomainServices.Chat.Interface;
using DomainServices.Validation;
using FluentValidation;
using Goods.System.Social.Network.DAL.Repository.Interface;
using Goods.System.Social.Network.DomainModel.Entities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Logging;
using NLog;
using System.Reflection;

namespace DomainServices.Chat.Realization
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository _chatRepository;
        private readonly IRepository<ChatRoomUser> _repositoryChatRoomUser;
        private readonly ILogger<ChatService> _logger;
        private readonly IValidator<ChatRoom> _chatRoomValidator;
        private readonly IValidator<int> _idValidator;
        private readonly int _countReturnChatRooms = 4;
        public ChatService(IRepository<ChatRoomUser> repositoryChatRoomUser,
                           ILogger<ChatService> logger,
                           IValidator<ChatRoom> chatRoomValidator,
                           IValidator<int> idValidator,
                           IChatRepository chatRepository)
        {
            _repositoryChatRoomUser = repositoryChatRoomUser;
            _logger = logger;
            _chatRoomValidator = chatRoomValidator;
            _idValidator = idValidator;
            _chatRepository = chatRepository;
        }

        public async Task<int> CreateAsync(ChatRoom chatRoom, int userId)
        {
            _logger.LogTrace($"Вызван метод Create с параметрами: chatRoom: {chatRoom}, userId: {userId}");

            var result = _chatRoomValidator.Validate(chatRoom);
            if (!result.IsValid) throw new ValidationException(result.Errors);
            result = _idValidator.Validate(userId);
            if (!result.IsValid) throw new ValidationException(result.Errors);

            return await _chatRepository.CreateAsync(chatRoom, userId);
        }

        public void Delete(int chatId)
        {
            _logger.LogTrace($"Вызван метод Delete с параметрами: chatId: {chatId}");

            var result = _idValidator.Validate(chatId);
            if (!result.IsValid) throw new ValidationException(result.Errors);

            _chatRepository.Delete(chatId);
        }

        public async Task<ChatRoom> GetAsync(int chatId)
        {
            _logger.LogTrace($"Вызван метод GetAsync с параметрами: chatId: {chatId}");

            var result = await _idValidator.ValidateAsync(chatId);
            if (!result.IsValid) throw new ValidationException(result.Errors);

            return await _chatRepository.GetAsync(chatId);
        }

        public async Task<PageChat> GetChatsAsync(int userId, int number, string search)
        {
            _logger.LogTrace($"Вызван метод GetChatsAsync с параметрами: userId: {userId}, number: {number}, search:{search}");

            var result = await _idValidator.ValidateAsync(userId);
            if (!result.IsValid) throw new ValidationException(result.Errors);

            var chats = await _chatRepository.GetAllAsync();
            var chatsByUser = new List<ChatRoom>();
            var chatRoomUsers = await _repositoryChatRoomUser.GetAllAsync();

            chatRoomUsers = chatRoomUsers.Where(x => x.UserId == userId).ToList();

            foreach (var chatRoomUser in chatRoomUsers)
            {
                if (chats.Where(c => c.Id == chatRoomUser.ChatRoomId).Any())
                    chatsByUser.Add(chats.Where(c => c.Id == chatRoomUser.ChatRoomId).First());
            }

            chatsByUser = chatsByUser.Where(c => c.Name.Contains(search)).ToList();

            foreach (var chatByUser in chatsByUser)
            {
                chatByUser.Messages = null;
            }
            PageChat pageChatViewModel = new PageChat(chatsByUser.Count,
                (int)Math.Ceiling((double)chatsByUser.Count / _countReturnChatRooms),
                number,
                chatsByUser.Skip(_countReturnChatRooms * number).Take(_countReturnChatRooms).ToList()
                );
            return pageChatViewModel; 
        }

        public async Task UpdateAsync(ChatRoom chatRoom)
        {
            _logger.LogTrace($"Вызван метод Update с параметрами: chatRoom: {chatRoom}");

            var result = _chatRoomValidator.Validate(chatRoom);
            if (!result.IsValid) throw new ValidationException(result.Errors);

            ChatRoom chatRoomOld = await GetAsync(chatRoom.Id);

            chatRoomOld.Name = chatRoom.Name;
            chatRoomOld.Description = chatRoom.Description;

            _chatRepository.Update(chatRoomOld);
        }
    }
}