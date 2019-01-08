using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PowerSpring.ViewModels
{
    public class UpdateViewModel
    {
        public string UpdateInfo { get; set; }
        
        [Display(Name = "New UserName")]
        public string UserName { get; set; }
        
        [Display(Name = "Verify Your Password")]
        public string VerifyPassword { get; set; }
        
        [Display(Name = "New Email")]
        public string Email { get; set; }
        
        [Display(Name = "New Phone")]
        public string Phone { get; set; }
        
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }
        
        [Display(Name = "Repeat New Password")]
        public string VerifyNewPassword { get; set; }
    }
}
