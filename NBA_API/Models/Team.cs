using System;

namespace NBA_API.Models
{
    public class Team
    {
        public string TeamName { get; set; }

        public Team()
        {

        }

        public Team(string team)
        {
            this.TeamName = team;
        }
    }
}
