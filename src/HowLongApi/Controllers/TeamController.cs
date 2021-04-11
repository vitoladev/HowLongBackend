using System.Reflection.Emit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HowLongApi.Infrastructure.Interfaces;
using HowLongApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using HowLongApi.Models.Enums;
using HowLongApi.Businesses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using HowLongApi.Dependencies.Requests;
using HowLongApi.Dependencies.Responses;

namespace HowLongApi.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        public ITeamBusiness _teamBusiness;

        public TeamController(ITeamBusiness teamBusiness)
        {
            _teamBusiness = teamBusiness;
        }

        [HttpGet("sport/{sport}")]
        public async Task<ActionResult> GetBySport(Sport sport)
        {
            var team = await _teamBusiness.GetBySport(sport);
            return Ok(team);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            try
            {
                var team = await _teamBusiness.GetById(id);

                if (team is not TeamResponse)
                {
                    return BadRequest();
                }

                return Ok(team);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error on retrieving team from database");
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Create([FromForm] TeamRequest teamRequest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var team = await _teamBusiness.Create(teamRequest);
                    var uri = Url.Action("GetById", new { id = team.Id });
                    return Created(uri, team);
                }
                return BadRequest();
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error on trying to create team: {e}");
            }
        }

        [HttpGet("{id}/image")]
        public async Task<IActionResult> Image(int id)
        {
            var img = await _teamBusiness.Image(id);
            return File(img, "image/png");
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update([FromForm] TeamRequest team)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _teamBusiness.Update(team);
                    return Ok();
                }
                return BadRequest();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating person");
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _teamBusiness.Delete(id);
                    return NoContent();
                }
                return BadRequest();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating person");
            }
        }
    }
}
