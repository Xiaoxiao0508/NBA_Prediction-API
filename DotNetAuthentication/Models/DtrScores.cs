using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetAuthentication.Models
{
    public class DtrScores
    {
        public string TeamName { get; set; }

        public bool Fav { get; set; }

        public int PlayerCount { get; set; }

        public decimal DTRScores { get; set; }        
    }
}
