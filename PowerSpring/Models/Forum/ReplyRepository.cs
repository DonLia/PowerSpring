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

        public IEnumerable<Reply> BBSReplies
        {
            get
            {
                return _appDbContext.Replies;
            }
        }

        public void AddReply(Reply bbsReply)
        {
            _appDbContext.Replies.Add(bbsReply);
            _appDbContext.SaveChanges(); ;
        }
    }
}
