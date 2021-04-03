using System.IO;
using HowLongApi.Dependencies.Requests;
using HowLongApi.Dependencies.Responses;
using HowLongApi.Models;
using Microsoft.AspNetCore.Http;

namespace HowLongApi.Dependencies.Extensions
{
    public static class TeamExtensions
    {
        public static byte[] ConvertToBytes(this IFormFile image)
        {
            using var inputStream = image.OpenReadStream();
            using var stream = new MemoryStream();
            inputStream.CopyTo(stream);
            return stream.ToArray();
        }
        public static string SaveFileAndReturnPath(this IFormFile image)
        {
            var filePath = Path.Combine("./", "Images", image.FileName);

            using (FileStream fileStream = new(filePath, FileMode.Create))
            {
                image.CopyTo(fileStream);
            }

            return filePath;
        }

        public static TeamResponse ToResponse(this Team team)
        {
            return new TeamResponse
            {
                Id = team.Id,
                Name = team.Name,
                Sport = team.Sport.ToString(),
                Image = $"/api/Team/{team.Id}/image",
                Championship = team.Championship,
                ChampionshipDate = team.ChampionshipDate
            };
        }

        public static Team ToModel(this TeamRequest team)
        {
            return new Team
            {
                Id = team.Id,
                Name = team.Name,
                Sport = team.Sport,
                Image = team.Image.ConvertToBytes(),
                Championship = team.Championship,
                ChampionshipDate = team.ChampionshipDate
            };
        }
    }
}