using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace UserManagementApp.Core
{
    public class UsersContextFactory : IDesignTimeDbContextFactory<UserManagementContext>
    {
        public UserManagementContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<UserManagementContext>();
            //TODO: looks ugly, db config string should be injected by config
            optionsBuilder.UseSqlite("Data Source=../user-management.db");

            return new UserManagementContext(optionsBuilder.Options);
        }
    }
}