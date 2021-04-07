using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class Player
    {
        public Player()
        {
        }

        public Player(int sEASON, int pLAYER_ID, string pLAYER_NAME, string fIRSTNAME, string lASTNAME, string tEAM_ABBREVIATION, int aGE, int gP, int w, int l, decimal w_PCT, decimal mINS, decimal fGM, decimal fGA, decimal fG_PCT, decimal fG3M, decimal fG3A, decimal fG3_PCT, decimal fTM, decimal fTA, decimal fT_PCT, decimal oREB, decimal dREB, decimal rEB, decimal aST, decimal tOV, decimal sTL, decimal bLK, decimal bLKA, decimal pF, decimal pFD, decimal pTS, decimal pLUS_MINUS, decimal nBA_FANTASY_PTS)
        {
            SEASON = sEASON;
            PLAYER_ID = pLAYER_ID;
            PLAYER_NAME = pLAYER_NAME;
            FIRSTNAME = fIRSTNAME;
            LASTNAME = lASTNAME;
            TEAM_ABBREVIATION = tEAM_ABBREVIATION;
            AGE = aGE;
            GP = gP;
            W = w;
            L = l;
            W_PCT = w_PCT;
            MINS = mINS;
            FGM = fGM;
            FGA = fGA;
            FG_PCT = fG_PCT;
            FG3M = fG3M;
            FG3A = fG3A;
            FG3_PCT = fG3_PCT;
            FTM = fTM;
            FTA = fTA;
            FT_PCT = fT_PCT;
            OREB = oREB;
            DREB = dREB;
            REB = rEB;
            AST = aST;
            TOV = tOV;
            STL = sTL;
            BLK = bLK;
            BLKA = bLKA;
            PF = pF;
            PFD = pFD;
            PTS = pTS;
            PLUS_MINUS = pLUS_MINUS;
            NBA_FANTASY_PTS = nBA_FANTASY_PTS;
        }

        [JsonProperty("SEASON")]
        public int SEASON { get; set; }
        [JsonProperty("PLAYER_ID")]
        public int PLAYER_ID { get; set; }

        [JsonProperty("PLAYER_NAME")]
        public string PLAYER_NAME { get; set; }
        [JsonProperty("FIRSTNAME")]
        public string FIRSTNAME { get; set; }
        [JsonProperty("LASTNAME")]
        public string LASTNAME { get; set; }

         [JsonProperty("TEAM_ABBREVIATION")]
        public string TEAM_ABBREVIATION { get; set; }
        [JsonProperty("AGE")]
        public int AGE { get; set; }
        [JsonProperty("GP")]
        public int GP { get; set; }
        [JsonProperty("W")]
        public int W { get; set; }
        [JsonProperty("L")]
        public int L { get; set; }
        [JsonProperty("W_PCT")]
        [Column(TypeName = "decimal(5,3)")]
        public decimal W_PCT { get; set; }
        [JsonProperty("MINS")]
        [Column(TypeName = "decimal(4,1)")]
        public decimal MINS { get; set; }
        [JsonProperty("FGM")]
         [Column(TypeName = "decimal(4,1)")]
        public decimal FGM { get; set; }
        [JsonProperty("FGA")]
         [Column(TypeName = "decimal(4,1)")]
        public decimal FGA { get; set; }
        [JsonProperty("FG_PCT")]
         [Column(TypeName = "decimal(5,3)")]
        public decimal FG_PCT { get; set; }
        [JsonProperty("FG3M")]
         [Column(TypeName = "decimal(3,1)")]
        public decimal FG3M { get; set; }
        [JsonProperty("FG3A")]
         [Column(TypeName = "decimal(4,1)")]
        public decimal FG3A { get; set; }
        [JsonProperty("FG3_PCT")]
        [Column(TypeName = "decimal(5,3)")]
        public decimal FG3_PCT { get; set; }
        [JsonProperty("FTM")]
          [Column(TypeName = "decimal(4,1)")]
        public decimal FTM { get; set; }
        [JsonProperty("FTA")]
          [Column(TypeName = "decimal(4,1)")]
        public decimal FTA { get; set; }
        [JsonProperty("FT_PCT")]
           [Column(TypeName = "decimal(5,3)")]
        public decimal FT_PCT { get; set; }
        [JsonProperty("OREB")]
          [Column(TypeName = "decimal(3,1)")]
        public decimal OREB { get; set; }
        [JsonProperty("DREB")]
         [Column(TypeName = "decimal(4,1)")]
        public decimal DREB { get; set; }
        [JsonProperty("REB")]
         [Column(TypeName = "decimal(4,1)")]
        public decimal REB { get; set; }
        [JsonProperty("AST")]
         [Column(TypeName = "decimal(4,1)")]
        public decimal AST { get; set; }
        [JsonProperty("TOV")]
         [Column(TypeName = "decimal(3,1)")]
      
        public decimal TOV { get; set; }
        [JsonProperty("STL")]
         [Column(TypeName = "decimal(3,1)")]
        public decimal STL { get; set; }
        [JsonProperty("BLK")]
         [Column(TypeName = "decimal(3,1)")]
        public decimal BLK { get; set; }
        [JsonProperty("BLKA")]
         [Column(TypeName = "decimal(3,1)")]
        public decimal BLKA { get; set; }
        [JsonProperty("PF")]
         [Column(TypeName = "decimal(3,1)")]
        public decimal PF { get; set; }
        [JsonProperty("PFD")]
         [Column(TypeName = "decimal(3,1)")]
        public decimal PFD { get; set; }
        [JsonProperty("PTS")]
         [Column(TypeName = "decimal(4,1)")]
        public decimal PTS { get; set; }
        [JsonProperty("PLUS_MINUS")]
         [Column(TypeName = "decimal(5,1)")]
        public decimal PLUS_MINUS { get; set; }
        [JsonProperty("NBA_FANTASY_PTS")]
         [Column(TypeName = "decimal(4,1)")]
        public decimal NBA_FANTASY_PTS { get; set; }

    }
}
