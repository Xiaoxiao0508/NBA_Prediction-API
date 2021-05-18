using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NBA_API.Models;

namespace NBA_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly NBA_DBContext _context;
        public TeamController(NBA_DBContext context)
        {
            _context = context;

        }

        // GET: api/Team
        // list all the team for a user
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Team>>> GetTeams()
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var UserId = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
            return await _context.Team.Where(p => p.Id == UserId).ToListAsync();
        }
         [HttpGet("searchteams")]
        public async Task<ActionResult<IEnumerable<Team>>> SearchTeams(string filter)
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var UserId = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
            return await _context.Team.Where(p =>EF.Functions.Like(p.TeamName, $"{filter}%")&&p.Id == UserId).ToListAsync();
        }
        [HttpGet("DTRS")]
        public async Task<ActionResult<IEnumerable<float>>> GetDTRS()
        {
            List<float> DTRList = new List<float>();
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var UserId = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
            var Teams = await _context.Team.Where(p => p.Id == UserId).ToListAsync();
            foreach (var t in Teams)
            {
                var DTR = _context.Database.ExecuteSqlRaw("DtrScore @p0,@p1", parameters: new[] { UserId, t.TeamName });
                DTRList.Add(DTR);
            }
            return DTRList;
        }

        [HttpPost]
        // Add new team to user's account
        public async Task<ActionResult<bool>> PostTeam([FromQuery] string teamname)
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var UserId = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
            // UserId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Team team = new Team(teamname, UserId, false, 0);
            try
            {
                _context.Team.Add(team);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                return BadRequest("Unsuccessful"); ;

            }

            return Ok("Add Team Successfully");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTeam(string teamname)
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var UserId = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
            var team = _context.Team.FirstOrDefault(p => p.Id == UserId && p.TeamName == teamname);
            if (team == null)
            {
                return NotFound();
            }

            _context.Team.Remove(team);
            await _context.SaveChangesAsync();

            return Ok("Delete Team Successfullly");
        }

        [HttpPut("setfavorites")]
        public void SetFavorites([FromBody] FavoriteTeams fav)
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var UserId = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
               
                foreach (var team in fav.TeamNames)
                {
                    var teamUpdate = _context.Team
                    .Where(t => t.TeamName == team)
                    .Where(t => t.Id == UserId)
                    .FirstOrDefault();

                    //if no team exists with that name skip team
                    if (teamUpdate == null) { continue; }

                    teamUpdate.isFav = fav.IsFav;
                    _context.SaveChangesAsync();
                }
            }



    }
    }
