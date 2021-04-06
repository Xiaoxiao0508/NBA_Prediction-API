using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;
using WebApi_DB;

namespace WebApi.Controllers
{
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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Team>>> GetTeams()
        {
            return await _context.Team.ToListAsync();
        }

        // GET: api/Team/5
        [HttpGet("{TeamName}")]
        public async Task<ActionResult<Team>> GetTeam(string teamname)
        {
            var team = await _context.Team.FindAsync(teamname);

            if (team == null)
            {
                return NotFound();
            }

            return team;
        }

        // PUT: api/Team/5
       
        // [HttpPut("{id}")]
        // public async Task<IActionResult> PutTeam(int id, Team team)
        // {
        //     if (id != team.Id)
        //     {
        //         return BadRequest();
        //     }

        //     _context.Entry(team).State = EntityState.Modified;

        //     try
        //     {
        //         await _context.SaveChangesAsync();
        //     }
        //     catch (DbUpdateConcurrencyException)
        //     {
        //         if (!TeamExists(id))
        //         {
        //             return NotFound();
        //         }
        //         else
        //         {
        //             throw;
        //         }
        //     }

        //     return NoContent();
        // }

        // // POST: api/Team
        // // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // [HttpPost]
        // public async Task<ActionResult<Team>> PostTeam(Team team)
        // {
        //     _context.Teams.Add(team);
        //     await _context.SaveChangesAsync();

        //     return CreatedAtAction("GetTeam", new { id = team.Id }, team);
        // }

        // // DELETE: api/Team/5
        // [HttpDelete("{id}")]
        // public async Task<IActionResult> DeleteTeam(int id)
        // {
        //     var team = await _context.Teams.FindAsync(id);
        //     if (team == null)
        //     {
        //         return NotFound();
        //     }

        //     _context.Teams.Remove(team);
        //     await _context.SaveChangesAsync();

        //     return NoContent();
        // }

        // private bool TeamExists(int id)
        // {
        //     return _context.Teams.Any(e => e.Id == id);
        // }
    }
}
