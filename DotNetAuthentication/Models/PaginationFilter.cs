using DotNetAuthentication.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetAuthentication.Models
{
    public class PaginationFilter
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SortString { get; set; }
        public string SortOrder { get; set; }

        public PaginationFilter()
        {
            this.PageNumber = 1;
            this.PageSize = 10;
            this.SortString = "FIRSTNAME";
            this.SortOrder = "ASC";
        }

        public PaginationFilter(int pageNumber, int pageSize, string sortString, string sortOrder)
        {
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = pageSize > 30 ? 30 : pageSize;

            var pNames = typeof(Player).GetProperties().Select(p => p.Name).ToArray();

        //https://stackoverflow.com/questions/2912476/using-c-sharp-to-check-if-string-contains-a-string-in-string-array
            if (pNames.Any(sortString.Contains))
            {
                SortString = sortString;
            }
            else
            {
                SortString = "FIRSTNAME";
            }
                
            if (sortOrder == "ASC" || sortOrder == "DESC")
            {
                SortOrder = sortOrder;
            }
            else
            {
                SortOrder = "ASC";
            }            
        }

        public int NumberOfPages(int totalRecords, int pageSize)
        {
            var pagesCount = Math.Ceiling(((decimal)totalRecords /(decimal) pageSize));           
            return (int)pagesCount;
        }


    }
}
