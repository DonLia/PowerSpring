using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerSpring.Models.News
{
    public interface INewsRepository
    {
        IEnumerable<NewsInfo> GetAllNews();
        NewsInfo GetNewsById(int NewsId);
        void AddNews(NewsInfo news);
    }
}
