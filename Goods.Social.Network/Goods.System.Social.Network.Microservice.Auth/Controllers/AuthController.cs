using Goods.System.Social.Network.DomainModel.Entities;
using Goods.System.Social.Network.DomainServices.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Goods.System.Social.Network.Microservice.Auth.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IJWTService _jwtService;
        public AuthController(IJWTService jwtService, ILogger<AuthController> logger)
        {
            
            _jwtService = jwtService;
            _logger = logger;
        }

        [HttpGet]
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

        [HttpPost]
        public async Task<IActionResult> Registration(User user)
        {
            _logger.LogInformation($"������ ����� Registration");
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