using PowerSpring.Models.Forum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerSpring.ViewModels
{
    public class ReplyViewModel
    {
        public BBSThread bbsThread { get; set; }
        public BBSReply bbsReply { get; set; }
    }
}
