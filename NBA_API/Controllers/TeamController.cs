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
    [Route("[controller]")]
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
        // [HttpGet("getTeams")]
        // public async Task<ActionResult<IEnumerable<Team>>> GetTeams()
        // {
        //     var claimsIdentity = this.User.Identity as ClaimsIdentity;
        //     var UserId = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
        //     return await _context.Team.Where(p => p.Id == UserId).ToListAsync();
        // }

        [HttpPost("getteams")]
        public async Task<ActionResult<IEnumerable<DtrScores>>> GetTeams()
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var UserId = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
            var team = await _context.DtrScores.FromSqlRaw("DtrScores @p0", UserId)
                .ToListAsync();
            return Ok(team);

        }
        // [HttpGet("searchteams")]
        // public async Task<ActionResult<IEnumerable<Team>>> SearchTeams(string filter)
        // {
        //     var claimsIdentity = this.User.Identity as ClaimsIdentity;
        //     var UserId = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
        //     return await _context.Team.Where(p => EF.Functions.Like(p.TeamName, $"{filter}%") && p.Id == UserId).ToListAsync();
        // }
        // [HttpPost("GetDTRS")]
        // public async Task<ActionResult<IEnumerable<Team>>> GetDTRS()
        // {

        //     var claimsIdentity = this.User.Identity as ClaimsIdentity;
        //     var UserId = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;

        //     //Show Users teams
        //     var team = await _context.DtrScores.FromSqlRaw("DtrScores @p0", UserId)
        //         .ToListAsync();
        //     return Ok(team);
        // }
        [HttpPost("addteam")]
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
                Ok(false);

            }

            return Ok(true);
        }

        [HttpDelete("deleteteam")]
        public void DeleteTeam(string teamname)
        //  public void DeleteTeam(string teamname)
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var UserId = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
            var team = _context.Team.FirstOrDefault(p => p.Id == UserId && p.TeamName == teamname);
            //     if (team == null)
            //     {
            //         return NotFound("Team doesn't exist");
            //     }

            //     _context.Team.Remove(team);
            //    await  _context.SaveChangesAsync();

            //     return Ok("Delete Team Successfullly");
            try
            {
                _context.Team.Remove(team);
                _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new ArgumentException("team doesn't exist");
            }
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
