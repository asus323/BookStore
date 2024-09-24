using System.Threading.Tasks;
using BookStore.Data;
using BookStore.Interfaces;
using BookStore.Model;
using Microsoft.AspNetCore.Identity;

namespace BookStore.Repository
{
    public class AccountRepository:IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountRepository(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> SingUp(SingUpDTO singUpDTO)
        {
            var user = new ApplicationUser()
            {
                FirstName = singUpDTO.FirstName,
                LastName = singUpDTO.LastName,
                Email = singUpDTO.Email,
                UserName = singUpDTO.Email,
            };
            return await _userManager.CreateAsync(user, singUpDTO.Password);
        }
    }

    
}