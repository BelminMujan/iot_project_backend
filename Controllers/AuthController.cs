using IOT_Backend.DTOs.User;
using IOT_Backend.Models;
using IOT_Backend.Services.UserService;
using Microsoft.AspNetCore.Mvc;

namespace IOT_Backend.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService UserService;
        public AuthController(IUserService UserService)
        {
            this.UserService = UserService;
        }
        [HttpPost("register")]
        public async Task<ActionResult<ServiceResponse<RegisterUserDto>>> Register(RegisterUserDto user)
        {
            return Ok(await UserService.Register(user));
        }
        [HttpPost("login")]
        public async Task<ActionResult<ServiceResponse<LoginUserDto>>> Login(LoginUserDto user)
        {
            return Ok(await UserService.Login(user));
        }
    }
}