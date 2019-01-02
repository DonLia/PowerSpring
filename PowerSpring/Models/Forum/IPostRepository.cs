using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerSpring.Models.Forum
{
    public interface IPostRepository
    {
        IEnumerable<Post> Posts { get; }

        void AddPost(Post post);
        Post GetPostById(int postId);
        void DeletePostById(int postId);
        void BlockPostById(int postId);
        void UbBlockPostById(int postId);
    }

}
