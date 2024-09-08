using RMQ.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMQ.IServices
{
    public interface IUserServices
    {
        public Task<IEnumerable<User>> GetUsersAsync();
        public Task<User> GetUserByIdAsync(int id);
        public Task<User> CreateUserAsync(User user);
        public Task<User> UpdateUserAsync(int id, User users);
        public Task<bool> DeleteUserAsync(int id);
    }
}
