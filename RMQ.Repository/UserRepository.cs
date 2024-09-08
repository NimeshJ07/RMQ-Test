using RMQ.Data;
using RMQ.IRepository;
using RMQ.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMQ.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(RMQDbContext dbcontext) : base(dbcontext)
        {
        }
    }
}
