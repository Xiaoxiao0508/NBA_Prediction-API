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

        public Player (string fIRSTNAME, string lASTNAME, int aGE, int gP, decimal mINS, decimal pLUS_MINUS, decimal aST, decimal bLK, decimal bLKA, decimal oREB, decimal dREB, decimal fG_PCT, decimal fG3_PCT, decimal fG3A, decimal fG3M, decimal fGA, decimal fGM, decimal fT_PCT, decimal fTA, decimal fTM, int w, int l, decimal w_PCT, decimal pF, decimal pFD, decimal rEB, decimal tOV, decimal sTL, decimal pTS)
	{            
        FIRSTNAME = fIRSTNAME;
        LASTNAME = lASTNAME;
        AGE = aGE;
        GP = gP;
        MINS = mINS;
        PLUS_MINUS = pLUS_MINUS;
        AST = aST;
        BLK = bLK;
        BLKA = bLKA;
        OREB = oREB;
        DREB = dREB;
        FG_PCT = fG_PCT;
        FG3_PCT = fG3_PCT;
        FG3A = fG3A;
        FG3M = fG3M;
        FGA = fGA;
        FGM = fGM;
        FT_PCT = fT_PCT;
        FTA = fTA;
        FTM = fTM;
        W = w;
        L = l;
        W_PCT = w_PCT;
        PF = pF;
        PFD = pFD;
        REB = rEB;
        TOV = tOV;
        STL = sTL;
        PTS = pTS;
	}                
        [JsonProperty("FIRSTNAME")]
        public string FIRSTNAME { get; set; }

        [JsonProperty("LASTNAME")]
        public string LASTNAME { get; set; }

        [JsonProperty("AGE")]
        public int AGE { get; set; }

        [JsonProperty("GP")]
        public int GP { get; set; }
        
        [JsonProperty("MINS")]
        public decimal MINS { get; set; }

        [JsonProperty("PLUS_MINUS")]
        public decimal PLUS_MINUS { get; set; }

        [JsonProperty("AST")]
        public decimal AST { get; set; }

        [JsonProperty("BLK")]
        public decimal BLK { get; set; }

        [JsonProperty("BLKA")]
        public decimal BLKA { get; set; }

        [JsonProperty("OREB")]
        public decimal OREB { get; set; }

        [JsonProperty("DREB")]
        public decimal DREB { get; set; }

        [JsonProperty("FG_PCT")]
        public decimal FG_PCT { get; set; }

        [JsonProperty("FG3_PCT")]
        public decimal FG3_PCT { get; set; }

        [JsonProperty("FG3A")]
        public decimal FG3A { get; set; }

        [JsonProperty("FG3M")]
        public decimal FG3M { get; set; }

        [JsonProperty("FGA")]
        public decimal FGA { get; set; }

        [JsonProperty("FGM")]
        public decimal FGM { get; set; }

        [JsonProperty("FT_PCT")]
        public decimal FT_PCT { get; set; }

        [JsonProperty("FTA")]
        public decimal FTA { get; set; }

        [JsonProperty("FTM")]
        public decimal FTM { get; set; }

        [JsonProperty("W")]
        public int W { get; set; }

        [JsonProperty("L")]
        public int L { get; set; }

        [JsonProperty("W_PCT")]
        public decimal W_PCT { get; set; }

        [JsonProperty("PF")]
        public decimal PF { get; set; }

        [JsonProperty("PFD")]
        public decimal PFD { get; set; }

        [JsonProperty("REB")]
        public decimal REB { get; set; }

        [JsonProperty("TOV")]
        public decimal TOV { get; set; }

        [JsonProperty("STL")]
        public decimal STL { get; set; }

        [JsonProperty("PTS")]
        public decimal PTS { get; set; }
        

    }
}
