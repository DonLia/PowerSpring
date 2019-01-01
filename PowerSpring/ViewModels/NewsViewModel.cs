using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace PowerSpring.ViewModels
{
    public class NewsViewModel
    {
        public string Title { get; set; }
       
        public List<Models.News> NewsList { get; set; }
    }
}
