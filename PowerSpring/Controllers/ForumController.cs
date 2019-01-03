﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PowerSpring.Models.Forum;
using PowerSpring.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using PowerSpring.Models;

namespace PowerSpring.Controllers
{
    public class ForumController : Controller
    {
        private readonly IReplyRepository _replyRepository;
        private readonly IPostRepository _postRepository;
        //private readonly IWebUserRepository _WebuserRepository;

        public ForumController(IReplyRepository replyRepository, IPostRepository threadRepository)
        {
            _replyRepository = replyRepository;
            _postRepository = threadRepository;
            //_WebuserRepository = WebuserRepository;
        }

        [Authorize]
        //Display forum Index page and Details Page
        public IActionResult Index()
        {
            var post = _postRepository.Posts.OrderBy(t => t.Id);
            ForumViewModel forumViewModel = new ForumViewModel
            {
                Posts = post.ToList()

            };
            return View(forumViewModel);
        }

        public IActionResult Details(int id)
        {
            var forumViewModel = new ForumViewModel()
            {
                post = _postRepository.GetPostById(id),
                Replies = _replyRepository.GetRepliesByParentId(id)
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
        public IActionResult NewPost(Post post)
        {
            if (ModelState.IsValid)
            {
                post.Time = DateTime.Now.ToString();
                post.UserId = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                _postRepository.AddPost(post);
                return RedirectToAction("Complete",new { act="Thanks for your post!"});
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
                reply.UserName = User.Identity.Name;
                _replyRepository.AddReply(reply);
                return RedirectToAction("Details", new { id });
            }
            return View();
        }

        //Delete or Block Reply or Post
        public IActionResult ForumActions(int id, string act)
        {
            string message = "";
            switch (act)
            {
                case "DeletePost":
                    _postRepository.DeletePostById(id);
                    message = "Post ID: " + id.ToString() + " has been successfully DELETED";
                    break;
                case "UnDeletePost":
                    _postRepository.UnDeletePostById(id);
                    message = "Post ID: " + id.ToString() + " has been successfully recovered.";
                    break;

                case "BlockPost":
                    _postRepository.BlockPostById(id);
                    message = "Post ID: " + id.ToString() + " has been successfully BLOCKED, no replies will be accepted.";
                    break;
                case "UnBlockPost":
                    _postRepository.UbBlockPostById(id);
                    message = "Post ID: " + id.ToString() + " has been successfully unBlocked.";
                    break;

                case "DeleteReply":
                    _replyRepository.DeleteReplyById(id);
                    message = "Reply ID: " + id.ToString() + " has been successfully DELETED";
                    break;
                case "UnDeleteReply":
                    _replyRepository.UnDeleteReplyById(id);
                    message = "Reply ID: " + id.ToString() + " has been successfully DELETED";
                    return RedirectToAction("Details", new { id });
                default:
                    message = "Nothing happened";
                    break;

            }
            return RedirectToAction("Complete", "Forum", new { act = message });
        }

        //public IActionResult DeleteReply(int id)
        //{
        //    _replyRepository.DeleteReplyById(id);

        //    return RedirectToAction("DeleteComplete");
        //}
        //public IActionResult DeletePost(int id)
        //{
        //    _postRepository.DeletePostById(id);

        //    return RedirectToAction("DeleteComplete");
        //}
        //public IActionResult UnDeletePost(int id)
        //{
        //    _postRepository.UnDeletePostById(id);

        //    return RedirectToAction("DeleteComplete");
        //}
        //public IActionResult BlockPost(int id)
        //{
        //    _postRepository.BlockPostById(id);

        //    return RedirectToAction("BlockComplete");
        //}
        //public IActionResult UnBlockPost(int id)
        //{
        //    _postRepository.UbBlockPostById(id);
        //    return RedirectToAction("BlockComplete");
        //}


        //Successful pages
        public IActionResult Complete(string act)
        {
            ViewBag.message = act;
            return View();
        }
        //public IActionResult ReplyComplete(int id)
        //{
        //    var forumViewModel = new ForumViewModel
        //    {
        //        Id = id
        //    };
        //    return View();
        //}
        //public IActionResult DeleteComplete()
        //{
        //    return View();
        //}
        //public IActionResult BlockComplete()
        //{
        //    return View();
        //}
    }
}