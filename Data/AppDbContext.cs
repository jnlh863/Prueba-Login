using MealMasterAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MealMasterAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<UserProfile> UsersProfile { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(u => u.profile)
                .WithOne(up => up.user)
                .HasForeignKey<UserProfile>(up => up.id)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
