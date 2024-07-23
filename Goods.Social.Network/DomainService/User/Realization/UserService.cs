using Goods.System.Social.Network.DAL.Repository.Interface;
using DomainModel.Entities.ViewModels;
using Goods.System.Social.Network.DomainModel.Entities;
using Goods.System.Social.Network.DomainServices.Interface;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using DomainModel.Entities.Dashboard;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Reflection;
using NLog;
using DomainServices.Validation;
using FluentValidation;
using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Azure;
using System.Diagnostics;

namespace Goods.System.Social.Network.DomainServices.Realization
{
    public class UserService : IUserService
    {
        private readonly int _countUserOnPage = 4;
        private readonly IUserRepository _repositoryUser;
        private readonly ISettingPrivacyRepository _repositorySettingPrivacy;
        private readonly ISettingNotificationRepository _repositorySettingNotification;
        private readonly ISubscribeRepository _subscribeRepository;
        private readonly IValidator<User> _userValidator;
        private readonly IValidator<int> _idValidator;
        private readonly IValidator<SettingPrivacy> _settingPrivacyValidator;
        private readonly ILogger<UserService> _logger;
        public UserService(IUserRepository repositoryUser,
                           ISettingPrivacyRepository repositorySettingPrivacy,
                           ISettingNotificationRepository repositorySettingNotification,
                           ISubscribeRepository subscribeRepository,
                           IValidator<User> userValidator,
                           IValidator<int> idValidator,
                           IValidator<SettingPrivacy> settingPrivacyValidator,
                           ILogger<UserService> logger)
        {
            _logger = logger;
            _repositoryUser = repositoryUser;
            _idValidator = idValidator;
            _repositorySettingPrivacy = repositorySettingPrivacy;
            _repositorySettingNotification = repositorySettingNotification;
            _subscribeRepository = subscribeRepository;
            _userValidator = userValidator;
            _settingPrivacyValidator = settingPrivacyValidator;
        }

        public async Task<User> GetAsync(int id)
        {
            _logger.LogTrace($"Вызван метод GetAsync с параметрами: id: {id}");

            var result = await _idValidator.ValidateAsync(id);
            if (!result.IsValid) throw new ValidationException(result.Errors);

            return await _repositoryUser.GetAsync(id);
        }

        public async Task<PageUser> GetAllAsync(string search, int number, int id, int who)
        {
            _logger.LogTrace($"Вызван метод GetAllAsync с параметрами: search: {search}, number: {number}, id: {id}, who: {who}");

            var allUsers = await _repositoryUser.GetAllAsync();
            List<User> userList = new List<User>();
            switch (who)
            {
                case 0: // все пользователи
                    if (search != "") //Если в поисковой строке, что-то было, значит нужно искать
                    {
                        allUsers = allUsers.Where(u => u.FirstName.Contains(search) || u.LastName.Contains(search)).ToList();
                    }
                    break;
                case 1: // подписчики
                    if (search != "") //Если в поисковой строке, что-то было, значит нужно искать
                    {
                        allUsers = allUsers.Where(u => u.FirstName.Contains(search) || u.LastName.Contains(search)).ToList();
                    }
                    foreach (var user in allUsers)
                    {
                        if (await _subscribeRepository.GetCheckAsync(user.Id, id))
                        {
                            userList.Add(user);
                        }
                    }
                    allUsers = userList;
                    break;
                case 2: // друзья
                    if (search != "") //Если в поисковой строке, что-то было, значит нужно искать
                    {
                        allUsers = allUsers.Where(u => u.FirstName.Contains(search) || u.LastName.Contains(search)).ToList();
                    }
                    foreach (var user in allUsers)
                    {
                        if (await _subscribeRepository.GetCheckAsync(user.Id, id) &&
                            await _subscribeRepository.GetCheckAsync(id, user.Id)) 
                        {
                            userList.Add(user);
                        }
                    }
                    allUsers = userList;
                    break;
                default:
                    break;
            }
            int count = allUsers.Count;
            int countPage = (int)Math.Ceiling((double)count / _countUserOnPage); //кол-во страниц пагинации
            var users = new List<User>();
            if (number != -1) //если -1 то выдать всех юзер, пагинация отключается
            {
                users = allUsers.Skip((number) * _countUserOnPage).Take(_countUserOnPage).ToList();//список юзеров
            }
            else
            {
                users = allUsers;
            }
            var pageUser = new PageUser(count, countPage, number + 1, users);
            return pageUser;
        }

        public async Task UpdateAsync(User user)
        {
            _logger.LogTrace($"Вызван метод UpdateAsync с параметрами: changeUserViewModel: {user}");

            var userOld = await _repositoryUser.GetAsync(user.Id);

            userOld.FirstName = user.FirstName != string.Empty ? user.FirstName : userOld.FirstName;
            userOld.LastName = user.LastName != string.Empty ? user.LastName : userOld.LastName;
            userOld.DateBirthday = user.DateBirthday != DateTime.MinValue ? user.DateBirthday : userOld.DateBirthday;
            userOld.City = user.City != string.Empty ? user.City : userOld.City;
            userOld.Country = user.Country != string.Empty ? user.Country : userOld.Country;
            userOld.Status = user.Status != string.Empty ? user.Status : userOld.Status;
            userOld.Email = user.Email != string.Empty ? user.Email : userOld.Email;
            userOld.Avatar = user.Avatar != string.Empty ? user.Avatar : userOld.Avatar;
            userOld.Background = user.Background != string.Empty ? user.Background : userOld.Background;

            var result = await _userValidator.ValidateAsync(userOld);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
            _repositoryUser.Update(userOld);
        }

        public async Task ChangeSettingPrivacyAsync(SettingPrivacy settingPrivacy, int userId)
        {
            _logger.LogTrace($"Вызван метод ChangeSettingPrivacyAsync с параметрами:  settingPrivacy: {settingPrivacy} и userId: {userId}");
            var result = await _settingPrivacyValidator.ValidateAsync(settingPrivacy);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
            result = await _idValidator.ValidateAsync(userId);
            if (!result.IsValid) throw new ValidationException(result.Errors);
            SettingPrivacy newSettingPrivacy = await _repositorySettingPrivacy.GetByItemAsync(settingPrivacy);
            var user = await _repositoryUser.GetAsync(userId);
            user.SettingPrivacyId = newSettingPrivacy.Id;
            _repositoryUser.Update(user);
        }

        public async Task ChangeSettingNotificationAsync(SettingNotification settingNotification, int userId)
        {
            _logger.LogTrace($"Вызван метод ChangeSettingNotificationAsync с параметрами: settingNotification: {settingNotification} и userId: {userId}");
            var result = await _idValidator.ValidateAsync(userId);
            if (!result.IsValid) throw new ValidationException(result.Errors);
            SettingNotification newSettingNotification = await _repositorySettingNotification.GetByItemAsync(settingNotification);
            var user = await _repositoryUser.GetAsync(userId);
            user.SettingNotificationId = newSettingNotification.Id;
            _repositoryUser.Update(user);
        }
    }
}