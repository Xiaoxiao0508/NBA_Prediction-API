using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DotNetAuthentication.DB;
using DotNetAuthentication.Models;
using JWT.Exceptions;
using System.Diagnostics;
using System.Data;

namespace DotNetAuthentication.Controllers
{
    //add authorize [authorize]
    [ApiController]
    [Route("[controller]")]

    public class TeamsController : Controller
    {
        private readonly NBAContext _context;

        public TeamsController(NBAContext context)
        {
            _context = context;
        }

        //Add Team
        [HttpPost("addteam")]
        public async Task<ActionResult<bool>> PostTeam([FromBody] TeamUpdate input)
        {
            try
            {
                //Validate Token
                var authorise = new Authorise();
                var userId = authorise.Validate(input.Token);

                var isTeam = _context.Team
                    .Where(t => t.TeamName == input.TeamName)
                    .Where(u => u.UserId == userId)
                    .FirstOrDefault();

                //if Team already exists
                if (isTeam != null) { return Ok(false); }

                //Create team object
                var team = new Team();
                team.TeamName = input.TeamName;
                team.UserId = userId;

                //insert into database
                _context.Team.Add(team);
                await _context.SaveChangesAsync();

            }

            catch (TokenExpiredException)
            {
                throw new ArgumentException("Token has expired");
            }
            catch (SignatureVerificationException)
            {
                throw new ArgumentException("Token has invalid signature");
            }

            catch (DbUpdateException e)
            {
                Debug.WriteLine(e.Message);
                throw;

            }

            return Ok(true);//return ok Return OK(true)
        }

        //Delete team 
        [HttpPost("deleteteam")]
        public async Task<ActionResult<string>> DeleteTeam([FromBody] TeamUpdate input)
        {
            //Delete Team
            try
            {
                //don't validate tokens inside endpoint this needs to be changed
                //Validate Token
                var authorise = new Authorise();
                var userId = authorise.Validate(input.Token);


                var team = new Team();
                team.UserId = userId;
                team.TeamName = input.TeamName;

                //check if Team exists 
                var isTeam = _context.Team
                    .Where(t => t.UserId == team.UserId)
                    .Where(t => t.TeamName == team.TeamName)
                    .FirstOrDefault().TeamName == team.TeamName;

                //If Team Exists
                if (isTeam)
                {
                    //delete team
                    _context.RemoveRange(
                        _context.Team
                        .Where(t => t.UserId == team.UserId)
                        .Where(t => t.TeamName == team.TeamName)
                        .FirstOrDefault());
                    await _context.SaveChangesAsync();

                    //return which team has been deleted
                    return Ok($"{team.TeamName} Deleted");
                }
                return Ok($"{team.TeamName} was not found");
            }

            catch (TokenExpiredException)
            {
                throw new ArgumentException("Token has expired");
            }
            catch (SignatureVerificationException)
            {
                throw new ArgumentException("Token has invalid signature");
            }
            catch (DbUpdateException e)
            {
                Debug.WriteLine(e.Message);
                throw;
            }
        }


        [HttpPost("getteams")]
        public async Task<ActionResult<IEnumerable<Team>>> GetTeams([FromBody] string token)
        {
            try
            {
                // Validate Token
                var authorise = new Authorise();
                var userId = authorise.Validate(token);

                //Show Users teams
                var team = await _context.DtrScores.FromSqlRaw("DtrScores @p0", userId).ToListAsync();
                return Ok(team);

            }
            catch (TokenExpiredException)
            {
                throw new ArgumentException("Token has expired");
            }
            catch (SignatureVerificationException)
            {
                throw new ArgumentException("Token has invalid signature");
            }

        }

        [HttpPost("searchteams")]
        public async Task<ActionResult<IEnumerable<Team>>> SearchTeams([FromQuery] string filter, [FromBody] string token)
        {
            try
            {
                // Validate Token
                var authorise = new Authorise();
                var userId = authorise.Validate(token);

                //Show Users teams
                var team = await _context.DtrScores
                    .FromSqlRaw("DtrScoresSearch @p0, @p1", userId, filter)           
                    .ToListAsync();
                
                return Ok(team);

            }
            catch (TokenExpiredException)
            {
                throw new ArgumentException("Token has expired");
            }
            catch (SignatureVerificationException)
            {
                throw new ArgumentException("Token has invalid signature");
            }

        }


    }
}
