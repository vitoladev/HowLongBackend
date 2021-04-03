using HowLongApi.Dependencies;
using HowLongApi.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HowLongApi.Infrastructure.Interfaces
{
    public interface ITeamRepository<T> : IRepository<T> where T : BaseModel
    {
        Task<List<T>> ListBySportAsync(Sport sport);
    }
}