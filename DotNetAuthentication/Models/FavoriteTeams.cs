using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetAuthentication.Models
{
    public class FavoriteTeams
    {
        public string Token { get; set; }

        public List<string> TeamNames { get; set; }

        public bool IsFav { get; set; }
    }
}
