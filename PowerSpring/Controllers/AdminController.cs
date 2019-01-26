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
using PowerSpring.Models.Logs;
using System.Security.Claims;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PowerSpring.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        // GET: /<controller>/ 
        private readonly IWebUserRepository _webUserRepository;
        private IUserManager _userManager;
        private IUserLogRepository _userLogRepository;
        public AdminController(IWebUserRepository webUserRepository, IUserManager userManager, IUserLogRepository userLogRepository)
        {
            _webUserRepository = webUserRepository;
            _userManager = userManager;
            _userLogRepository = userLogRepository;
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
            var UserToMute = _webUserRepository.GetWebUserById(id);
            string action = "mute";
            if (UserToMute.Muted == true)
            {
                action = "unmute";
            }
            UserToMute.Muted = !UserToMute.Muted;
            _userManager.Update(UserToMute);
            UserLog userLog = new UserLog { Time = DateTime.Now.ToString(), UserId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier)), Action = action, Value = id.ToString()};
            _userLogRepository.CreateLog(userLog);
            return RedirectToAction("Index","Admin",adminViewModel);
        }
    }
}
