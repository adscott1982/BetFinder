using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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
                hasResult = true;
                bookmaker = tempBookmaker;
                odds = temporaryOdds;
            }
            else
            {
                hasResult = false;
                bookmaker = null;
                odds = 1d;
            }
        }
    }
}

//        public Result(string eventSource, List<Bookmaker> enabledBookies)
//        {
//            // Find name of result (e.g. Arsenal/Draw/Liverpool)
//            name = FindName(eventSource);

//            // Go through each bookie in list and store odds if available updating with the best odds, favour bookie with higher rating
//            double temporaryOdds = 0d;
//            int temporaryRating = 0;
//            Bookmaker tempBookmaker = null;

//            foreach (Bookmaker bookie in enabledBookies)
//            {
//                double odds = FindOdds(eventSource, bookie);

//                if ((odds > temporaryOdds) ||
//                    (odds >= temporaryOdds) && (bookie.Rating > temporaryRating))
//                {
//                    temporaryOdds = odds;
//                    temporaryRating = bookie.Rating;
//                    tempBookmaker = bookie;
//                }

//            }

            

//            // Store odds of best result, and bookmaker
//            if ((tempBookmaker != null) && (temporaryOdds > 1))
//            {
//                hasResult = true;
//                bookmaker = tempBookmaker;
//                odds = temporaryOdds;
//            }
//            else
//            {
//                hasResult = false;
//                bookmaker = null;
//                odds = 1d;
//            }
//        }

//        private string FindName(string source)
//        {
//            // Remove everything before the start of the name
//            int nameStart = source.IndexOf(NAME_IDENTIFIER_START) + NAME_IDENTIFIER_START.Length;
//            source = source.Substring(nameStart);

//            // Remove everything in the string after the name
//            int nameEnd = source.IndexOf(NAME_IDENTIFIER_END);
//            source = source.Substring(0, nameEnd);

//            // Return the only thing remaining in the source which is the name
//            return source;
//        }

//        private double FindOdds(string eventSource, Bookmaker bookie)
//        {
//            List<string> oddsLines = OddsLines(eventSource);

//            double odds = 0d;

//            foreach (string line in oddsLines)
//            {
//                // Check if line contains bookie code and odds identifier, then extract string of the odds and attempt to convert to double
//                if ((line.Contains(bookie.Code)) && (line.Contains(ODDS_IDENTIFIER_START)))
//                {
//                    int oddsStart = line.IndexOf(ODDS_IDENTIFIER_START) + ODDS_IDENTIFIER_START.Length;
//                    string stringOdds = line.Substring(oddsStart);
//                    stringOdds = stringOdds.Substring(0, stringOdds.Length);

//                    if (stringOdds.Length > 0)
//                    {
//                        try
//                        {
//                            odds = Convert.ToDouble(stringOdds);
//                        }
//                        catch
//                        {

//                        }
//                    }
//                }
//            }

//            return odds;
//        }

//        private List<string> OddsLines(string eventSource)
//        {
//            // Separate lines of odds into list
//            List<string> oddsLines = new List<string>();

//            while (eventSource.Contains(ODDS_LINE_IDENTIFIER_START))
//            {
//                // Find start and end of odds line
//                int lineStart = eventSource.IndexOf(ODDS_LINE_IDENTIFIER_START);
//                string line = eventSource.Substring(lineStart);
//                int lineEnd = line.IndexOf(ODDS_LINE_IDENTIFIER_END);

//                // Store line in list
//                line = line.Substring(0, lineEnd);
//                oddsLines.Add(line);

//                // Remove the line and move onto next
//                eventSource = eventSource.Substring(lineStart + line.Length);
//            }

//            return oddsLines;
//        }
//    }
//}
