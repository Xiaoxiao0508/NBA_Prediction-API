
using System;

namespace WebApi.Models
{
    public class FullTeamRosterRequest
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SortString { get; set; }
        public string TeamName { get; set; }

        public FullTeamRosterRequest()
        {
            this.PageNumber = 1;
            this.PageSize = 10;
            this.SortString = "FIRSTNAME";
            this.TeamName = "";
        }

        public FullTeamRosterRequest(int pageNumber, int pageSize, string sortString, string tname)
        {
            PageNumber = pageNumber < 1 ? 1 : pageNumber; ;
            PageSize = pageSize > 30 ? 30 : pageSize; ;
            SortString = sortString;
            TeamName = tname;
        }


    }
}
