using System;
using System.Collections.Generic;

namespace WebApi.Models
{
    public class TeamPlayers
    {
      

        public string TeamName { get; set; }
        public int Player_Key { get; set; }
        
        public TeamPlayers()
        {
        }

        public TeamPlayers(string teamName, int player_Key)
        {
            TeamName = teamName;
            Player_Key = player_Key;
        }

    }
}
