using Microsoft.EntityFrameworkCore;
using RMQ.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMQ.Data
{
    public class RMQDbContext : DbContext
    {
        public RMQDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> users { get; set; }
    }
}
