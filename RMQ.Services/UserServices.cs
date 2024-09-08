using RMQ.IRepository;
using RMQ.IServices;
using RMQ.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMQ.Services
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepository _repository;
        public UserServices(IUserRepository repository)
        {
            _repository = repository;
        }
        public async Task<User> CreateUserAsync(User user)
        {
            return await _repository.CreateAsync(user);
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var olduser = await _repository.GetAsync(id);
            if (olduser != null)
            {
                return await _repository.DeleteAsync(olduser);
            }
            return false;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _repository.GetAsync(id);
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<User> UpdateUserAsync(int id, User users)
        {
            var olduser = await _repository.GetAsync(id);

            if (olduser != null)
            {
                olduser.UserName = users.UserName;
                olduser.Password = users.Password;

                await _repository.UpdateAsync(olduser);
            }

            return olduser;
        }
    }
}
