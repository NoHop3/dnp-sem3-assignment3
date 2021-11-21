using LoginExampleServer.Models;
using Microsoft.EntityFrameworkCore;

namespace LoginExampleServer.DataAccess
{
    public class AdultDBContext : DbContext
    {
        
        public DbSet<Adult> Adults { get; set; }
        public DbSet<User> Users { get; set; }
       // public DbSet<Job> Jobs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source = C:\Users\bird\RiderProjects\dnp-sem3-assignment3\Assignment3.db");
        }
    }
}