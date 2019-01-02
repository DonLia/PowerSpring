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
        private readonly IPostRepository _postRepository;

        public ForumController(IReplyRepository replyRepository, IPostRepository threadRepository)
        {
            _replyRepository = replyRepository;
            _postRepository = threadRepository;
        }

        [Authorize]
        public IActionResult Index()
        {
            var bBSThreads = _postRepository.Posts.OrderBy(t => t.Id);
            var bBSReplies = _replyRepository.Replies;
            ForumViewModel forumViewModel = new ForumViewModel
            {
                Posts = bBSThreads.ToList(),
                Replies = bBSReplies.ToList()
            };
            return View(forumViewModel);
        }
        //Get Input pages of Reply or Post
        public IActionResult NewPost()
        {
            return View();
        }
        public IActionResult NewReply()
        {
            return View();
        }

        //Get Reply or Post from User and update Database
        [HttpPost]
        public IActionResult Index(Post post)
        {
            if (ModelState.IsValid)
            {
                post.Time = DateTime.Now.ToString();
                post.UserId = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                _postRepository.AddPost(post);
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
                reply.Time = DateTime.Now.ToString();
                reply.UserId = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                _replyRepository.AddReply(reply);
                return RedirectToAction("PostComplete");
            }
            return View();
        }

        //Delete or Block Reply or Post

        public IActionResult DeleteReply(int id)
        {
            _replyRepository.DeleteReplyById(id);

            return RedirectToAction("DeleteComplete");
        }
        public IActionResult DeletePost(int id)
        {
            _postRepository.DeletePostById(id);

            return RedirectToAction("DeleteComplete");
        }
        public IActionResult BlockPost(int id)
        {
            _postRepository.BlockPostById(id);

            return RedirectToAction("BlockComplete");
        }
        public IActionResult UnBlockPost(int id)
        {
            _postRepository.UbBlockPostById(id);
            return RedirectToAction("BlockComplete");
        }
        

        //Successful pages
        public IActionResult PostComplete()
        {
            return View();
        }
        public IActionResult DeleteComplete()
        {
            return View();
        }
        public IActionResult BlockComplete()
        {
            return View();
        }
    }
}