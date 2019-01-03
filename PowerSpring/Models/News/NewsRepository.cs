using PowerSpring.Models.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace PowerSpring.Models.News
{
    public class NewsRepository:INewsRepository
    {
        private readonly AppDbContext _appDbContext;
        public NewsRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<NewsInfo> GetAllNews()
        {
            return _appDbContext.News;
        }

        public NewsInfo GetNewsById(int NewsId)
        {
            return _appDbContext.News.FirstOrDefault(p => p.Id == NewsId);

        }
        public void AddNews(NewsInfo news)
        {
            _appDbContext.News.Add(news);
            _appDbContext.SaveChanges();
        }

        public void DeleteNews(News news)
        {
            _appDbContext.News.Remove(news);
            _appDbContext.SaveChanges();
        }
        public void BlockNewsById(int Id)
        {
            _appDbContext.News.FirstOrDefault(r => r.Id == Id).IsDeleted = true;
            _appDbContext.SaveChanges();
        }

    }
}
