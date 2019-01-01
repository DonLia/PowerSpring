using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerSpring.Models.Forum
{
    public class PostRepository : IPostRepository
    {
        private readonly AppDbContext _appDbContext;

        public PostRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Post> Posts
        {
            get
            {
                return _appDbContext.Posts;
            }
        }

        public void AddThread(Post bbsThread)
        {
            _appDbContext.Posts.Add(bbsThread);
            _appDbContext.SaveChanges(); ;
        }

        public Post GetThreadById(int threadId)
        {
            return _appDbContext.Posts.FirstOrDefault(th => th.Id ==threadId);
        }
    }
}
