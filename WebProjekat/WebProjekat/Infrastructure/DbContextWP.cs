﻿using Microsoft.EntityFrameworkCore;
using WebProjekat.Models;

namespace WebProjekat.Infrastructure
{
    public class DbContextWP : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<UserImage> UserImages { get; set; }

        public DbContextWP(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DbContextWP).Assembly);
        }
    }
}
