using System.Threading.Tasks;
using BookStore.Model.Account;
using Microsoft.AspNetCore.Identity;

namespace BookStore.Interfaces
{
    public interface IAccountRepository
    {
        Task<IdentityResult> SingUp(SingUpDTO singUpDTO);
        Task<string> Login(LoginDTO loginDTO);
    }
}