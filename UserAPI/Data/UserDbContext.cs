﻿using Microsoft.EntityFrameworkCore;
using UserAPI.Data.Configurations;
using UserAPI.Models;

namespace UserAPI.Data
{
    public class UserDbContext : DbContext
    {
        public DbSet<User> Users { get; private set; } = null!;

        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());

            DataSeeder.Seed(modelBuilder);
        }
    }
}
