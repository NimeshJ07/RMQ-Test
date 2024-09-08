using Microsoft.EntityFrameworkCore;
using RMQ.Data;
using RMQ.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMQ.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly RMQDbContext _dbcontext;

        public Repository(RMQDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<T> CreateAsync(T entity)
        {
            await _dbcontext.Set<T>().AddAsync(entity);
            await _dbcontext.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            _dbcontext.Remove(entity);
            var row = await _dbcontext.SaveChangesAsync();
            return row > 0;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbcontext.Set<T>().ToListAsync();
        }

        public async Task<T> GetAsync(int entity)
        {
            return await _dbcontext.Set<T>().FindAsync(entity);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _dbcontext.Set<T>().Update(entity);
            await _dbcontext.SaveChangesAsync();
            return entity;
        }
    }
}
