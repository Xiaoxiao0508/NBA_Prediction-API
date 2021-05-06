using System;

namespace NBA_API.Models
{
    public class PlayerSelection
    {
        public string TeamName { get; set; }
        public int Player_key{ get; set; }

        public PlayerSelection()
        {

        }

        public PlayerSelection(string teamName, int player_Key)
        {
            TeamName = teamName;
            Player_key = player_Key;
        }

    }
}
