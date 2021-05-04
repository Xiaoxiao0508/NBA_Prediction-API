using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetAuthentication.DB;
using DotNetAuthentication.Models;
using JWT;
using JWT.Algorithms;
using JWT.Exceptions;
using JWT.Serializers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace DotNetAuthentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : Controller
    {
        private readonly NBAContext _context;

        public PlayersController(NBAContext context)
        {
            _context = context;
        }

        // Get all players
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Player>>> GetallPlayers([FromQuery] PaginationFilter filter)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize, filter.SortString, filter.SortOrder);
            var totalRecords = await _context.allPlayers.CountAsync();
            var pagesCount = (decimal)totalRecords / (decimal)filter.PageSize;
            var pagedData = await _context.allPlayers
            // .OrderByDescending(p =>"p."+validFilter.SortString)
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
           // .OrderByDescending(p =>"p."+validFilter.SortString)
           .OrderByDescending(p => EF.Property<object>(p, validFilter.SortString))
           .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
           .Take(validFilter.PageSize)
           .ToListAsync();
                return Ok(new Response<List<Player>>(pagedData1, Decimal.ToInt32(pagesCount)));
            }

            return Ok(new Response<List<Player>>(pagedData, Decimal.ToInt32(pagesCount)));
        }

        // Get players from team for a user
        [Route("getPlayersFromTeam")]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<Player>>> getPlayersFromTeam([FromHeader] string Token, [FromBody] FullTeamRosterRequest teamReq)
        {
            //See all teams the current user has.
            try
            { 
                //Validate Token
                var authorise = new Authorise();
                var userId = authorise.Validate(Token);

                //If valid send requested data
                var userInput = teamReq.TeamName;
                var usortcol = teamReq.SortString;
                var uSortType = teamReq.SortType;
                var pagedData = await _context.allPlayers
                    .FromSqlRaw("getPlayersFromTeam @p0,@p1,@p2,@p3", userId, userInput, usortcol, uSortType).ToListAsync();
                return Ok(new Response<List<Player>>(pagedData));
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
