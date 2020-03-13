using System.Threading.Tasks;
using src.Models.UserAccount;

namespace src.Contracts
{
    public interface IUserService
    {
        Task<User> UserByUserName(string username);
        Task<bool> UserExists(string username);
    }
}