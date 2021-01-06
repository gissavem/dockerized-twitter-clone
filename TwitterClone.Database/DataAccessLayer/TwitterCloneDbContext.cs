using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TwitterClone.Database.DataAccessLayer
{
    public class TwitterCloneDbContext : DbContext
    {
        public TwitterCloneDbContext(DbContextOptions<TwitterCloneDbContext> options) : base(options)
        {
        }
        public DbSet<Post> Posts { get; set; }
    }

}
