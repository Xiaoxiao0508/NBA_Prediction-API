using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetAuthentication.Models
{
    public class PlayerSelections
    {

        public string TeamName { get; set; }
        public int UserId { get; set; }
        public int[] PlayerKeys { get; set; }
        public PlayerSelections()
        {
        }

        //int userId,
        //UserId = userId;
        public PlayerSelections(string teamName, int[] playerKeys)
        {
            TeamName = teamName;            
            PlayerKeys = playerKeys;
        }



    }
}
