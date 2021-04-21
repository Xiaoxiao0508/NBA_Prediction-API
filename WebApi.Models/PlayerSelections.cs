using System;
using System.Collections.Generic;

namespace WebApi.Models
{
    public class PlayerSelections
    {
       
        public string TeamName { get; set; }
        public int[] PlayerKeys{get;set;}
         public PlayerSelections()
        {
        }

        public PlayerSelections(string teamName, int[] playerKeys)
        {
            TeamName = teamName;
            PlayerKeys = playerKeys;
        }


       
    }
}
