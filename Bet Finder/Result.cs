using System;
using System.Collections.Generic;

namespace Bet_Finder
{
    class Result
    {
        // Variables to be retrieved from site
        public string name;
        public double odds;
        public Bookmaker bookmaker;

        // Variables to be calculated
        public double chance;
        public double chanceAdjusted;
        public double bet;
        public double betReturn;

        // Whether there is a valid result
        public bool hasResult;

        // Constants for parsing the HTML source
        const string NAME_IDENTIFIER_START = "data-name=\"";
        const string NAME_IDENTIFIER_END = "\"";

        const string ODDS_LINE_IDENTIFIER_START = "<td id=";
        const string ODDS_LINE_IDENTIFIER_END = "</td>";

        const string ODDS_IDENTIFIER_START = "\">";
        const string ODDS_IDENTIFIER_END = "\"";

        public Result(string name, List<string> bookieCodes, List<string> oddsList, List<Bookmaker> enabledBookies)
        {
            // Go through each bookie in list and store odds if available updating with the best odds, favour bookie with higher rating
            this.name = name;
            double temporaryOdds = 0d;
            int temporaryRating = 0;
            Bookmaker tempBookmaker = null;

            foreach (Bookmaker bookie in enabledBookies)
            {
                double odds = 0;
                int i = bookieCodes.IndexOf(bookie.Code);

                try
                {
                    odds = Convert.ToDouble(oddsList[i]);
                }
                catch
                {
                    odds = 0;
                }

                if ((odds > temporaryOdds) ||
                    (odds >= temporaryOdds) && (bookie.Rating > temporaryRating))
                {
                    temporaryOdds = odds;
                    temporaryRating = bookie.Rating;
                    tempBookmaker = bookie;
                }

            }

            // Store odds of best result, and bookmaker
            if ((tempBookmaker != null) && (temporaryOdds > 1))
            {
                this.hasResult = true;
                this.bookmaker = tempBookmaker;
                this.odds = temporaryOdds;
            }
            else
            {
                this.hasResult = false;
                this.bookmaker = null;
                this.odds = 1d;
            }
        }
    }
}
