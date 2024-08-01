using AutoMapper;
using Goods.System.Social.Network.DomainModel.Entities;
using Goods.System.Social.Network.DomainServices.Interface;
using Goods.System.Social.Network.Microservice.Auth.Entities.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Goods.System.Social.Network.Microservice.Auth.Controllers
{
    /// <summary>
    /// ��������������� ����������
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IJWTService _jwtService;
        private readonly IMapper _mapper;
        public AuthController(IJWTService jwtService, 
                              ILogger<AuthController> logger, 
                              IMapper mapper)
        {
            _mapper = mapper;
            _jwtService = jwtService;
            _logger = logger;
        }

        /// <summary>
        /// �����������
        /// </summary>
        /// <param name="email">����������� �����</param>
        /// <param name="password">������</param>
        /// <returns></returns>
        /// <response code="200">�������� ����������� � ������ ������</response>
        /// <response code="401">������ �����������</response>
        [HttpGet]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> Auth(string email, string password)
        {
            _logger.LogInformation($"������ ����� Auth");
            var response =  await _jwtService.AuthAsync(email, password);
            if (response == "401") {
                _logger.LogWarning($"������ ����� � ������� {email}");
                return StatusCode(401);
            }
            _logger.LogInformation($"���� � �������: {email}");
            return Ok(response);
        }

        /// <summary>
        /// �����������
        /// </summary>
        /// <param name="userRegistrationViewModel">�������� ������ ������������</param>
        /// <returns></returns>
        /// <response code="200">�������� ����������� � ������ ������</response>
        /// <response code="401">������ �����������</response>
        [HttpPost]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> Registration(UserRegistrationViewModel userRegistrationViewModel)
        {
            _logger.LogInformation($"������ ����� Registration");

            User user = _mapper.Map<User>(userRegistrationViewModel);

            var response = await _jwtService.RegistrationAsync(user);
            if (response == "Email �����!")
            {
                _logger.LogWarning(response);
                return StatusCode(401);
            }
            _logger.LogInformation($"Successful registration {user.Email}");
            return Ok(response);
        }
    }
}