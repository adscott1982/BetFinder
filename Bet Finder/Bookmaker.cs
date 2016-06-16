using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bet_Finder
{
    class Bookmaker
    {
        public string Code;     // Bookmaker code used by oddschecker
        public string Name;     // Full name of bookmaker
        public bool Enabled;    // whether the bookmaker is enabled 
        public int Rating;      // Rating of the bookmaker, higher is better
        public string URL;      // Main URL for bookmakers website

        public Bookmaker(string code, string name, bool enabled, int rating, string url)
        {
            Code = code;
            Name = name;
            Enabled = enabled;
            Rating = rating;
            URL = url;
        }
    }
}
