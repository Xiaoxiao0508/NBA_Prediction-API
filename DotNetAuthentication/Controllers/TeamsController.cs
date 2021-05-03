using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DotNetAuthentication.DB;
using DotNetAuthentication.Models;
using JWT;
using JWT.Serializers;
using JWT.Algorithms;
using Newtonsoft.Json.Linq;
using JWT.Exceptions;
using System.Diagnostics;

namespace DotNetAuthentication.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class TeamsController : Controller
    {
        private readonly NBAContext _context;

        public TeamsController(NBAContext context)
        {
            _context = context;
        }

        [HttpPost("addteam")]
        public async Task<bool> PostTeam([FromHeader] string Token, [FromBody] string TeamName)
        {//add pagination

            //See all teams the current user has.
            try
            {   
                //Validate Token
                 var authorise = new Authorise();
                var userId = authorise.Validate(Token);

                var team = new Team();
                team.TeamName = TeamName;
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

            return true;
        }

        [HttpPost("getteams")]
        public async Task<ActionResult<IEnumerable<Team>>> GetTeams([FromHeader] string Token)
        {//add pagination

            //See all teams the current user has.
            try
            {   
                
                // Validate Token
                 var authorise = new Authorise();
                var userId = authorise.Validate(Token);

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
