using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Internal;
using UserManagementApp.Core.Models;
using UserManagementApp.Core.Repositories;

namespace UserManagementApp.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();

            return users.OrderByDescending(u => u.Id);
        }

        public async Task<User> GetByAsync(int id)
        {
            return await _userRepository.GetByAsync(id);
        }
        
        public async Task<User> CreateAsync(UserModelDto userModel)
        {
            var user = new User
            {
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                Email = userModel.Email,
                PhoneNumber = userModel.PhoneNumber
            };
            
            return await _userRepository.InsertAsync(user);
        }
        
        public async Task<User> UpdateAsync(int id, UserModelDto userModelDto)
        {
            var user = new User()
            {
                Id = id,
                FirstName = userModelDto.FirstName,
                LastName = userModelDto.LastName,
                Email = userModelDto.Email,
                PhoneNumber = userModelDto.PhoneNumber
            };
            
            return await _userRepository.UpdateAsync(user);
        }
        
        public async Task<bool> DeleteAsync(int id)
        {
            return await _userRepository.DeleteByAsync(id);
        }
    }
}