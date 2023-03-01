using IOT_Backend.DTOs.User;
using IOT_Backend.Models;
using AutoMapper;
using IOT_Backend.Data;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace IOT_Backend.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly DataContext context;
        private readonly IMapper mapper;
        private readonly IConfiguration config;

        public UserService(DataContext context, IMapper mapper, IConfiguration config){
            this.context = context;
            this.mapper = mapper;
            this.config = config;
        }
        public async Task<ServiceResponse<UserDto>> Login(LoginUserDto userData)
        {
            var sr = new ServiceResponse<UserDto>();
            var u = await context.User.FirstOrDefaultAsync(u => u.Email == userData.Email);
            if(u is null){
                sr.Success = false;
                sr.Message = "Incorrect credentials!";
                return sr;
            }
            var ph = new PasswordHasher<User>();
            PasswordVerificationResult passwordResult = ph.VerifyHashedPassword(null, u.Password, userData.Password);
            if(passwordResult == PasswordVerificationResult.Success){
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityKey = config.GetValue<string>("AppSettings:SecretKey");
                var keyBytes = string.IsNullOrEmpty(securityKey) ? null : Encoding.ASCII.GetBytes(securityKey);
                if (keyBytes == null) {
                    throw new Exception("Security key is not configured.");
                }
                var tokenDescriptor = new SecurityTokenDescriptor{
                    Subject = new ClaimsIdentity(new Claim[]{
                        new Claim(ClaimTypes.Name, u.Email)
                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes),SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                sr.Success = true;
                sr.Message = "Login succesfull!";
                sr.Data = mapper.Map<UserDto>(u);
                sr.Token = tokenHandler.WriteToken(token);
                return sr;
            }
            sr.Success = false;
            sr.Message = "Incorrect credentials!"; 
            return sr;
        }

        public async Task<ServiceResponse<UserDto>> Register(RegisterUserDto user)
        {
            var sr = new ServiceResponse<UserDto>();
            var existingUser = await context.User.FirstOrDefaultAsync(u => u.Email == user.Email);
            if (existingUser != null) 
            {
                sr.Success = false;
                sr.Message = "A user with the same email already exists.";
                return sr;
            }
            User newUser = mapper.Map<User>(user);
            var ph = new PasswordHasher<User>();
            var hashedPassword = ph.HashPassword(null, newUser.Password);
            newUser.Password = hashedPassword;
            context.User.Add(newUser);
            await context.SaveChangesAsync();

            var tokenHandler = new JwtSecurityTokenHandler();
                var securityKey = config.GetValue<string>("AppSettings:SecretKey");
                var keyBytes = string.IsNullOrEmpty(securityKey) ? null : Encoding.ASCII.GetBytes(securityKey);
                if (keyBytes == null) {
                    throw new Exception("Security key is not configured.");
                }            var tokenDescriptor = new SecurityTokenDescriptor{
                Subject = new ClaimsIdentity(new Claim[]{
                    new Claim(ClaimTypes.Name, newUser.Email)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes),SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            sr.Data = mapper.Map<UserDto>(newUser);
            sr.Token = tokenHandler.WriteToken(token);
            return sr;
        }
    }
}