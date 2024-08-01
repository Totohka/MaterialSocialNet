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
using System.Net;

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

        /// <summary>
        /// Получение типа реакции
        /// </summary>
        /// <param name="changeUserViewModel">Модель для редактирования юзера</param>
        /// <returns></returns>
        /// <response code="200">Новый JWT токен</response>
        /// <response code="401">Ошибка авторизации</response>
        [Authorize]
        [HttpPut]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> Update(ChangeUserViewModel changeUserViewModel)
        {
            _logger.LogInformation($"Вызван метод Update");

            if (int.Parse(HttpContext.User.FindFirst(claim => claim.Type == "id").Value) == changeUserViewModel.Id)
            {
                User user = _mapper.Map<User>(changeUserViewModel);
                await _userService.UpdateAsync(user);
                return Ok(await _jwtService.UpdateTokenAsync(changeUserViewModel.Id));
            }
            else
            {
                _logger.LogInformation($"Устаревший токен");
                return StatusCode(401);
            }
        }

        /// <summary>
        /// Удаление пользователя вручную
        /// Сейчас не работает
        /// </summary>
        /// <param name="id">Id юзера</param>
        /// <returns></returns>
        /// <response code="200">Всё ок</response>
        /// <response code="401">Ошибка авторизации</response>
        [Authorize]
        [HttpDelete("{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        public IActionResult Delete(int id)
        {
            //Пока не нужно, понадобится тогда, 
            //когда будем добавлять админку.
            //Пока что нужно оставить
            return Ok();
        }

        /// <summary>
        /// Получение страницы юзеров DTO
        /// </summary>
        /// <param name="search">Строка поиска</param>
        /// <param name="number">Параметр пагинации</param>
        /// <param name="who">0 - Все юзеры;\n 1 - Твои подписчики; \n 2 - Твои друзья (взаимная подписка);</param>
        /// <returns></returns>
        /// <response code="200">Страница юзеров</response>
        /// <response code="401">Ошибка авторизации</response>
        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(PageUserDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
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

        /// <summary>
        /// Получение юзера DTO
        /// </summary>
        /// <param name="id">Id юзера</param>
        /// <returns></returns>
        /// <response code="200">Юзер DTO</response>
        /// <response code="401">Ошибка авторизации</response>
        [Authorize]
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(UserDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> Get(int id)
        {
            _logger.LogInformation($"Вызван метод Get");

            int mainId = int.Parse(HttpContext.User.FindFirst(claim => claim.Type == "id").Value);
            User user = await _userService.GetAsync(id);
            UserDTO userDTO = _mapper.Map<UserDTO>(user);
            userDTO.IsSubscriber = await _subscribeService.GetCheckSubscriberAsync(mainId, user.Id);
            return Ok(userDTO);
        }

        /// <summary>
        /// Создание юзера вручную без регистрации.
        /// Сейчас не работает
        /// </summary>
        /// <param name="user">Юзер</param>
        /// <returns></returns>
        /// <response code="200">Всё ок</response>
        /// <response code="401">Ошибка авторизации</response>
        [Authorize]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        public IActionResult Create(User user)
        {
            //Пока не нужно, понадобится тогда, 
            //когда будем добавлять админку.
            //Пока что нужно оставить
            return Ok();
        }
    }
}
