using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PowerSpring.Models;
using PowerSpring.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace News.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsRepository _newRepository;
        public NewsController(INewsRepository newsRepository)
        {
            _newRepository = newsRepository;

        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            var news = _newRepository.GetAllNews().OrderBy(p => p.NewsTitle);
            var newsViewModel = new NewsViewModel()
            {
                Title = "Welcome to NewsPage",
                NewsList = news.ToList()
            };
            return View(newsViewModel);
        }
    }
}
