using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using LoginExampleServer.Data.Impl;
using LoginExampleServer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace LoginExampleServer.DataAccess
{
    public class AdultDBContext : DbContext
    {
        
        public DbSet<Adult> Adults { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Job> Jobs { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source = C:\Users\bird\RiderProjects\dnp-sem3-assignment3\Assignment3.db");
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.EnableDetailedErrors();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Adult>().HasKey(adult => new {adult.Id});
            modelBuilder.Entity<User>().HasKey(user => new {user.Id});
            modelBuilder.Entity<Job>().HasKey(job => new {job.Id});


        }
    }
}