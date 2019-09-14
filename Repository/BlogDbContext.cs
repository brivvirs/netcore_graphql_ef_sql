using graphqlpractise.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace graphqlpractise.Repository
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext(DbContextOptions options)
: base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Post> Posts { get; set; }
    }
}
