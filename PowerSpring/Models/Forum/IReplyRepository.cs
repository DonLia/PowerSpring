using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerSpring.Models.Forum
{
    public interface IReplyRepository
    {
        IEnumerable<Reply> Replies { get; }
        void AddReply(Reply reply);
        Reply GetReplyById(int replyId);
        List<Reply> GetRepliesByParentId(int parentId);
        void DeleteReplyById(int replyId);
        void UnDeleteReplyById(int replyId);
    }
}
