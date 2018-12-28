﻿using System;
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
        public IEnumerable<BBSThread> BBSThreads => _appDbContext.BBSThreads;
    }
}
