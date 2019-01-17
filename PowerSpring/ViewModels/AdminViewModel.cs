using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PowerSpring.Models;

namespace PowerSpring.ViewModels
{
    public class AdminViewModel
    {
        public IEnumerable<WebUser> webUsers { get; set; }
        public string searchName { get; set; }
    }
}
