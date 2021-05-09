using System;

namespace NBA_API.Models
{
    public class Team
    {


        public string TeamName { get; set; }
        public string Id { get; set; }
        public Team()
        {
        }

        public Team(string teamName, string id)
        {
            TeamName = teamName;
            Id = id;
        }

    }
}
