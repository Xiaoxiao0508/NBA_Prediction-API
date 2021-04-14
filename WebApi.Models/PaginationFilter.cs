using System;

namespace WebApi.Models
{
    public class PaginationFilter
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SortString { get; set; }

        public PaginationFilter()
        {
            this.PageNumber = 1;
            this.PageSize = 10;
            this.SortString="FIRSTNAME";
        }

        public PaginationFilter(int pageNumber, int pageSize, string sortString)
        {
            PageNumber = pageNumber < 1 ? 1 : pageNumber;;
            PageSize = pageSize > 30 ? 30 : pageSize;;
            SortString = sortString;
        }

       
    }
}
