using Microsoft.AspNetCore.Mvc;
using DutchTreat.ViewModels;
using DutchTreat.Services;
using DutchTreat.Data;
using Microsoft.AspNetCore.Identity;
using DutchTreat.Data.Entities;
using System.Threading.Tasks;

namespace DutchTreat.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<StoreUser> signInManager;

        public AccountController(SignInManager<StoreUser> signInManager)
        {
            this.signInManager = signInManager;
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
        
    }
}
