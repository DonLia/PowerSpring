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
        public Post post { get; set; }
        public List<Reply> Replies { get; set; }
        public string Success { get; set; }
        public string Username { get; set; }
        public int Id { get; set; }
    }
}
