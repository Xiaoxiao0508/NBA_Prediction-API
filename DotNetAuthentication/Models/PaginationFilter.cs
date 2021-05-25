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
            //default if there are not arguments
            this.PageNumber = 1;
            this.PageSize = 30;
            this.SortString = "FIRSTNAME";
            this.SortOrder = "ASC";
        }

        public PaginationFilter(int pageNumber, int pageSize, string sortString, string sortOrder)
        {
            //default if incorrect numbers are inputed
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = pageSize > 30 ? 30 : pageSize;

            var pNames = typeof(Player).GetProperties().Select(p => p.Name).ToArray();

            //check statistic in SortString is valid
        //https://stackoverflow.com/questions/2912476/using-c-sharp-to-check-if-string-contains-a-string-in-string-array
            if (pNames.Any(sortString.Contains))
            {
                SortString = sortString;
            }
            else
            {
                throw new ArgumentOutOfRangeException($"{sortString} is not a valid sort column");
            }

            //Checks if valid sort order is inputed    
            if (sortOrder == "ASC" || sortOrder == "DESC")
            {
                SortOrder = sortOrder;
            }
            else
            {
                throw new ArgumentOutOfRangeException("Sort Order must be ASC or DESC");
            }            
        }

        //Calculates Number of Pages
        public int NumberOfPages(int totalRecords, int pageSize)
        {                      
            //https://stackoverflow.com/questions/21211843/how-to-use-try-catch-to-check-for-whether-a-number-is-within-a-range-of-1-3-c-s
                if (pageSize > 30 || pageSize <= 0) { throw new ArgumentOutOfRangeException("Page Size must be between 1 and 30"); }

                var pagesCount = Math.Ceiling(((decimal)totalRecords /(decimal) pageSize));           
                return (int)pagesCount;                       
        }


    }
}
