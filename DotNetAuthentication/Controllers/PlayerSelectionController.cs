using DotNetAuthentication.DB;
using DotNetAuthentication.Models;
using JWT.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetAuthentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerSelectionController : ControllerBase
    {
        private readonly NBAContext _context;

        public PlayerSelectionController(NBAContext context)
        {
            _context = context;
        }

        // GET: api/PlayerSelection
        // display all Players and teams
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlayerSelection>>> GetPlayerSelection([FromHeader] string token)
        {
            try
            {
                //Validate Token
                var authorise = new Authorise();
                var userId = authorise.Validate(token);

                return await _context.PlayerSelection
                .Where(p => p.UserId == userId)
                .ToListAsync();
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

        [HttpPost]
        public async Task<bool> PostPlayer([FromHeader] string token,[FromBody] PlayerSelections selections)

        {

            try
            {
                //Validate Token
                var authorise = new Authorise();
                var userId = authorise.Validate(token);

                selections.UserId = userId;

                var PlayerCount = _context.PlayerSelection
                    .Where(p => p.TeamName == selections.TeamName)
                    .Where(u => u.UserId == userId)
                    .CountAsync()
                    .Result;

                if (PlayerCount < 15)
                {
                    try
                    {
                        foreach (int i in selections.PlayerKeys)
                        {
                            var selection = new PlayerSelection(selections.TeamName, i, userId);
                            _context.PlayerSelection.Add(selection);

                            await _context.SaveChangesAsync();
                        }

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

            catch (TokenExpiredException)
            {
                throw new ArgumentException("Token has expired");
            }
            catch (SignatureVerificationException)
            {
                throw new ArgumentException("Token has invalid signature");
            }
        }


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
