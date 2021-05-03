using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetAuthentication.Models
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
