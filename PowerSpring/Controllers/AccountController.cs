using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using PowerSpring.ViewModels;
using PowerSpring.Helper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using PowerSpring.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PowerSpring.Controllers
{
    public class AccountController : Controller
    {
        private IUserManager _userManager;

        public AccountController( IUserManager userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
                return View(loginViewModel);

            var user =  _userManager.Authenticate(loginViewModel.UserName,loginViewModel.Password);

            if (user == null)
            {
                ModelState.AddModelError("", "User name/password not found");
            }
            ClaimsIdentity identity = new ClaimsIdentity(this.GetUserClaims(user), CookieAuthenticationDefaults.AuthenticationScheme);
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            return RedirectToAction("Index", "Account");

        }

        public IActionResult Register()
        {
            return View(new LoginViewModel());
        }
        public IActionResult RegisterSuccess() {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser() { UserName = loginViewModel.UserName };
                var result =  _userManager.Create(loginViewModel.UserName, loginViewModel.Password);

                if (result!=null)
                {
                    return RedirectToAction("RegisterSuccess", "Account");
                }
            }            
            return View(loginViewModel);
        }
        private IEnumerable<Claim> GetUserClaims(WebUser user)
        {
            List<Claim> claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
           // claims.Add(new Claim(ClaimTypes.Email, user.Email));
            claims.AddRange(this.GetUserRoleClaims(user));
            return claims;
        }

        private IEnumerable<Claim> GetUserRoleClaims(WebUser user)
        {
            List<Claim> claims = new List<Claim>();
            var Role = "User";
            if (user.IsAdmin)
                Role = "Admin";
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.UserName.ToString()));
            claims.Add(new Claim(ClaimTypes.Role, Role));
            return claims;
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            //wait _signInManager.SignOutAsync();
            //await httpContext.SignOutAsync();
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
