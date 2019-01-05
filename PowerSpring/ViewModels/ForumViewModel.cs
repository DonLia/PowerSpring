using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PowerSpring.Models.Forum;
using PowerSpring.ViewModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;


namespace PowerSpring.ViewModels
{
    public class ForumViewModel
    {

        public List<Post> Posts { get; set; }
        public Post post { get; set; }
        public IFormFile Image { get; set; }
        public List<Reply> Replies { get; set; }
        public string Success { get; set; }
        public string Username { get; set; }
        public int Id { get; set; }
        public int CurrentUserId { get; set; }
    }
}
