
using System;

namespace WebApi.Models
{
    public class FullTeamRosterRequest
    {
        public FullTeamRosterRequest()
        {
        }

        public FullTeamRosterRequest(string teamName, string sortString, string sortType)
        {
            TeamName = teamName;
            SortString = "FIRSTNAME";
            SortType = "DESC";
        }

        public string TeamName { get; set; }
        public string SortString { get; set; }
        public string SortType { get; set; }

      


    }
}
