using System;

namespace NBA_API.Models
{
    public class Team
    {


        public string TeamName { get; set; }
        public string Id { get; set; }
        public bool isFav { get; set; }

        public int PlayerCount { get; set; }

        public Team()
        {
           
        }

        public Team(string teamName, string id, bool isfav, int playerCount)
        {
            TeamName = teamName;
            Id = id;
            isFav = isfav;
            PlayerCount = playerCount;
        }
    }
}
