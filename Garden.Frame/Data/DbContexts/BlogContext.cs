using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore;
using MySQL.Data.EntityFrameworkCore;

namespace Garden.Frame.Data.DbContexts
{
    public class BlogContext : DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options) : base(options)
        {

        }

        public DbSet<Blog> blog { get; set; }
        public DbSet<User> User { get; set; }
    }

    public class Blog
    {
        public int Id { get; set; }
        public string Url { get; set; }
    }

    //[Table("User")]
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
