using System.Collections.Generic;
using System.Threading.Tasks;
using HowLongApi.Dependencies.Requests;
using HowLongApi.Dependencies.Responses;
using HowLongApi.Models;
using HowLongApi.Models.Enums;

namespace HowLongApi.Infrastructure.Interfaces
{
    public interface ITeamBusiness
    {
        Task<IEnumerable<TeamResponse>> GetBySport(Sport sport);
        Task<TeamResponse> GetById(int id);
        Task<TeamResponse> Create(TeamRequest team);
        Task<TeamResponse> Update(TeamRequest team);
        Task Delete(int id);
        Task<byte[]> Image (int id);
    }
}