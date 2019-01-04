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
using Microsoft.AspNetCore.Authorization;


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


        //login action reviewed 1/3
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel loginViewModel, string ReturnUrl)
        {
            if (!ModelState.IsValid)
                return View(loginViewModel);

            var user = _userManager.Authenticate(loginViewModel.UserName, loginViewModel.Password);

            if (user == null)
            {
                ModelState.AddModelError("", "User name/password not found");
                return View(loginViewModel);
            }

            UpdateIdentity(user);

            if (ReturnUrl != null)
                return Redirect(ReturnUrl);

            return RedirectToAction("Index", "Home");
        }

        //logout reviewed 1/3
        public async Task<IActionResult> Logout()
        {
            //wait _signInManager.SignOutAsync();
            //await httpContext.SignOutAsync();
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult Update(String id)
        {
            UpdateViewModel updateViewModel = new UpdateViewModel();
            updateViewModel.UpdateInfo = id;
            switch (id) {
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
        public IActionResult Update(UpdateViewModel updateViewModel,String id)
        {
            if (updateViewModel.UpdateInfo == null)
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
            WebUser updateUser = new WebUser()
            {
                UserName = user.UserName,
                IsAdmin = user.IsAdmin,
                Email = user.Email,
                PasswordHash = user.PasswordHash,
                PasswordSalt = user.PasswordSalt,
                Phone = user.Phone,
                Id=user.Id,
            };
            switch (updateViewModel.UpdateInfo)
            {
                case "UserName":
                    updateUser.UserName = updateViewModel.UpdateInfoValue;
                    try
                    {
                        _userManager.Update(updateUser);

                    }
                    catch (Exception e)
                    {
                        ModelState.AddModelError("", e.Message);
                        return View(updateViewModel);

                    }
                    UpdateIdentity(updateUser);

                    break;
                case "Email":
                    updateUser.Email = updateViewModel.UpdateInfoValue;
                    _userManager.Update(updateUser);
                    break;
                case "Phone":
                    updateUser.Phone = updateViewModel.UpdateInfoValue;
                    _userManager.Update(updateUser);
                    break;
                case "Password":
                    if (updateViewModel.VerifyUpdateInfoValue == updateViewModel.UpdateInfoValue)
                    {
                        _userManager.Update(updateUser, updateViewModel.UpdateInfoValue);
                        UpdateIdentity(updateUser);
                    }

                    else
                    {
                        ModelState.AddModelError("", "Your new password does not match");
                        updateViewModel.VerifyUpdateInfoValue = null;
                        updateViewModel.UpdateInfoValue = null;
                        return View(updateViewModel);
                    }
                    break;
            }
            return RedirectToAction("Index", "Account");
        }

        [Authorize]
        public IActionResult Index()
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (id == null)
                return RedirectToAction("Login", "Account");
            var _user = _userManager.GetById(Convert.ToInt32(id));
            return View(_user);
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
        public IActionResult Register(LoginViewModel loginViewModel, string ReturnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }
            try
            {
                var NewUser = _userManager.Create(loginViewModel.UserName, loginViewModel.Password);
                if (NewUser != null)
                {
                    UpdateIdentity(NewUser);
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                return View(loginViewModel);
            }
            if (ReturnUrl != null)
                return Redirect(ReturnUrl);
            return RedirectToAction("RegisterSuccess", "Account");
        }

        private async void UpdateIdentity(WebUser user)
        {
            ClaimsIdentity identity = new ClaimsIdentity(this.GetUserClaims(user), CookieAuthenticationDefaults.AuthenticationScheme);
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
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


    }
}
