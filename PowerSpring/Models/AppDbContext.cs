using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PowerSpring.Models.Forum;
using PowerSpring.Models.News;
using PowerSpring.Models.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PowerSpring.Models.Logs;

namespace PowerSpring.Models
{
   // public class AppDbContext: IdentityDbContext<IdentityUser>
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<WebUser> WebUsers { get; set; }
        public DbSet<Pie> Pies { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Reply> Replies { get; set; }
        public DbSet<NewsInfo> News { get; set; }
        public DbSet<UserLog> Logs { get; set; }
        //public DbSet<HomePage> HomePages { get; set; }
    }
}
