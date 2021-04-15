using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TeamPlayerController : ControllerBase
    {
        TeamPlayers teamPlayers = new TeamPlayers();

        public TeamPlayerController(TeamPlayers teamPlayers)
        {
            TeamPlayers = teamPlayers;
        }

        [HttpPost]
        public bool AddPlayer([FromBody] Player player)
        {
            if (TeamPlayers.PlayerList.Count <= 15)
            {
                TeamPlayers.PlayerList.Add(player);
                return true;
            }
            else

                return false;
        }
    }
}


