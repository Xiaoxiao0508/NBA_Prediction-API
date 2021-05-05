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

        [HttpPost("addteam")]//lower case paramater

        //return TasK<IActionResult>
        public async Task<bool> PostTeam([FromHeader] string token, [FromBody] string teamName)
        {            
            //See all teams the current user has.
            try
            {   
                //Validate Token
                 var authorise = new Authorise();
                var userId = authorise.Validate(token);

                var team = new Team();
                team.TeamName = teamName;
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

            return true;//return ok Return OK(true)
        }

        //delete team 
        [HttpPost("deleteteam")]
        public async Task<string> DeleteTeam([FromHeader] string token, [FromBody] string teamName)
        {//add pagination

            //Delete Team
            try
            {    
                //don't validate tokens inside endpoint
                //Validate Token
                var authorise = new Authorise();
                var userId = authorise.Validate(token);
               

                var team = new Team();
                team.UserId = userId;
                team.TeamName = teamName;

                //check if Team exists 
                var isTeam = _context.Team
                    .Where(t => t.UserId == team.UserId)
                    .Where(t => t.TeamName == team.TeamName)
                    .FirstOrDefault().TeamName == team.TeamName;
                                    

                if(isTeam)
                {                    
                    //delete team
                    _context.RemoveRange(
                        _context.Team
                        .Where(t => t.UserId == team.UserId)
                        .Where(t => t.TeamName == team.TeamName)
                        .FirstOrDefault());
                    await _context.SaveChangesAsync();

                    //return which team has been deleted
                    return $"{team.TeamName} Deleted";
                }                
                return $"{team.TeamName} was not found";
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
        public async Task<ActionResult<IEnumerable<Team>>> GetTeams([FromHeader] string token)
        {//add pagination

            //See all teams the current user has.
            try
            {   
                
                // Validate Token
                 var authorise = new Authorise();
                var userId = authorise.Validate(token);

                var team = await _context.Team.Where(t => t.UserId == userId).ToListAsync();

                return team;

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
