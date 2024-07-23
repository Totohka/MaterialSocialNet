using AutoMapper;
using DomainModel.Entities.ViewModels;
using Goods.System.Social.Network.DomainModel.Entities;
using Goods.System.Social.Network.DomainServices.Interface;
using Goods.System.Social.Network.Microservice.Users.Entities.DTO;
using Goods.System.Social.Network.Microservice.Users.Entities.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace Goods.System.Social.Network.Microservice.Users.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;
        private readonly IJWTService _jwtService;
        private readonly ISubscribeService _subscribeService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper, ISubscribeService subscribeService, ILogger<UserController> logger, ILoggerFactory loggerFactory, IJWTService jwtService)
        {
            _userService = userService;
            _logger = logger;
            _jwtService = jwtService;
            _subscribeService = subscribeService;
            _mapper = mapper;
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Update(ChangeUserViewModel changeUserViewModel)
        {
            _logger.LogInformation($"Вызван метод Update");

            if (int.Parse(HttpContext.User.FindFirst(claim => claim.Type == "id").Value) == changeUserViewModel.id)
            {
                User user = _mapper.Map<User>(changeUserViewModel);
                await _userService.UpdateAsync(user);
                return Ok(await _jwtService.UpdateTokenAsync(changeUserViewModel.id));
            }
            else
            {
                _logger.LogInformation($"Устаревший токен");
                return StatusCode(401);
            }
        }

        [Authorize]
        [HttpDelete]
        public void Delete(int userId)
        {
            //Пока не нужно, понадобится тогда, 
            //когда будем добавлять админку.
            //Пока что нужно оставить
        }

        [Authorize]
        [Route("all")]
        [HttpGet]
        public async Task<IActionResult> GetAll(string search = "", int number = 0, int who = 0) //реализация пагинации
        {
            _logger.LogInformation($"Вызван метод GetAll");

            int id = int.Parse(HttpContext.User.FindFirst(claim => claim.Type == "id").Value);
            PageUser pageUserViewModel = await _userService.GetAllAsync(search, number, id, who);
            var usersDTO = new List<UserDTO>();
            foreach (var user in pageUserViewModel.Users)
            {
                usersDTO.Add(_mapper.Map<UserDTO>(user));
                usersDTO.Last().IsSubscriber = await _subscribeService.GetCheckSubscriberAsync(id, user.Id);
            }

            var pageUserDTOViewModel = new PageUserDTO(pageUserViewModel.CountAllUsers, pageUserViewModel.PageCount, pageUserViewModel.NumberPage, usersDTO);
            return Ok(pageUserDTOViewModel);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            _logger.LogInformation($"Вызван метод Get");

            int mainId = int.Parse(HttpContext.User.FindFirst(claim => claim.Type == "id").Value);
            User user = await _userService.GetAsync(id);
            UserDTO userDTO = _mapper.Map<UserDTO>(user);
            userDTO.IsSubscriber = await _subscribeService.GetCheckSubscriberAsync(mainId, user.Id);
            return Ok(userDTO);
        }

        [Authorize]
        [HttpPost]
        public void Create(User user)
        {
            //Пока не нужно, понадобится тогда, 
            //когда будем добавлять админку.
            //Пока что нужно оставить
        }
    }
}
