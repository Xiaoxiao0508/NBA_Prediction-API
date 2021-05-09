using System;

namespace NBA_API.Models
{
    public class TeamUpdate
    {
        public string Token { get; set; }
        public string TeamName { get; set; }

        public TeamUpdate(string token, string teamName)
        {
            Token = token;
            TeamName = teamName;
        }
    }
}
