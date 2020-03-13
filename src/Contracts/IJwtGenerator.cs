using System.Threading.Tasks;
using src.Models.UserAccount;

namespace src.Contracts
{
    public interface IJwtGenerator
    {
         Task<string> GenerateJwtTokenString(User user);
    }
}