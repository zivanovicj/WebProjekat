using Microsoft.EntityFrameworkCore;
using UserAdminAPI.Models;

namespace UserAdminAPI.Infrastructure
{
    public class DbContextWP : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserImage> UserImages { get; set; }

        public DbContextWP(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DbContextWP).Assembly);
        }
    }
}
