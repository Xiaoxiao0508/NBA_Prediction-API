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
    public class PlayerController : ControllerBase
    {
        private readonly NBA_DBContext _context;

        public PlayerController(NBA_DBContext context)
        {
            _context = context;
        }

        // GET: api/Player
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Player>>> GetallPlayers([FromQuery] PaginationFilter filter)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var pagedData = await _context.allPlayers
            .OrderBy(p => p.FIRSTNAME)
            .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
            .Take(validFilter.PageSize)
            .ToListAsync();

            var totalRecords = await _context.allPlayers.CountAsync();
            var pagesCount = (decimal)totalRecords / (decimal)filter.PageSize;

            if((pagesCount % 1) != 0){
              pagesCount = Decimal.ToInt32(pagesCount) ;
              pagesCount += 1;
            }


            // return Ok(new PageResponse<List<Player>>(pagedData, validFilter.PageNumber, validFilter.PageSize));
            return Ok(new Response<List<Player>>(pagedData, Decimal.ToInt32(pagesCount)));

        }


        // GET: api/Player
        [HttpGet("{search}")]

        public async Task<ActionResult<IEnumerable<Player>>> GetPlayer([FromQuery] string searchstring)
        {

            var searchResult = await _context.allPlayers.Where(p => EF.Functions.Like(p.FIRSTNAME, $"{searchstring}%") || EF.Functions.Like(p.Lastname, $"{searchstring}%") || EF.Functions.Like(p.FIRSTNAME + " " + p.Lastname, $"{searchstring}%")).ToListAsync();
           
            return Ok(new Response<List<Player>>(searchResult, 0)); // zero is a place holder for now
        }

        // GET: api/Player/5
        // [HttpGet("{PLAYER_NAME}")]
        // public async Task<ActionResult<Player>> GetPlayer(string PLAYER_NAME)
        // {
        //     var player = await _context.Players.FindAsync(PLAYER_NAME);

        //     if (player == null)
        //     {
        //         return NotFound();
        //     }

        //     return player;
        // }


        // PUT: api/Player/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // [HttpPut("{id}")]
        // public async Task<IActionResult> PutPlayer(int id, Player player)
        // {
        //     if (id != player.Id)
        //     {
        //         return BadRequest();
        //     }

        //     _context.Entry(player).State = EntityState.Modified;

        //     try
        //     {
        //         await _context.SaveChangesAsync();
        //     }
        //     catch (DbUpdateConcurrencyException)
        //     {
        //         if (!PlayerExists(id))
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

        // POST: api/Player
        // [HttpPost]
        // public async Task<ActionResult<Player>> PostPlayer(Player player)
        // {
        //     _context.Players.Add(player);
        //     await _context.SaveChangesAsync();

        //     return CreatedAtAction("GetPlayer", new { id = player.Id }, player);
        // }

        // DELETE: api/Player/5
        // [HttpDelete("{id}")]
        // public async Task<IActionResult> DeletePlayer(int id)
        // {
        //     var player = await _context.Players.FindAsync(id);
        //     if (player == null)
        //     {
        //         return NotFound();
        //     }

        //     _context.Players.Remove(player);
        //     await _context.SaveChangesAsync();

        //     return NoContent();
        // }

        // private bool PlayerExists(int id)
        // {
        //     return _context.Players.Any(e => e.Id == id);
        // }
    }
}
