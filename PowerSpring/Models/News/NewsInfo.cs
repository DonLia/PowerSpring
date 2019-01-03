using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PowerSpring.Models.News
{
    public class NewsInfo
    {
        public int Id { get; set; }

        public string NewsTitle { get; set; }

        public int UserId { get; set; }
        public string ImageThumbnailUrl { get; set; }


        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }

        public string Date { get; set; }

        public string Content { get; set; }
        public bool IsBlocked { get; set; }
        public bool IsDeleted { get; set; }

    }
}
