using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagementApp.Core.Models;

namespace UserManagementApp.Core.Services
{
    public interface IUserService
    {
        public Task<IEnumerable<User>> GetAllAsync();

        public Task<User> GetByAsync(int id);
        
        public Task<User> CreateAsync(UserModelDto user);

        public Task<User> UpdateAsync(int id, UserModelDto user);
        
        public Task<bool> DeleteAsync(int id);

    }
}