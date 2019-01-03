using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerSpring.Models.Forum
{
    public class ReplyRepository : IReplyRepository
    {
        private readonly AppDbContext _appDbContext;

        public ReplyRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Reply> Replies
        {
            get
            {
                return _appDbContext.Replies;
            }
        }

        public List<Reply> GetRepliesByParentId(int parentId)
        {
            List<Reply> replyList = new List<Reply>();
            
            
            foreach(var reply in _appDbContext.Replies)
            {
                if (reply.ParentId == parentId)
                {
                    replyList.Add(reply);
                }
            }

            return replyList;

        }

        public Reply GetReplyById(int replyId)
        {
            return _appDbContext.Replies.FirstOrDefault(r => r.Id == replyId);
        }
        public void AddReply(Reply reply)
        {
            _appDbContext.Replies.Add(reply);
            _appDbContext.SaveChanges();
        }

        public void DeleteReplyById(int replyId)
        {
            _appDbContext.Replies.FirstOrDefault(r => r.Id == replyId).IsDeleted = true;
            _appDbContext.SaveChanges();
        }


        public void UnDeleteReplyById(int replyId)
        {
            _appDbContext.Replies.FirstOrDefault(r => r.Id == replyId).IsDeleted = false;
            _appDbContext.SaveChanges();
        }
    }
}

