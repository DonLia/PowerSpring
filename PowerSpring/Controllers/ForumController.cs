using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PowerSpring.Models.Forum;
using PowerSpring.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace PowerSpring.Controllers
{
    public class ForumController : Controller
    {
        private readonly IReplyRepository _replyRepository;
        private readonly IPostRepository _threadRepository;

        public ForumController(IReplyRepository replyRepository, IPostRepository threadRepository)
        {
            _replyRepository = replyRepository;
            _threadRepository = threadRepository;
        }

        [Authorize]
        public IActionResult Index()
        {
            var bBSThreads = _threadRepository.Posts.OrderBy(t => t.Id);
            var bBSReplies = _replyRepository.BBSReplies;
            ForumViewModel forumViewModel = new ForumViewModel
            {
                Posts = bBSThreads.ToList(),
                Replies = bBSReplies.ToList()
            };
            return View(forumViewModel);
        }

        public IActionResult NewPost()
        {
            return View();
        }
        public IActionResult NewReply()
        {
            return View();
            //BBSReply bBSReplie = new BBSReply();
            ////bBSReplie.ParentThreadId = id;
            //return View(bBSReplie);
            ////return RedirectToAction("View","Forum", new {id = bbsThreadId});
        }
        //public IActionResult Details(int bbsThreadId)
        //{
        //    var bBSThreads = _threadRepository.BBSThreads.OrderBy(t => t.Id);
        //    var bBSReplies = _replyRepository.BBSReplies;
        //    ForumViewModel forumViewModel = new ForumViewModel
        //    {
        //        BBSThreads = bBSThreads.ToList(),
        //        BBSReplies = bBSReplies.ToList()
        //    };


        //    foreach (var r in bBSReplies)
        //    {
        //        if (r.ParentThreadId == bbsThreadId)
        //        {
        //            forumViewModel.BBSReplies.Add(r);
        //        }
        //    }

        //    return View(forumViewModel);

        //}

        [HttpPost]
        public IActionResult Index(Post post)
        {
            if (ModelState.IsValid)
            {
                _threadRepository.AddThread(post);
                return RedirectToAction("PostComplete");
            }
            return View(post);
        }

        [HttpPost]
        public IActionResult NewReply(Reply reply, int id)
        {
            if (ModelState.IsValid)
            {
                reply.ParentId = id;
                reply.UserId = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                _replyRepository.AddReply(reply);
                return RedirectToAction("PostComplete");
            }
            return View();
        }
        public IActionResult Delete() {


            return View();
        }
        public IActionResult PostComplete()
        {
            return View();
        }


    }
}