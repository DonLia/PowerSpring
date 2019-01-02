using PowerSpring.Models.Forum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerSpring.ViewModels
{
    public class ReplyViewModel
    {
        public Post post { get; set; }
        public Reply reply { get; set; }
    }
}
