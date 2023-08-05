using AlgoritmikAPI_ClassApp.Interface;
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
        private readonly IUser _IUser;


        public AccountController(IConfiguration config, DatabaseContext context, IUser IUser)
        {
            auth = new FirebaseAuthProvider(
                           new FirebaseConfig("AIzaSyDEQbDRh7R1Q8QQdOPPAloeF2IH814CJ_M"));
            _configuration = config;
            _dbContext = context;
            _IUser = IUser;

        }
        public string CreateToken(UserInfo user)
        {

            var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("UserId", user.UserId.ToString()),
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



        [HttpPost("registerfirebase")]
        public async Task<ActionResult<UserInfoResponseModel>> RegisterFirebase(RegisterModel registerModel)
        {
            ResponseModel responseModel = new ResponseModel(isSuccess: true, statusCode: 200, errorModel: null);
            var response = new UserInfoResponseModel(responseModel: responseModel);
            try
            {
                FirebaseAuthLink resp = await auth.CreateUserWithEmailAndPasswordAsync(registerModel.Email, registerModel.Password);
                var fbAuthLink = await auth.SignInWithEmailAndPasswordAsync(registerModel.Email, registerModel.Password);
                string token = fbAuthLink.FirebaseToken;
                if (token != null)
                {
                    var user = new UserInfo
                    {
                        Email = registerModel.Email,
                        Password = registerModel.Password,
                        CreatedDate = DateTime.Now,
                        UserType = registerModel.UserType,
                        Token = token
                    };
                    response.models.Add(user);
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

        [HttpGet("getUserWithEmail/{email}")]
        public async Task<ActionResult<UserInfoResponseModel>> GetUserWithEmail(String email)
        {
            ResponseModel responseModel = new ResponseModel(isSuccess: true, statusCode: 200, errorModel: null);
            var response = new UserInfoResponseModel(responseModel: responseModel);
            try
            {
                UserInfo userInfoModel = await Task.FromResult(_IUser.GetUserWithEmail(email));
                if (userInfoModel != null)
                {
                    response.models.Add(userInfoModel);
                    var user = new UserInfo
                    {
                        UserId = userInfoModel.UserId,
                        Email = userInfoModel.Email,
                        Password = userInfoModel.Password,
                        CreatedDate = DateTime.Now,
                        UserType = userInfoModel.UserType,
                    };
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
        [HttpGet("getUserWithId/{email}")]
        public async Task<ActionResult<UserInfoResponseModel>> GetUserWithId(String email)
        {
            ResponseModel responseModel = new ResponseModel(isSuccess: true, statusCode: 200, errorModel: null);
            var response = new UserInfoResponseModel(responseModel: responseModel);
            try
            {
                UserInfo userInfoModel = await Task.FromResult(_IUser.GetUserWithEmail(email));
                if (userInfoModel != null)
                {
                    response.models.Add(userInfoModel);
                    var user = new UserInfo
                    {
                        Email = userInfoModel.Email,
                        Password = userInfoModel.Password,
                        CreatedDate = DateTime.Now,
                        UserType = userInfoModel.UserType,
                    };
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
                if (token != null)
                {
                    UserInfo userInfoModel = await Task.FromResult(_IUser.GetUserWithEmail(login.Email));
                    var user = new UserInfo
                    {
                        UserId = userInfoModel.UserId,
                        Email = login.Email,
                        Password = login.Password,
                        CreatedDate = DateTime.Now,
                        UserType = userInfoModel.UserType,
                        Token = token,
                    };
                    response.models.Add(user);
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

                if (token != null)
                {
                    var user = new UserInfo
                    {
                        Email = login.Email,
                        Password = login.Password,
                        CreatedDate = DateTime.Now,

                    };
                    response.models.Add(user);

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


        private async Task<bool> UserExists(string email)
        {
            return await _dbContext.UserInfo!.AnyAsync(x => x.Email == email);
        }

    }
}
