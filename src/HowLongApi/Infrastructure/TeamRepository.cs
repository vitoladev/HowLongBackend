using HowLongApi.Infrastructure.Data;
using HowLongApi.Infrastructure.Interfaces;
using HowLongApi.Models;
using HowLongApi.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HowLongApi.Infrastructure
{
    public class TeamRepository<T>: ITeamRepository<T> where T : Team
    {
        private readonly ApiContext _dbContext;
        public TeamRepository(ApiContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<T> GetByIdAsync(int id) => _dbContext.Set<T>().SingleOrDefaultAsync(e => e.Id == id);

        public Task<List<T>> ListAsync() => _dbContext.Set<T>().ToListAsync();

        public Task<List<T>> ListBySportAsync(Sport sport)
        {
            return _dbContext.Set<T>().Where(t => t.Sport == sport).ToListAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
