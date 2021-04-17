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

        // GET: api/PlayerSelection/5
        // display players from searching team name
        [HttpGet("ViewPlayers")]
        public async Task<ActionResult<PlayerSelection>> GetPlayerSelection([FromQuery] string searchstring)
        {
            var DisplayData = await _context.PlayerSelection.Where(p => EF.Functions.Like(p.TeamName, $"{searchstring}%"))
                   .OrderBy(p => p.TeamName)
                   .ToListAsync();

            return Ok(new Response<List<PlayerSelection>>(DisplayData)); ;
        }


        [HttpPost]
        public async Task<bool> PostPlayer([FromBody] PlayerSelection playerSelection)
        {
            var PlayerCount = _context.PlayerSelection.Where(p => p.TeamName == playerSelection.TeamName).CountAsync().Result;

            if (PlayerCount < 15)
            {
                try
                {
                    _context.PlayerSelection.Add(playerSelection);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {

                    return false;
                }
            }
            else
            {
                return false;
            }

            return true;
        }

        // DELETE: api/PlayerSelection/DeletePlayer
        [HttpDelete("DeletePlayer")]
        public async Task<bool> DeletePlayerSelection([FromBody] PlayerSelection playerSelection)
        {
            var PlayerDeleted = _context.PlayerSelection.Where(p => p.TeamName == playerSelection.TeamName);

            try
            {
                _context.PlayerSelection.Remove(playerSelection);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return false;
            }

            return true;
        }
    }
}
