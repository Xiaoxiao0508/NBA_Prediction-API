using System;

namespace WebApi.Models
{
    public class PlayerSelection
    {
        public PlayerSelection()
        {
        }

        public PlayerSelection(string teamName, int player_Key)
        {
            TeamName = teamName;
            Player_key = player_Key;
        }

        public string TeamName { get; set; }
        public int Player_key{get;set;}
    }
}
