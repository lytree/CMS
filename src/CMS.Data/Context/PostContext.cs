using CMS.Data.Models;
using FreeSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Data.Context
{
    public class PostContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Tag> Tags { get; set; }
        public DbSet<PostContext> Contexts { get; set; }

        protected override void OnModelCreating(ICodeFirst codeFirst)
        {

        }
    }
}
