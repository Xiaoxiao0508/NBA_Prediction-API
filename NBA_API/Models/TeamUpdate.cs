using System;

namespace NBA_API.Models
{
    public class TeamUpdate
    {
        public string TeamName { get; set; }

        public TeamUpdate(string teamName)
        {
            TeamName = teamName;
        }

        public TeamUpdate()
        {
        }
    }
}
