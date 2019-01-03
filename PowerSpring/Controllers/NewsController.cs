using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PowerSpring.Models;
using PowerSpring.Models.News;
using PowerSpring.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace News.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsRepository _newsRepository;
        public NewsController(INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;

        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            var news = _newsRepository.GetAllNews().OrderBy(p => p.NewsTitle);
            var newsViewModel = new NewsViewModel()
            {
                Title = "Welcome to NewsPage",
                NewsList = news.ToList()
            };
            return View(newsViewModel);
        }
        public IActionResult Details(int id)
        {
            var news = _newsRepository.GetNewsById(id);
            if (news == null)
                return NotFound();

            return View(news);
        }

        //Open submittion page
        public IActionResult NewsInPut()
        {
            return View();
        }
        //Process Input 
        [HttpPost]
        public IActionResult ProcessInPut(NewsInPut newsInPut)
        {
            if (ModelState.IsValid)
            {
                newsInPut.UserId = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                NewsInfo news = new NewsInfo();

                news.NewsTitle = newsInPut.NewsTitle;
                news.Date = System.DateTime.Today.ToShortDateString();
                news.Content = newsInPut.Content;

                _newsRepository.AddNews(news);
                
                return RedirectToAction("NewsInputComplete");
            }
            return View();
        }

        public IActionResult NewsInputComplete()
        {
            return View();
        }
        public IActionResult BlockNewsById(int id)
        {
            _newsRepository.BlockNewsById(id);

            return RedirectToAction("DeleteComplete");
        }
        public IActionResult DeleteComplete()
        {
            return View();
        }
    }
}
