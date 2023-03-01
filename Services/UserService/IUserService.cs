using IOT_Backend.DTOs.User;
using IOT_Backend.Models;

namespace IOT_Backend.Services.UserService
{
    public interface IUserService
    {
        Task<ServiceResponse<UserDto>> Register(RegisterUserDto user);
        Task<ServiceResponse<UserDto>> Login(LoginUserDto user);
    }
}