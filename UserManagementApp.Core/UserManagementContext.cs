using Microsoft.EntityFrameworkCore;
using UserManagementApp.Core.Models;

namespace UserManagementApp.Core
{
    public class UserManagementContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public UserManagementContext(DbContextOptions<UserManagementContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.FirstName);
                entity.Property(e => e.LastName);
                entity.Property(e => e.Email);
                entity.Property(e => e.PhoneNumber);
            });
        }
    }
}