using PowerSpring.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerSpring.Models
{
    public class NewsRepository:INewsRepository
    {
        private readonly AppDbContext _appDbContext;
        public NewsRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<News> GetAllNews()
        {
            return _appDbContext.News;
        }

        public News GetNewsById(int NewsId)
        {
            return _appDbContext.News.FirstOrDefault(p => p.Id == NewsId);

        }
        public void AddNews(News news)
        {
            _appDbContext.News.Add(news);
            _appDbContext.SaveChanges();
        }

    }
}
