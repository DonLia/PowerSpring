using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerSpring.Models.Forum
{
    public class ThreadRepository : IThreadRepository
    {
        private readonly AppDbContext _appDbContext;

        public ThreadRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<BBSThread> BBSThreads
        {
            get
            {
                return _appDbContext.BBSThreads;
            }
        }

        public void AddThread(BBSThread bbsThread)
        {
            _appDbContext.BBSThreads.Add(bbsThread);
            _appDbContext.SaveChanges(); ;
        }
    }
}
