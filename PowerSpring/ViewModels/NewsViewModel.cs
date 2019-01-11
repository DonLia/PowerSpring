using PowerSpring.Models.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace PowerSpring.ViewModels
{
    public class NewsViewModel
    {
        public string Title { get; set; }
       
        public List<NewsInfo> NewsList { get; set; }



        public List<NewsInfo> newsInfos
        {
            get
            {
                List<NewsInfo> lst = new List<NewsInfo>();
                foreach(NewsInfo info in this.newsInfos)
                {
                    if (!info.IsDeleted) {
                        lst.Add(info);
                    }
                }

                return lst;
     
            }
        }

    }
}
