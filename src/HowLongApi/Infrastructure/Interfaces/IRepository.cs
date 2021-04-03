using HowLongApi.Dependencies;
using HowLongApi.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HowLongApi.Infrastructure.Interfaces
{
    public interface IRepository<T> where T : BaseModel
    {
        Task<T> GetByIdAsync(int id);
        Task<List<T>> ListAsync();
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
