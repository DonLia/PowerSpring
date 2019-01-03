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
using Microsoft.AspNetCore.Mvc.ModelBinding;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PowerSpring.Controllers
{
    public class AccountController : Controller
    {
        private IUserManager _userManager;

        public AccountController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Update(String id)
        {
            UpdateViewModel updateViewModel = new UpdateViewModel();
            updateViewModel.UpdateInfo = id;
            switch (updateViewModel.UpdateInfo) {
                case "Email":
                    updateViewModel.InputType = "email";
                    break;
                case "Phone":
                    updateViewModel.InputType = "tel";
                    break;
                case "Password":
                    updateViewModel.InputType = "password";
                    break;
                default:
                    updateViewModel.InputType = "text";
                    break;
            }
            return View(updateViewModel);
        }
        [HttpPost]
        public IActionResult Update(UpdateViewModel updateViewModel, String id)
        {
            if (String.IsNullOrWhiteSpace(updateViewModel.UpdateInfo))
                updateViewModel.UpdateInfo = id;
            if (!ModelState.IsValid)
            {
                return View(updateViewModel);
            }

            var user = _userManager.Authenticate(User.Identity.Name, updateViewModel.VerifyPassword);
            if (user == null)
            {
                ModelState.AddModelError("", "Your password is not correct!");
                updateViewModel.VerifyPassword = null;
                return View(updateViewModel);
            }

            switch (updateViewModel.UpdateInfo)
            {
                case "UserName":
                    user.UserName = updateViewModel.UpdateInfoValue;
                    _userManager.Update(user);
                    break;
                case "Email":
                    user.Email = updateViewModel.UpdateInfoValue;
                    _userManager.Update(user);
                    break;
                case "Phone":
                    user.Phone = updateViewModel.UpdateInfoValue;
                    _userManager.Update(user);
                    break;
                case "Password":
                    if (updateViewModel.VerifyUpdateInfoValue == updateViewModel.UpdateInfoValue)
                        _userManager.Update(user, updateViewModel.UpdateInfoValue);
                    else {
                        ModelState.AddModelError("", "Your new password does not match");
                        updateViewModel.VerifyUpdateInfoValue = null;
                        updateViewModel.UpdateInfoValue = null;
                        return View(updateViewModel);
                    }
                    break;
            }
            return RedirectToAction("Index", "Account");
        }


        public IActionResult Index()
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (id == null)
                return RedirectToAction("Login", "Account");
            var _user = _userManager.GetById(Convert.ToInt32(id));
            return View(_user);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
                return View(loginViewModel);

            var user = _userManager.Authenticate(loginViewModel.UserName, loginViewModel.Password);

            if (user == null)
            {
                ModelState.AddModelError("", "User name/password not found");
                return View(loginViewModel);
            }
            ClaimsIdentity identity = new ClaimsIdentity(this.GetUserClaims(user), CookieAuthenticationDefaults.AuthenticationScheme);
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            return RedirectToAction("Index", "News");

        }

        public IActionResult Register()
        {
            return View();
        }
        public IActionResult RegisterSuccess()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser() { UserName = loginViewModel.UserName };
                var result = _userManager.Create(loginViewModel.UserName, loginViewModel.Password);

                if (result != null)
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
            //var _Email = "No Email";
            //var _Phone = "No Phone";
            //if (!String.IsNullOrEmpty(user.Email))
            //    _Email = user.Email;
            //if (!String.IsNullOrEmpty(user.Phone))
            //    _Phone = user.Phone;
            //claims.Add(new Claim(ClaimTypes.Email, _Email));
            //claims.Add(new Claim(ClaimTypes.MobilePhone, _Phone));
            claims.AddRange(this.GetUserRoleClaims(user));
            return claims;
        }

        private IEnumerable<Claim> GetUserRoleClaims(WebUser user)
        {
            List<Claim> claims = new List<Claim>();
            var _Role = "User";
            if (user.IsAdmin)
                _Role = "Admin";
            //claims.Add(new Claim(ClaimTypes.NameIdentifier, user.UserName.ToString()));
            claims.Add(new Claim(ClaimTypes.Role, _Role));
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
