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

        public IEnumerable<BBSReply> BBSReplies
        {
            get
            {
                return _appDbContext.BBSReplies;
            }
        }

        public void AddReply(BBSReply bbsReply)
        {
            _appDbContext.BBSReplies.Add(bbsReply);
            _appDbContext.SaveChanges(); ;
        }
    }
}
