using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PowerSpring.Models.Forum
{
    public class Reply
    {
        [BindNever]        
        public int Id { get; set; }

        //[Required]
        [StringLength(2000, ErrorMessage = "Your comment is required")]
        public string Content { get; set; }

        public int ParentId { get; set; }
        public String Time { get; set; }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public bool IsBlocked { get; set; }
        public bool IsDeleted { get; set; }
    }
}
