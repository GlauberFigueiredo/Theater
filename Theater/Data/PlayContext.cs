using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Theater.Data
{
    public class PlayContext : DbContext
    {
        public DbSet<Play> Plays { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Performance> Performances { get; set; }

        public PlayContext(DbContextOptions<PlayContext> options)
        : base(options)
        {
        }
    }
}
