using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PowerSpring.Models.Forum;
using PowerSpring.ViewModels;

namespace PowerSpring.Controllers
{
    public class ForumController : Controller
    {
        private readonly IThreadRepository _threadRepository;

        public ForumController(IThreadRepository threadRepository)
        {
            _threadRepository = threadRepository;
        }

        // [Authorize]
        public IActionResult Index()
        {
            var bBSThreads = _threadRepository.BBSThreads.OrderBy(t => t.Id);
            var threadViewModel = new ThreadViewModel()
            {
                BBSThreads = bBSThreads.ToList()
            };


            return View(threadViewModel);
        }

        public IActionResult NewPost()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index( BBSThread bbsThread)
        {
            if (ModelState.IsValid)
            {
                _threadRepository.AddThread(bbsThread);
                return RedirectToAction("PostComplete");
            }
            return View(bbsThread);
        }

        public IActionResult PostComplete()
        {
            return View();
        }


    }
}