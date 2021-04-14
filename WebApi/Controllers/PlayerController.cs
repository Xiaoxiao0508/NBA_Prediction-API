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
        public async Task<ActionResult<IEnumerable<Player>>> GetallPlayers([FromQuery] PaginationFilter filter, string filstring)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize); 
            // var res = typeof(AllPlayer).GetProperties()
            //             .Select(property => property.Name)
            //             .ToArray();
            var pagedData = await _context.allPlayers
            // .OrderBy()
            .Skip((validFilter.PageNumber - 1) * validFilter.PageSize) 
            .Take(validFilter.PageSize)
            .ToListAsync();

            var totalRecords = await _context.allPlayers.CountAsync();
            var pagesCount = (decimal)totalRecords / (decimal)filter.PageSize;

            if((pagesCount % 1) != 0){
              pagesCount = Decimal.ToInt32(pagesCount) ;
              pagesCount += 1;
            }
//  string procedure_AddGame = "[dbo].[ADD_GAME]";
//             SqlConnection connection = new SqlConnection(this.connnectionString);
//             connection.Open();
//             using (SqlCommand command = new SqlCommand(procedure_AddGame, connection))
//             {
//                 command.CommandType = CommandType.StoredProcedure;
//                 command.Parameters.Add(new SqlParameter("@UserName", playerchoice.name));
//                 command.Parameters.Add(new SqlParameter("@GameStarted", DateTime.Now.ToString("MM/dd/yyyy HH:mm")));
//                 command.Parameters.Add(new SqlParameter("@GameResult", gameresult.result));
//                 command.Parameters.Add(new SqlParameter("@NumOfTurns", playerchoice.numberofrounds));

//                 command.ExecuteNonQuery();
//             }

            // return Ok(new PageResponse<List<Player>>(pagedData, validFilter.PageNumber, validFilter.PageSize));
            return Ok(new Response<List<Player>>(pagedData, Decimal.ToInt32(pagesCount)));
        }


        // GET: api/Player
        [HttpGet("{search}")]

        public async Task<ActionResult<IEnumerable<Player>>> GetPlayer([FromQuery] SearchPaginationFilter filter)
        {

            // partial first name and partial lastname search or player initials with pagination
            var validFilter = new SearchPaginationFilter(filter.searchstring, filter.PageNumber, filter.PageSize);
            
            string[]? splitString = filter.searchstring?.Split(' ');

            
            if (splitString?.Length == 2)
            {
                //filters the player view data based on the searchstring and adds pagination based on the url.
                var pageData = await _context.allPlayers.Where(p=>EF.Functions.Like(p.FIRSTNAME, $"{splitString[0]}%") && EF.Functions.Like(p.Lastname, $"{splitString[1]}%"))
                    .OrderBy(p => p.FIRSTNAME)
                    .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                    .Take(validFilter.PageSize)
                    .ToListAsync();

                //total pages in current search
                var totalRecords = await _context.allPlayers.Where(p=>EF.Functions.Like(p.FIRSTNAME, $"{splitString[0]}%") && EF.Functions.Like(p.Lastname, $"{splitString[1]}%"))
                    .OrderBy(p => p.FIRSTNAME).CountAsync();

                var pagesCount = (decimal)totalRecords / (decimal)filter.PageSize;

                if((pagesCount % 1) != 0){
                    pagesCount = Decimal.ToInt32(pagesCount) ;
                    pagesCount += 1;
                }

                return  Ok(new Response<List<Player>>(pageData,Decimal.ToInt32(pagesCount)));
            }
            else
            {
                //Partial firstname + lastname search this code runs if the splitString array has more then 2 items
                var pageData = await _context.allPlayers
                    .Where(p=>EF.Functions.Like(p.FIRSTNAME, $"{filter.searchstring}%")||EF.Functions.Like(p.Lastname, $"{filter.searchstring}%")||EF.Functions.Like(p.FIRSTNAME +" "+ p.Lastname,$"{filter.searchstring}%"))
                    .OrderBy(p => p.FIRSTNAME)
                    .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                    .Take(validFilter.PageSize)
                    .ToListAsync();

                //total pages in current search
                var totalRecords = await _context.allPlayers.Where(p=>EF.Functions.Like(p.FIRSTNAME, $"{filter.searchstring}%")||EF.Functions.Like(p.Lastname, $"{filter.searchstring}%")||EF.Functions.Like(p.FIRSTNAME +" "+ p.Lastname,$"{filter.searchstring}%")).CountAsync();

                var pagesCount = (decimal)totalRecords / (decimal)filter.PageSize;

                if((pagesCount % 1) != 0){
                    pagesCount = Decimal.ToInt32(pagesCount);
                    pagesCount += 1;
                }
                    

                return  Ok(new Response<List<Player>>(pageData, Decimal.ToInt32(pagesCount)));
            }
        }

        //Get Column Headers
        [HttpGet("headers")]

        public async Task<ActionResult<IEnumerable<ColumnHeaders>>> GetHeaders()
        {
            var Data = await _context.columnHeaders
                .ToListAsync();

            return Ok(new Response<List<ColumnHeaders>>(Data));
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
