using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using src.Contracts;
using src.Dtos.AuthDtos;
using src.Models.UserAccount;

namespace src.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IJwtGenerator jwtGenerator;
        public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, IJwtGenerator jwtGenerator)
        {
            this.jwtGenerator = jwtGenerator;
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        public async Task<bool> SignInByUserNameAndPassword(User user, string password)
        {
            var result = await signInManager.CheckPasswordSignInAsync(user, password, false);

            return result.Succeeded;
        }

        public async Task<string> LoginAndRetrieveJwtToken(string userName)
        {
            var appUser = await userManager.Users.FirstOrDefaultAsync(u => u.NormalizedUserName == userName.ToUpper());

            return await jwtGenerator.GenerateJwtTokenString(appUser);
        }
    }
}