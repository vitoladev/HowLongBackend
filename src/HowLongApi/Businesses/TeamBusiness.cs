using System.Reflection.Metadata;
using System.Net.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HowLongApi.Infrastructure;
using HowLongApi.Infrastructure.Interfaces;
using HowLongApi.Models;
using HowLongApi.Models.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using HowLongApi.Dependencies.Extensions;
using HowLongApi.Dependencies.Responses;
using HowLongApi.Dependencies.Requests;

namespace HowLongApi.Businesses
{
    public class TeamBusiness : ITeamBusiness
    {
        public ITeamRepository<Team> _repository;

        public TeamBusiness(ITeamRepository<Team> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TeamResponse>> GetBySport(Sport sport)
        {
            var teams = await _repository.ListBySportAsync(sport);
            return teams.Select(item => item.ToResponse());
        }

        public async Task<TeamResponse> GetById(int id)
        {
            var team = await _repository.GetByIdAsync(id);
            return team.ToResponse();
        }

        public async Task<TeamResponse> Create(TeamRequest team)
        {
            var model = team.ToModel();
            await _repository.AddAsync(model);

            return model.ToResponse();
        }

        public async Task<TeamResponse> Update(TeamRequest team)
        {
            var model = team.ToModel();
            await _repository.UpdateAsync(model);

            return model.ToResponse();
        }

        public async Task Delete(int id)
        {
            var team = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(team);
        }
        public async Task<byte[]> Image(int id) {
            var teams = await _repository.ListAsync();
            return teams
                .Where(l => l.Id == id)
                .Select(l => l.Image)
                .FirstOrDefault();
        }
    }
}