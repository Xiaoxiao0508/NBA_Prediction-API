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
    [Route("[controller]")]
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
        // [HttpGet]
        // public async Task<ActionResult<IEnumerable<PlayerSelection>>> GetPlayerSelection()
        // {
        //     return await _context.PlayerSelection.ToListAsync();
        // }


        [HttpPut]
        public async Task<ActionResult<IEnumerable<PlayerSelections>>> UptatePlayerSelection([FromBody] PlayerSelections selections)

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

            // GET: api/PlayerSelection/5
            // display players from searching team name
            // [HttpGet("ViewPlayers")]
            // public async Task<ActionResult<PlayerSelection>> GetPlayerSelection([FromQuery] string searchstring)
            // {
            //     var DisplayData = await _context.PlayerSelection.Where(p => EF.Functions.Like(p.TeamName, $"{searchstring}%"))
            //            .OrderBy(p => p.TeamName)
            //            .ToListAsync();

            //     return Ok(new Response<List<PlayerSelection>>(DisplayData)); ;
            // }


            // DELETE: api/PlayerSelection/DeletePlayer
            // [HttpDelete("DeletePlayer")]
            // public async Task<bool> DeletePlayerSelection([FromBody] PlayerSelection playerSelection)
            // {
            //     var PlayerDeleted = _context.PlayerSelection.Where(p => p.TeamName == playerSelection.TeamName);

            //     try
            //     {
            //         _context.PlayerSelection.Remove(playerSelection);
            //         await _context.SaveChangesAsync();
            //     }
            //     catch (DbUpdateException)
            //     {
            //         return false;
            //     }

            //     return true;
            // }
        }
    }
