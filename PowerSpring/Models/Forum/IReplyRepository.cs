﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerSpring.Models.Forum
{
    public interface IReplyRepository
    {
        IEnumerable<BBSReply> BBSReplies { get; }
        void AddReply(BBSReply bbsReply);
    }
}
