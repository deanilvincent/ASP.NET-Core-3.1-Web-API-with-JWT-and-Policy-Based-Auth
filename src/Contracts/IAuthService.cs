using System.Threading.Tasks;
using src.Models.UserAccount;

namespace src.Contracts
{
    public interface IAuthService
    {
        Task<bool> SignInByUserNameAndPassword(User user, string password);
         Task<string> LoginAndRetrieveJwtToken(string userName);
    }
}