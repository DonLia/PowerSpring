using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PowerSpring.Models.Forum
{
    public class BBSReply
    {
        [BindNever]        
        public int Id { get; set; }

        //[Required]
        [StringLength(2000, ErrorMessage = "Your comment is required")]
        public string Content { get; set; }

        public int ParentThreadId { get; set; }

        public int UserId { get; set; }
        public bool IsBlocked { get; set; }
        public bool IsDeleted { get; set; }
    }
}
