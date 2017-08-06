using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.Entity;

using System.Data;
using SPAStudy.Domain.Entities;

namespace SPAStudy.Domain.Context
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class BloggingContext : DbContext
    {
        public BloggingContext() : base("name = MyContext")
        {
        }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
    }
}