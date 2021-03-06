﻿using System;
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

        public void AddPost(Post post)
        {
            _appDbContext.Posts.Add(post);
            _appDbContext.SaveChanges(); 
        }

        public void BlockPostById(int postId)
        {
            _appDbContext.Posts.FirstOrDefault(r => r.Id == postId).IsBlocked = true;
            _appDbContext.SaveChanges();
        }

        public void ChangePost(Post post, int postId)
        {
            _appDbContext.Posts.FirstOrDefault(r => r.Id == postId).Time = DateTime.Now.ToString();
            _appDbContext.Posts.FirstOrDefault(r => r.Id == postId).Content = post.Content;
            _appDbContext.Posts.FirstOrDefault(r => r.Id == postId).Title = post.Title;
            _appDbContext.SaveChanges();
        }

        public void DeletePostById(int postId)
        {
            _appDbContext.Posts.FirstOrDefault(r => r.Id == postId).IsDeleted = true;
            _appDbContext.SaveChanges();
        }

        public Post GetPostById(int postId)
        {
            return _appDbContext.Posts.FirstOrDefault(th => th.Id ==postId);
        }

        public void UbBlockPostById(int postId)
        {
            _appDbContext.Posts.FirstOrDefault(r => r.Id == postId).IsBlocked = false;
            _appDbContext.SaveChanges();
        }

        public void UnDeletePostById(int postId)
        {
            _appDbContext.Posts.FirstOrDefault(r => r.Id == postId).IsDeleted = false;
            _appDbContext.SaveChanges();
        }
    }
}
