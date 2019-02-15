using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using PowerSpring.Models;
using PowerSpring.Helper;
using PowerSpring.ViewModels;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PowerSpring.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        // GET: /<controller>/ 
        private readonly IWebUserRepository _webUserRepository;
        private IUserManager _userManager;
        public AdminController(IWebUserRepository webUserRepository, IUserManager userManager)
        {
            _webUserRepository = webUserRepository;
            _userManager = userManager;
        }
        
        public IActionResult Index(AdminViewModel adminViewModel)
        {
            if (adminViewModel.searchName == null)
            {
                adminViewModel.webUsers = _webUserRepository.GetAllWebUsers();
                return View(adminViewModel);
            }
            
            adminViewModel.webUsers = Search(adminViewModel.searchName);
            return View(adminViewModel);
        }

        private IEnumerable<WebUser> Search(string searchName)
        {
            IEnumerable<WebUser> suspects = _webUserRepository.GetAllWebUsers().Where(p=>p.UserName.Contains(searchName));
            return (suspects);
        }
        public IActionResult Mute(int id, string searchName)
        {
            AdminViewModel adminViewModel = new AdminViewModel { searchName=searchName };
            var User = _webUserRepository.GetWebUserById(id);
            User.Muted = !User.Muted;
            _userManager.Update(User);
            return RedirectToAction("Index","Admin",adminViewModel);
        }
    }
}
