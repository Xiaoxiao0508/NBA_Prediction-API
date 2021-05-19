using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NBA_API.Models;

namespace NBA_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerSelectionController : ControllerBase
    {
        private readonly NBA_DBContext _context;
        public PlayerSelectionController(NBA_DBContext context)
        {
            _context = context;

        }

        // GET: api/PlayerSelection
        // display all Players and teams
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlayerSelection>>> GetPlayerSelection()
        {
            return await _context.PlayerSelection.ToListAsync();
        }


        [HttpPut]
        public async Task<ActionResult<IEnumerable<PlayerSelections>>> PostPlayer([FromBody] PlayerSelections selections)

        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var UserId = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
            var PlayerCount = _context.PlayerSelection.Where(p => p.TeamName == selections.TeamName).CountAsync().Result;
            var players = _context.PlayerSelection.Where(p => p.TeamName == selections.TeamName && p.Id == UserId).ToList();
            var team = await _context.Team
                        .Where(t => t.TeamName == selections.TeamName)
                        .Where(t => t.Id == UserId)
                        .FirstAsync();

            foreach (var player in players)
            {

                _context.PlayerSelection.Remove(player);

                await _context.SaveChangesAsync();
            }
            //   var PlayerCount = 0;

            if (PlayerCount < 15)
            {
                try
                {
                    foreach (int i in selections.PlayerKeys)
                    {
                        var selection = new PlayerSelection(selections.TeamName, UserId, i);
                        _context.PlayerSelection.Add(selection);
                     

                        await _context.SaveChangesAsync();
                           PlayerCount+=1;
                    }
                    team.PlayerCount=PlayerCount;
                    await _context.SaveChangesAsync();

                }
                catch (DbUpdateException)
                {

                    return BadRequest("Unsuccessful");
                }
            }
            else
            {
                return BadRequest("Unsuccessful");
            }

            return Ok("Add players successfully");
        }
    }
}
