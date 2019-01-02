﻿
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PowerSpring.Models
{
    public class NewsInPut
    {
        [BindNever]
        public int Id { get; set; }


        [Required]
        [StringLength(100, ErrorMessage = "Title is required")]
        public string NewsTitle { get; set; }
        public int UserId { get; set; }
        [Required]
        [StringLength(2000, ErrorMessage = "Your comment is required")]
        public string Content { get; set; }

        public string Date { get; set; }


        //public List<BBSReply> Replies { get; set; }


        public bool IsBlocked { get; set; }
        public bool IsDeleted { get; set; }
    }
}
