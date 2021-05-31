using System;

namespace NBA_API.Models
{
    public class PlayerSelection
    {
        

        public string TeamName { get; set; }
        public string Id { get; set; }
        public int Player_key{ get; set; }

      public PlayerSelection()
        {
        }

        public PlayerSelection(string teamName, string id, int player_key)
        {
            TeamName = teamName;
            Id = id;
            Player_key = player_key;
        }

    }
}
