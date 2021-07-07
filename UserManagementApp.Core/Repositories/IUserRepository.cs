using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagementApp.Core.Models;

namespace UserManagementApp.Core.Repositories
{
    public interface IUserRepository
    {
        public Task<IEnumerable<User>> GetAllAsync();
        public Task<User> GetByAsync(int id);
        public Task<User> UpdateAsync(User user);
        public Task<User> InsertAsync(User user);
        public Task<bool> DeleteByAsync(int id);
    }
}