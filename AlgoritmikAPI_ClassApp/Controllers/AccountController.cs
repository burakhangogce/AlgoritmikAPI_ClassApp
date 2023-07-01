using AlgoritmikAPI_ClassApp.Models;
using Firebase.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AlgoritmikAPI_ClassApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        public IConfiguration _configuration;
        readonly DatabaseContext _dbContext = new();
        FirebaseAuthProvider auth;

        public AccountController(IConfiguration config, DatabaseContext context)
        {
            auth = new FirebaseAuthProvider(
                           new FirebaseConfig("AIzaSyDEQbDRh7R1Q8QQdOPPAloeF2IH814CJ_M"));
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
                expires: DateTime.UtcNow.AddMinutes(1000),
                signingCredentials: signIn);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }


        [HttpPost("register")]
        public async Task<ActionResult<UserModel>> Register(RegisterDto registerDto)
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

            _dbContext.UserInfo!.Add(user);
            await _dbContext.SaveChangesAsync();

            var userDto = new UserModel
            {
                UserName = registerDto.UserName!,
                Token = CreateToken(user),
            };

            return userDto;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserResponseModel>> Login(LoginDTO loginDto)
        {
            ResponseModel responseModel = new ResponseModel(isSuccess: true, statusCode: 200, errorModel: null);
            var response = new UserResponseModel(responseModel: responseModel);
            try
            {
                var user = await _dbContext.UserInfo!.SingleOrDefaultAsync(x => x.UserName == loginDto.Username);
                if (user == null)
                {
                    response.isSuccess = false;
                    response.errorModel = new ErrorResponseModel(errorMessage: "Invalid UserName");
                    return response;
                }
                if (loginDto.Password != user.Password)
                {
                    response.isSuccess = false;
                    response.errorModel = new ErrorResponseModel(errorMessage: "Invalid Password");
                    return response;
                }
                var userModel = new UserModel
                {
                    UserName = loginDto.Username!,
                    Token = CreateToken(user),
                };
                response.models.Add(userModel);
                return response;
            }
            catch (Exception ex)
            {
                response.statusCode = 400;
                response.isSuccess = false;
                response.errorModel = new ErrorResponseModel(errorMessage: ex.Message);
                return response;
            }

        }
        [HttpPost("registerfirebase")]
        public async Task<ActionResult<UserInfoResponseModel>> RegisterFirebase(LoginModel login)
        {
            ResponseModel responseModel = new ResponseModel(isSuccess: true, statusCode: 200, errorModel: null);
            var response = new UserInfoResponseModel(responseModel: responseModel);
            try
            {
                FirebaseAuthLink resp = await auth.CreateUserWithEmailAndPasswordAsync(login.Email, login.Password);
                var fbAuthLink = await auth.SignInWithEmailAndPasswordAsync(login.Email, login.Password);
                string token = fbAuthLink.FirebaseToken;
                response.models.Add(fbAuthLink);
                var user = new UserInfo
                {
                    UserName = "username",
                    Email = login.Email,
                    DisplayName = "dd",
                    Password = login.Password,
                    CreatedDate = DateTime.Now,

                };
                if (token != null)
                {
                    _dbContext.UserInfo!.Add(user);
                    await _dbContext.SaveChangesAsync();
                }
                return response;
            }
            catch (FirebaseAuthException ex)
            {
                response.statusCode = 400;
                response.isSuccess = false;
                response.errorModel = new ErrorResponseModel(errorMessage: ex.Message);
                return response;
            }
        }

        [HttpPost("loginfirebase")]
        public async Task<ActionResult<UserInfoResponseModel>> LoginFirebase(LoginModel login)
        {
            ResponseModel responseModel = new ResponseModel(isSuccess: true, statusCode: 200, errorModel: null);
            var response = new UserInfoResponseModel(responseModel: responseModel);
            try
            {
                var fbAuthLink = await auth.SignInWithEmailAndPasswordAsync(login.Email, login.Password);
                string token = fbAuthLink.FirebaseToken;
                response.models.Add(fbAuthLink);
                var user = new UserInfo
                {
                    UserName = "username",
                    Email = login.Email,
                    DisplayName = "dd",
                    Password = login.Password,
                    CreatedDate = DateTime.Now,

                };
                return response;
            }
            catch (FirebaseAuthException ex)
            {
                response.statusCode = 400;
                response.isSuccess = false;
                response.errorModel = new ErrorResponseModel(errorMessage: ex.Message);
                return response;
            }
        }

        [HttpPost("refreshfirebase")]
        public async Task<ActionResult<UserInfoResponseModel>> RefreshToken(LoginModel login)
        {
            ResponseModel responseModel = new ResponseModel(isSuccess: true, statusCode: 200, errorModel: null);
            var response = new UserInfoResponseModel(responseModel: responseModel);
            try
            {
                FirebaseAuthLink resp = await auth.SignInWithEmailAndPasswordAsync(login.Email, login.Password);
                var fbAuthLink = await auth.RefreshAuthAsync(resp);
                string token = fbAuthLink.FirebaseToken;
                response.models.Add(fbAuthLink);
                var user = new UserInfo
                {
                    UserName = "username",
                    Email = login.Email,
                    DisplayName = "dd",
                    Password = login.Password,
                    CreatedDate = DateTime.Now,

                };
                return response;
            }
            catch (FirebaseAuthException ex)
            {
                response.statusCode = 400;
                response.isSuccess = false;
                response.errorModel = new ErrorResponseModel(errorMessage: ex.Message);
                return response;
            }
        }


        private async Task<bool> UserExists(string username)
        {
            return await _dbContext.UserInfo!.AnyAsync(x => x.UserName == username.ToLower());
        }

    }
}
