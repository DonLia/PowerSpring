using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PowerSpring.Models.Forum
{
    public class Post
    {   [BindNever]
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required]
        [StringLength(2000, ErrorMessage = "Your comment is required")]
        public string Content { get; set; }

        public string ImageUrl { get; set; }
        public string Time { get; set; }
        public int GroupId { get; set; } 

        public int UserId { get; set; }
        public string UserName { get; set; }

        public bool IsBlocked { get; set; } 
        public bool IsDeleted { get; set; } 
    }
}
