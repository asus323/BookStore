using System.Threading.Tasks;
using BookStore.Model;
using Microsoft.AspNetCore.Identity;

namespace BookStore.Interfaces
{
    public interface IAccountRepository
    {
        Task<IdentityResult> SingUp(SingUpDTO singUpDTO);
    }
}