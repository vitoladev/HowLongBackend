using System;
using HowLongApi.Dependencies;
using HowLongApi.Models.Enums;

namespace HowLongApi.Models
{
    public class Team : BaseModel
    {
        public string Name { get; set; }
        public Sport Sport { get; set; }
        public byte[] Image { get; set; }
        public string Championship { get; set; }
        public DateTime ChampionshipDate { get; set; }
    }
}
