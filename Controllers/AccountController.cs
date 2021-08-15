using Microsoft.AspNetCore.Mvc;
using DutchTreat.ViewModels;
using DutchTreat.Services;
using DutchTreat.Data;
using Microsoft.AspNetCore.Identity;
using DutchTreat.Data.Entities;
using System.Threading.Tasks;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace DutchTreat.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<StoreUser> signInManager;
        private readonly UserManager<StoreUser> userManager;
        private readonly IConfiguration config;

        public AccountController(
            SignInManager<StoreUser> signInManager, 
            UserManager<StoreUser> userManager,
            IConfiguration config)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.config = config;
        }

        [HttpGet]
        public IActionResult Login() 
        {
            if (this.User.Identity.IsAuthenticated) 
            {
                return RedirectToAction("Shop", "App");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromForm] LoginViewModel loginVm) 
        {
            if (ModelState.IsValid) 
            {
                var loginResult = await this.signInManager.PasswordSignInAsync(loginVm.UserName, loginVm.Password, true, false);
                if (loginResult.Succeeded)
                {
                    return RedirectToAction("shop", "app");
                }
                else
                {
                    ModelState.AddModelError("", "Login failed");
                }
            }
            return View();                
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await this.signInManager.SignOutAsync();
            return RedirectToAction("index", "app");
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateToken([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid) 
            {
                var user = await this.userManager.FindByNameAsync(model.UserName);
                var result = await this.signInManager.CheckPasswordSignInAsync(user, model.Password, false);
                if (result.Succeeded) 
                {
                    var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.config["Token:Key"]));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(
                        this.config["Token:Issuer"], 
                        this.config["Token:Audience"],
                        claims,
                        signingCredentials: creds,
                        expires: DateTime.UtcNow.AddMinutes(60));
                    return Created("", new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo
                    });

                }

            }


            return BadRequest();
        }
    }
}
