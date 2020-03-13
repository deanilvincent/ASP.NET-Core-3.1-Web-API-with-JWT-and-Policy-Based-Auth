using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using src.Contracts;
using src.Dtos.AuthDtos;

namespace src.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IAuthService authService;
        public AuthController(IUserService userService, IAuthService authService)
        {
            this.authService = authService;
            this.userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLogin)
        {
            if (await userService.UserByUserName(userForLogin.Username) == null)
                return BadRequest("No user has found");

            if (await authService.SignInByUserNameAndPassword(
            await userService.UserByUserName(userForLogin.Username),
            userForLogin.Password)) // sign in using the username and password
            {
                return Ok(new
                {
                    token = await authService.LoginAndRetrieveJwtToken(userForLogin.Username)
                });
            }

            return BadRequest("Username or password is incorrect.");
        }
    }
}