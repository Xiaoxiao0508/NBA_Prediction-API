using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetAuthentication.Models
{
    public class PlayerSelections
    {

        public string TeamName { get; set; }
        public int Id { get; set; }
        public int[] PlayerKeys { get; set; }
        public PlayerSelections()
        {
        }

        public PlayerSelections(string teamName,int id, int[] playerKeys)
        {
            TeamName = teamName;
            Id = id;
            PlayerKeys = playerKeys;
        }



    }
}
