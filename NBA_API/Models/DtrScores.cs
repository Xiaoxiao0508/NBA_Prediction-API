using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NBA_API.Models
{
    public class DtrScores
    {
        

        public string TeamName { get; set; }

        public bool isFav { get; set; }

        public int PlayerCount { get; set; }
         [Column(TypeName = "decimal(5,1)")]
        public decimal DTRScores { get; set; }  

        public DtrScores()
        {
        }

        public DtrScores(string teamName, bool isfav, int playerCount, decimal dTRScores)
        {
            TeamName = teamName;
            isFav = isfav;
            PlayerCount = playerCount;
            DTRScores = dTRScores;
        }
    }
}
