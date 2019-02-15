using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PowerSpring.ViewModels
{
    public class UpdateViewModel
    {
        [Required]
        public string VerifyPassword { get; set; }
        public string UpdateInfo { get; set; }
        [Required]
        public string UpdateInfoValue { get; set; }
        public string VerifyUpdateInfoValue { get; set; }
        public string InputType { get; set; }

    }
}
