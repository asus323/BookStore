using System.Linq;
using System.Threading.Tasks;
using BookStore.Interfaces;
using BookStore.Model;
using BookStore.Model.Account;
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
        /* Login */
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {
            var result = await _accountRepository.Login(loginDto);
            if (string.IsNullOrEmpty(result))
            {
                return Unauthorized();
            }

            return Ok(result);
        }
    }
}