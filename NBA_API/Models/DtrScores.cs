using System;

namespace NBA_API.Models
{
    public class DtrScores
    {
        

        public string TeamName { get; set; }

        public bool isFav { get; set; }

        public int PlayerCount { get; set; }
        
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
