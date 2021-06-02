using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NBA_API.Models;



namespace NBA_API.Controllers
{
    // /Add [Authorize] at the top of your controllers so you can restrict the endpoints to only users that have logged in
    [Authorize]
    [Route("[controller]")]
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
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize, filter.SortString, filter.SortOrder);
            var totalRecords = await _context.allPlayers.CountAsync();
            var pagesCount = (decimal)totalRecords / (decimal)filter.PageSize;
            var pagedData = await _context.allPlayers
            .OrderBy(p => EF.Property<object>(p, validFilter.SortString))
            .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
            .Take(validFilter.PageSize)
            .ToListAsync();
            if ((pagesCount % 1) != 0)
            {
                pagesCount = Decimal.ToInt32(pagesCount);
                pagesCount += 1;
            }
            if (filter.SortOrder == "ASC")
            {
                return Ok(new Response<List<Player>>(pagedData, Decimal.ToInt32(pagesCount)));

            }
            else if (filter.SortOrder == "DESC")
            {
                var pagedData1 = await _context.allPlayers
           .OrderByDescending(p => EF.Property<object>(p, validFilter.SortString))
           .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
           .Take(validFilter.PageSize)
           .ToListAsync();
                return Ok(new Response<List<Player>>(pagedData1, Decimal.ToInt32(pagesCount)));
            }

            return Ok(new Response<List<Player>>(pagedData, Decimal.ToInt32(pagesCount)));
        }

        [Route("getPlayersFromTeam")]
        [HttpGet]
        /// <summary>
        /// get the players of a team
        /// </summary>
        /// <param name="teamReq"></param>
        /// <returns></returns>
        public async Task<ActionResult<IEnumerable<Player>>> getPlayersFromTeam([FromQuery] FullTeamRosterRequest teamReq)
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var UserId = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
            var pagedData = await _context.allPlayers
                .FromSqlRaw("getPlayersFromTeam @p0,@p1,@p2,@p3", UserId, teamReq.TeamName, teamReq.SortString, teamReq.SortType)

            .ToListAsync();
            return Ok(new Response<List<Player>>(pagedData));
        }

    
        [Route("teamSummary")]
        [HttpGet]
        /// <summary>
        /// get the players and dtr score of a team
        /// </summary>
        /// <param name="teamReq"></param>
        /// <returns></returns>
        public async Task<ActionResult<IEnumerable<Player>>> teamSummary([FromQuery] FullTeamRosterRequest teamReq)
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var UserId = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
            var pagedData = await _context.allPlayers.FromSqlRaw
                ("getPlayersFromTeam @p0,@p1,@p2,@p3", UserId, teamReq.TeamName, teamReq.SortString, teamReq.SortType).ToListAsync();
            var parameterReturn = new SqlParameter
            {
                ParameterName = "dtr",
                SqlDbType = System.Data.SqlDbType.Int,
                Direction = System.Data.ParameterDirection.Output,
            };

            var dtrScore = _context.Database.ExecuteSqlRaw("EXEC @dtr = [dbo].[DtrScore] @p0, @p1", UserId, teamReq.TeamName, parameterReturn);

            int dtr = (int)parameterReturn.Value;

            var result = new { pagedData, dtr };

            return Ok(result);
        }
        [HttpGet("SearchPlayer")]

        public async Task<ActionResult<IEnumerable<Player>>> GetPlayer([FromQuery] SearchPaginationFilter filter)
        {

            // partial first name and partial lastname search or player initials with pagination
            var validFilter = new SearchPaginationFilter(filter.searchstring, filter.PageNumber, filter.PageSize, filter.SortString, filter.SortOrder);

            string[]? splitString = filter.searchstring?.Split(' ');
            var pagedData = new List<Player>();
            decimal pagescount = 0;


            if (splitString?.Length == 2)
            {
                //filters the player view data based on the searchstring and adds pagination based on the url.
                var pageData = await _context.allPlayers.Where(p => EF.Functions.Like(p.FIRSTNAME, $"{splitString[0]}%") && EF.Functions.Like(p.LASTNAME, $"{splitString[1]}%"))
                    .OrderBy(p => EF.Property<object>(p, validFilter.SortString))
                    .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                    .Take(validFilter.PageSize)
                    .ToListAsync();

                //total pages in current search
                var totalRecords = await _context.allPlayers.Where(p => EF.Functions.Like(p.FIRSTNAME, $"{splitString[0]}%") && EF.Functions.Like(p.LASTNAME, $"{splitString[1]}%"))
                    .OrderBy(p => p.FIRSTNAME).CountAsync();

                var pagesCount = (decimal)totalRecords / (decimal)filter.PageSize;

                if ((pagesCount % 1) != 0)
                {
                    pagesCount = Decimal.ToInt32(pagesCount);
                    pagesCount += 1;
                }
                if (filter.SortOrder == "ASC")
                {
                    return Ok(new Response<List<Player>>(pageData, Decimal.ToInt32(pagesCount)));

                }
                else if (filter.SortOrder == "DESC")
                {
                    pageData = await _context.allPlayers.Where(p => EF.Functions.Like(p.FIRSTNAME, $"{splitString[0]}%") && EF.Functions.Like(p.LASTNAME, $"{splitString[1]}%"))
                       .OrderByDescending(p => EF.Property<object>(p, validFilter.SortString))
                       .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                       .Take(validFilter.PageSize)
                       .ToListAsync();

                    return Ok(new Response<List<Player>>(pageData, Decimal.ToInt32(pagesCount)));
                }
            }
            else
            {
                //Partial firstname + lastname search this code runs if the splitString array has more then 2 items
                var pageData = await _context.allPlayers
                    .Where(p => EF.Functions.Like(p.FIRSTNAME, $"{filter.searchstring}%") || EF.Functions.Like(p.LASTNAME, $"{filter.searchstring}%") || EF.Functions.Like(p.FIRSTNAME + " " + p.LASTNAME, $"{filter.searchstring}%"))
                      .OrderBy(p => EF.Property<object>(p, validFilter.SortString))
                    .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                    .Take(validFilter.PageSize)
                    .ToListAsync();

                //total pages in current search
                var totalRecords = await _context.allPlayers.Where(p => EF.Functions.Like(p.FIRSTNAME, $"{filter.searchstring}%") || EF.Functions.Like(p.LASTNAME, $"{filter.searchstring}%") || EF.Functions.Like(p.FIRSTNAME + " " + p.LASTNAME, $"{filter.searchstring}%")).CountAsync();

                var pagesCount = (decimal)totalRecords / (decimal)filter.PageSize;

                if ((pagesCount % 1) != 0)
                {
                    pagesCount = Decimal.ToInt32(pagesCount);
                    pagesCount += 1;
                }
                if (filter.SortOrder == "ASC")

                {
                    pagedData = pageData;
                    pagescount = pagesCount;
                    return Ok(new Response<List<Player>>(pageData, Decimal.ToInt32(pagesCount)));

                }
                else if (filter.SortOrder == "DESC")
                {
                    pageData = await _context.allPlayers
                    .Where(p => EF.Functions.Like(p.FIRSTNAME, $"{filter.searchstring}%") || EF.Functions.Like(p.LASTNAME, $"{filter.searchstring}%") || EF.Functions.Like(p.FIRSTNAME + " " + p.LASTNAME, $"{filter.searchstring}%"))
                      .OrderByDescending(p => EF.Property<object>(p, validFilter.SortString))
                    .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                    .Take(validFilter.PageSize)
                    .ToListAsync();
                    return Ok(new Response<List<Player>>(pageData, Decimal.ToInt32(pagesCount)));
                }

            }
            return Ok(new Response<List<Player>>(pagedData, Decimal.ToInt32(pagescount)));
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
        // search player by player_key
        [HttpGet("{Player_key}")]
        public async Task<ActionResult<Player>> GetPlayer(int Player_key)
        {
            var player = await _context.allPlayers.FindAsync(Player_key);

            if (player == null)
            {
                return NotFound();
            }

            return player;
        }
    }
}
