using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PowerSpring.Models.Forum;

namespace PowerSpring.Controllers
{
    public class ReplyController : Controller
    {
        private readonly IReplyRepository _replyRepository;

        public ReplyController(IReplyRepository replyRepository)
        {
            _replyRepository = replyRepository;
        }

        public IActionResult NewReply(int bbsThreadId)
        {
            BBSThread bBSThread = new BBSThread();
            //bBSThread
            return View();
        }
    }
}