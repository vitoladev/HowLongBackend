using System;
using HowLongApi.Models.Enums;
using Microsoft.AspNetCore.Http;

namespace HowLongApi.Dependencies.Requests
{
    public record TeamRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Sport Sport { get; set; }
        public IFormFile Image { get; set; }
        public string Championship { get; set; }
        public DateTime ChampionshipDate { get; set; }
    }
}
