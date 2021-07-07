using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserManagementApp.Core.Models;

namespace UserManagementApp.Core.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManagementContext _userManagementContext;
        
        public UserRepository(UserManagementContext userManagementContext)
        {
            _userManagementContext = userManagementContext ?? throw new ArgumentException(nameof(userManagementContext));
        }
        
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _userManagementContext.Users.AsNoTracking().ToListAsync();
        }

        public async Task<User> GetByAsync(int id)
        {
            return await _userManagementContext.Users.SingleOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> InsertAsync(User user)
        {
            await _userManagementContext.Users.AddAsync(user);
            await _userManagementContext.SaveChangesAsync();

            return user;
        }
        
        public async Task<User> UpdateAsync(User user)
        {
            var existedUser = await _userManagementContext.Users
                .SingleOrDefaultAsync(u => u.Id == user.Id);

            if (existedUser == null) return null;

            existedUser.FirstName = user.FirstName;
            existedUser.LastName = user.LastName;
            existedUser.Email = user.Email;
            existedUser.PhoneNumber = user.PhoneNumber;

            await _userManagementContext.SaveChangesAsync();

            return existedUser;
        }
        
        public async Task<bool> DeleteByAsync(int id)
        {
            var existedUser = await _userManagementContext.Users
                .SingleOrDefaultAsync(u => u.Id == id);

            if (existedUser == null) return false;

            _userManagementContext.Users.Remove(existedUser);

            return await _userManagementContext.SaveChangesAsync() > 0;
        }
    }
}