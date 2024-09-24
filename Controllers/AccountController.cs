using System.Linq;
using System.Threading.Tasks;
using BookStore.Interfaces;
using BookStore.Model;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class AccountController:ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        /* SingUp */
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] SingUpDTO singUpDTO)
        {
            var result = await _accountRepository.SingUp(singUpDTO);
            if (result.Succeeded)
            {
                return Ok();
            }

            return BadRequest(result.Errors.Select(x=>x.Description));
        }
    }
}