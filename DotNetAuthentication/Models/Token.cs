using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetAuthentication.Models
{
    public class Token
    {
        public string token { get; set; }
        //public int uerId { get; set; }

        public Token(string input/*, int userId*/)
        {
           token = input;
            
        }
    }
}
