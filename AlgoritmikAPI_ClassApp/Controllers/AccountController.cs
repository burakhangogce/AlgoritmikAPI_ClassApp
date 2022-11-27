using AlgoritmikAPI_ClassApp.DTO;
using AlgoritmikAPI_ClassApp.Interface;
using AlgoritmikAPI_ClassApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace AlgoritmikAPI_ClassApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        public IConfiguration _configuration;
        readonly DatabaseContext _dbContext = new();

        public AccountController(IConfiguration config, DatabaseContext context)
        {
            _configuration = config;
            _dbContext = context;

        }
        public string CreateToken(UserInfo user)
        {

            var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("UserId", user.UserId.ToString()),
                        new Claim("DisplayName", user.DisplayName),
                        new Claim("UserName", user.UserName),
                        new Claim("Email", user.Email)
                    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: signIn);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }


        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (await UserExists(registerDto.UserName!)) return BadRequest("UserName Is Already Taken");

            var user = new UserInfo
            {
                UserName = registerDto.UserName!.ToLower(),
                Email = registerDto.Email,
                DisplayName = "dd",
                Password = "ddd",
                CreatedDate = DateTime.Now,

            };

            _dbContext.UserInfos!.Add(user);
            await _dbContext.SaveChangesAsync();

            var userDto = new UserDto
            {
                UserName = registerDto.UserName!,
                Token = CreateToken(user),
                
            };

            return userDto;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDTO loginDto)
        {
            
            var user = await _dbContext.UserInfos!
                .SingleOrDefaultAsync(x => x.UserName == loginDto.Username);

            if (user == null) return Unauthorized("Invalid UserName");
                if (loginDto.Password != user.Password) return Unauthorized("Invalid Password");

            var userDto = new UserDto
            {
                UserName = loginDto.Username!,
                Token = CreateToken(user),

            };

            return userDto;
        }


        private async Task<bool> UserExists(string username)
        {
            return await _dbContext.UserInfos!.AnyAsync(x => x.UserName == username.ToLower());
        }

    }
}
