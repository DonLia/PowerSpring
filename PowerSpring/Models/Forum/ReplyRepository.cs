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

        public Reply GetReplyById(int replyId)
        {
            return _appDbContext.Replies.FirstOrDefault(r => r.Id == replyId);
        }
    }
}
