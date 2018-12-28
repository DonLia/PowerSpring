using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerSpring.Models.Forum
{
    public interface IThreadRepository
    {
        IEnumerable<BBSThread> BBSThreads { get; }
    }
}
