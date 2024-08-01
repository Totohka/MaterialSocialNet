using AutoMapper;
using DomainModel.Entities.ViewModels;
using DomainServices.Chat.Interface;
using FluentValidation;
using Goods.System.Social.Network.DAL.Repository.Interface;
using Goods.System.Social.Network.DomainModel.Entities;
using Microsoft.Extensions.Logging;
using NLog;
namespace DomainServices.Chat.Realization
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IChatRepository _chatRepository;
        private readonly IRepository<ChatRoomUser> _repositoryChatRoomUser;
        private readonly ILogger<ChatService> _logger;
        private readonly IValidator<Message> _messageValidator;
        private readonly IValidator<int> _idValidator;
        private readonly IMapper _mapper;
        private readonly int _countReturnMessages = 20;
        public MessageService(IRepository<ChatRoomUser> repositoryChatRoomUser,
                           ILogger<ChatService> logger,
                           IMessageRepository messageRepository,
                           IChatRepository chatRepository,
                           IValidator<Message> messageValidator,
                           IValidator<int> idValidator,
                           IMapper mapper
                           )
        {
            _repositoryChatRoomUser = repositoryChatRoomUser;
            _logger = logger;
            _messageValidator = messageValidator;
            _idValidator = idValidator;
            _messageRepository = messageRepository;
            _chatRepository = chatRepository;
            _mapper = mapper;
        }

        public async Task<int> CreateAsync(Message message)
        {
            _logger.LogTrace($"Вызван метод CreateAsync с параметрами: message: {message}");

            var result = await _messageValidator.ValidateAsync(message);
            if (!result.IsValid) throw new ValidationException(result.Errors);

            var chat = await _chatRepository.GetAsync(message.ChatRoomId);
            chat.LastMessage = message.Msg;
            _chatRepository.Update(chat);
            return _messageRepository.Create(message);
        }

        public async Task DeleteAsync(int messageId)
        {
            _logger.LogTrace($"Вызван метод DeleteAsync с параметрами: messageId: {messageId}");

            var result = await _idValidator.ValidateAsync(messageId);
            if (!result.IsValid) throw new ValidationException(result.Errors);

            await _messageRepository.DeleteAsync(messageId);
        }

        public async Task<Message> GetAsync(int messageId)
        {
            _logger.LogTrace($"Вызван метод GetAsync с параметрами: messageId: {messageId}");

            var result = await _idValidator.ValidateAsync(messageId);
            if (!result.IsValid) throw new ValidationException(result.Errors);

            return await _messageRepository.GetAsync(messageId);
        }

        public async Task<PageMessage> GetMessagesAsync(int chatId, int number)
        {
            _logger.LogTrace($"Вызван метод GetMessagesAsync с параметрами: chatId: {chatId}, number: {number}");

            var result = await _idValidator.ValidateAsync(chatId);
            if (!result.IsValid) throw new ValidationException(result.Errors);

            var messageByChat = await _messageRepository.GetAllByChatAsync(chatId);
            messageByChat.Reverse();

            messageByChat.ForEach(message => {
                message.ChatRoom = null;
                message.ReactionMessages = null;
            });

            PageMessage pageMessageViewModel = new PageMessage(
                   messageByChat.Count,
                   (int)Math.Ceiling((double)messageByChat.Count / _countReturnMessages),
                   number,
                   messageByChat.Skip(number * _countReturnMessages).Take(_countReturnMessages).ToList()
               );
            pageMessageViewModel.Messages.Reverse();

            return pageMessageViewModel;
        }

        public async Task UpdateAsync(Message message)
        {
            _logger.LogTrace($"Вызван метод UpdateAsync с параметрами: message: {message}");

            var result = await _messageValidator.ValidateAsync(message);
            if (!result.IsValid) throw new ValidationException(result.Errors);

            var msg = await _messageRepository.GetAsync(message.Id);
            msg.Msg = message.Msg;
            _messageRepository.Update(msg);
        }
    }
}