using PowerSpring.Models.Forum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerSpring.ViewModels
{
    public class ForumViewModel
    {
        public List<Post> Posts { get; set; }
        public List<Reply> Replies { get; set; }
    }
}
