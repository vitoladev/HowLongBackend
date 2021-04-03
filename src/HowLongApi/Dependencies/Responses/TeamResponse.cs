using System;
using HowLongApi.Dependencies;
using HowLongApi.Models.Enums;

namespace HowLongApi.Dependencies.Responses
{
    public record TeamResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Sport { get; set; }
        public string Image { get; set; }
        public string Championship { get; set; }
        public DateTime ChampionshipDate { get; set; }
        // public TeamResponse(int id, string name, string sport,
        //     string image, string championship,
        //     DateTime championshipDate)
        // {
        //     Id = id;
        //     Name = name;
        //     Sport = sport;
        //     Image = image;
        //     Championship = championship;
        //     ChampionshipDate = championshipDate;
        // }
    }
}