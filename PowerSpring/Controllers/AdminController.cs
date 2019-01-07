using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using PowerSpring.Models;
using PowerSpring.Helper;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PowerSpring.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        // GET: /<controller>/ 
        private readonly IWebUserRepository _webUserRepository;
        private IUserManager _userManager;
        public AdminController(IWebUserRepository webUserRepository, IUserManager userManager) {
            _webUserRepository = webUserRepository;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View(_webUserRepository.GetAllWebUsers());
        }
        public IActionResult Mute(int id) {
            var User = _webUserRepository.GetWebUserById(id);
            User.Muted = !User.Muted;
            _userManager.Update(User);
            return RedirectToAction("Index", "Admin");
        }
    }
}
