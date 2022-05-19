using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UpMenu.Core.DTOs.Account;
using UpMenu.Core.Services.Interfaces;
using UpMenu.Core.Utilities.Common;

namespace UpMenu.Web.Controllers
{
    public class AccountController : SiteBaseController
    {
        private IUserService userService;
        public AccountController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet("register")]
        public async Task<IActionResult> Register()
        {
            LoginUserDTO obj = new LoginUserDTO();
            return Ok(obj);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDTO register)
        {
            if (ModelState.IsValid)
            {
                var res = await userService.RegisterUser(register);
                switch (res)
                {
                    case RegisterUserResult.EmailExists:
                        return JsonResponseStatus.Error(new { info = "EmailExist" });
                    default:
                        break;
                }
            }
            return JsonResponseStatus.Success();
        }

        [HttpGet("login")]
        public async Task<IActionResult> Login()
        {
            LoginUserDTO obj = new LoginUserDTO();
            return Ok(obj);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO login)
        {
            if (ModelState.IsValid)
            {
                var res = await userService.LoginUser(login);
                switch (res)
                {
                    case LoginUserResult.IncorrectData:
                        return JsonResponseStatus.NotFound(new { message = "کاربری با مشخصات وارد شده یافت نشد" });
                    case LoginUserResult.NotActivated:
                        return JsonResponseStatus.Error(new { message = "حساب کاربری شما فعال نشده است" });
                    //case LoginUserResult.Success:
                    //    var user = await userService.GetUserByEmail(login.Email);
                    //    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("AngularEshopJwtBearer"));
                    //    var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                    //    var tokenOptions = new JwtSecurityToken(
                    //           issuer: "https://localhost:44369",
                    //           claims: new List<Claim>
                    //           {
                    //               new Claim(ClaimTypes.Name, user.Email),
                    //               new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                    //           },
                    //           expires: DateTime.Now.AddDays(30),
                    //           signingCredentials: signinCredentials
                    //        );
                    //    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                    //    return JsonResponseStatus.Success(new { token = tokenString, expireTime = 30, firstName = user.FirstName, lastName = user.LastName, userId = user.Id, address = user.Address });
                    default:
                        break;
                }
            }
            return JsonResponseStatus.Error();
        }

    }
}
