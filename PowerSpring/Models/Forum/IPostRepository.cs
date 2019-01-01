using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerSpring.Models.Forum
{
    public interface IPostRepository
    {
        IEnumerable<Post> Posts { get; }

        void AddThread(Post bbsThread);
        Post GetThreadById(int bbsThreadId);
    }

}
