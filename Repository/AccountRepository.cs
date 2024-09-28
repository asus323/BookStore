using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BookStore.Data;
using BookStore.Interfaces;
using BookStore.Model.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BookStore.Repository
{
    public class AccountRepository:IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AccountRepository(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
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
        public async Task<string> Login(LoginDTO loginDTO){
            var result = await _signInManager.PasswordSignInAsync(loginDTO.Email,loginDTO.Password,false,false);
           if(!result.Succeeded)return null;
           //claims -metadata signiture
           var authClaim = new List<Claim>(){
            new Claim(ClaimTypes.Email,loginDTO.Email),
            new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
           } ;
           var authSingInKey =  new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
           var token = new JwtSecurityToken(
            issuer:_configuration["JWT:ValidIssuer"],
            audience:_configuration["JWT:ValidAudience"],
            expires:DateTime.Now.AddDays(1),
            claims:authClaim,
            signingCredentials: new SigningCredentials(authSingInKey, SecurityAlgorithms.HmacSha256Signature)
           );
           return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }

    
}