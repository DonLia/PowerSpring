using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerSpring.Models
{
    public interface INewsRepository
    {
        IEnumerable<News> GetAllNews();
        News GetNewsById(int NewsId);
        void AddNews(News news);
    }
}
