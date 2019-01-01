﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerSpring.Models
{
    public class News
    {
        public int Id { get; set; }
        public string NewsTitle { get; set; }
        public int UserId { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string Date { get; set; }
        public string Content { get; set; }

    }
}