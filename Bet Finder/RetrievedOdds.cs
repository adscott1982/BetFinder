using System;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Globalization;
using HtmlAgilityPack;

namespace Bet_Finder
{
    class RetrievedOdds
    {
        // Variables

        public string URL;                                     // URL of page
        public string source;
        //public List<string> outputLog;                          // Reference to output log
        public bool hasOdds = false;
        public bool hadSource = true;      
        public bool isProfitable = false;                             // Is the best betting profitable
        public bool inPlay = false;
        double totalChance = 0d;

        public double minReturn = 0d;
        public double profit = -1d;
        public double profitPercentage = -1d;

        public DateTime eventTime = DateTime.MaxValue;   // Initialise event time as earliest possible date
        public TimeSpan timeToEvent;
        public string dateString = "";
        public List<Result> results = new List<Result>();       // List to store results
        public List<Bookmaker> bookmakers;
        public string eventName = "Not found";
        public string betType = "Not found";
        public string sport = "Not found";
        public string sportBracket = "Not found";

        // Constants

        // HTML Tags

        const string eventTableTag = "//table[@class='eventTable ']";
        const string eventTableHeaderTag = "//tr[@class='eventTableHeader']";
        const string eventTableRowTag = "//tr[contains(@class,'eventTableRow')]";

        const string dateTag = "data-time";
        const string eventTag = "data-sname";
        const string betTag = "data-mname";
        const string sportTag = "data-sport-name";
        const string sportBracketTag = "data-ename";

        const string bookieTag = "data-bk";
        const string resultTag = "data-bname";
        const string oddsTag = "data-odig";
        //const string noBetTag = "o np";
        const string noBetTag = "np o";

        //// URL Constants

        // Constructor

        public RetrievedOdds(string URL, string source, List<Bookmaker> bookmakers, double stake)
        {
            this.URL = URL;
            this.source = source;
            this.bookmakers = bookmakers;

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(source);

            if (source.Length == 0)
            {
                hadSource = false;
            }

            if (doc != null)
            {
                hasOdds = ParseHTMLDoc(doc);

                if (results.Count < 2)
                {
                    hasOdds = false;
                }

                int bookMakerCount = 0;

                foreach(Result result in results)
                {
                    try
                    {
                        if (results[0].bookmaker.Code == result.bookmaker.Code) bookMakerCount++;
                    }
                    catch
                    {
                        bool exception = true;
                    }
                }

                if (bookMakerCount == results.Count) hasOdds = false;

                if (hasOdds)
                {
                    isProfitable = CheckIfProfitable();
                    CalculateReturn(stake);
                }

            }
        }

        #region HTML Parsing

        private bool ParseHTMLDoc(HtmlDocument doc)
        {
            try
            {
                ReadEventDetails(doc);

                LoadInOdds(doc);
            }
            catch
            {

            }
            

            bool hasOdds = true;

            foreach(Result result in results)
            {
                if (result.hasResult == false) hasOdds = false;
            }

            return hasOdds;
        }

        private void LoadInOdds(HtmlDocument doc)
        {
            List<string> bookieCodesList = new List<string>();

            HtmlNode headerRow = doc.DocumentNode.SelectNodes(eventTableHeaderTag)[0];

            if (headerRow != null)
            {
                foreach (HtmlNode cell in headerRow.SelectNodes("th|td"))
                {

                    HtmlAttributeCollection attributes = cell.Attributes;

                    foreach (HtmlAttribute bookieCode in attributes.AttributesWithName(bookieTag))
                    {
                        // If bookie code is not already in the list
                        if (!bookieCodesList.Contains(bookieCode.Value))
                        {
                            bookieCodesList.Add(bookieCode.Value);
                        }
                    }
                }
            }

            HtmlNodeCollection eventRows = doc.DocumentNode.SelectNodes(eventTableRowTag);

            foreach(HtmlNode eventRow in eventRows)
            {
                string name = "";
                List<string> oddsList = new List<string>();

                HtmlAttributeCollection attributes = eventRow.Attributes;

                foreach (HtmlAttribute resultName in attributes.AttributesWithName(resultTag))
                {
                    name = resultName.Value;
                }

                foreach (HtmlNode cell in eventRow.SelectNodes("th|td"))
                {
                    attributes = cell.Attributes;

                    bool isNoBet = (cell.Attributes.Contains("class") && cell.Attributes["class"].Value.Contains(noBetTag));

                    if (!isNoBet)
                    {
                        foreach (HtmlAttribute odds in attributes.AttributesWithName(oddsTag))
                        {
                            oddsList.Add(odds.Value);
                        }
                    }
                    else
                    {
                        oddsList.Add("0");
                    }

                }

                results.Add(new Result(name, bookieCodesList, oddsList, bookmakers));
            }

        }

        private void ReadEventDetails(HtmlDocument doc)
        {
            // Read event table for event details

            //HtmlNode eventTable = doc.DocumentNode.SelectNodes(eventTableTag)[0];

            try
            {
                foreach (HtmlNode eventTable in doc.DocumentNode.SelectNodes(eventTableTag))
                {
                    HtmlAttributeCollection attributes = eventTable.Attributes;

                    foreach (HtmlAttribute dateAttribute in attributes.AttributesWithName(dateTag))
                        dateString = dateAttribute.Value;

                    foreach (HtmlAttribute eventAttribute in attributes.AttributesWithName(eventTag))
                        eventName = eventAttribute.Value;

                    foreach (HtmlAttribute betTypeAttribute in attributes.AttributesWithName(betTag))
                        betType = betTypeAttribute.Value;

                    foreach (HtmlAttribute sportAttribute in attributes.AttributesWithName(sportTag))
                        sport = sportAttribute.Value;

                    foreach (HtmlAttribute sportBracketAttribute in attributes.AttributesWithName(sportBracketTag))
                        sportBracket = sportBracketAttribute.Value;
                }
            }
            catch
            {

            }

            if (dateString != "")
            {
                try
                {
                    eventTime = Convert.ToDateTime(dateString);
                    if (eventTime < DateTime.Now) inPlay = true;
                }
                catch
                {

                }

                timeToEvent = eventTime - DateTime.Now;
            }
        }

        //private bool ParseHTML()
        //{
        //    //outputLog.Add("Parsing HTML source");

        //    // Find event time if available and store it - <span class="date">Tuesday 31st March / 19:45</span>

        //    if (source.Contains(DATE_IDENTIFIER_START))
        //    {
        //        eventTime = ExtractDateTime();
        //        timeToEvent = eventTime - DateTime.Now;
        //    }

        //    // Find eventTableRows and split them into each result, if does not exist set bool

        //    if (source.Contains(EVENT_IDENTIFIER_START))
        //    {
        //        GetEventDetails(); // Attempt to populate event name and bet type

        //        List<string> eventsSplitList = EventsSplitList();

        //        if (eventsSplitList.Count > 1)
        //        {
        //            // If the number of results is two or more seek odds for enabled bookmakers
        //            foreach (string eventSource in eventsSplitList)
        //            {
        //                results.Add(new Result(eventSource, bookmakers));
        //            }

        //            // Check that enabled bookmakers provide odds for every result path
        //            foreach (Result result in results)
        //            {
        //                if (result.hasResult == false)
        //                {
        //                    return false;
        //                }
        //            }

        //            // Odds are available for enabled bookmakers in each result path, return true
        //            return true;
        //        }
        //        else return false;
        //    }
        //    else return false;

        //}

        //private void GetEventDetails()
        //{
        //    // Check there are two slashes, so that Event Name and Bet Type may be extracted from URL
        //    int slashCount = URL.Split(SLASH).Length - 1;
        //    if (slashCount > 1)
        //    {
        //        string tempURL = URL;

        //        int lastSlash = tempURL.LastIndexOf(SLASH);
        //        betType = tempURL.Substring(lastSlash + 1);

        //        tempURL = tempURL.Substring(0, lastSlash);

        //        lastSlash = tempURL.LastIndexOf(SLASH);
        //        eventName = tempURL.Substring(lastSlash + 1);

        //        // Replace dashes with spaces
        //        eventName = eventName.Replace(WORD_SEPARATOR_URL, WORD_SEPARATOR_NEW);
        //        betType = betType.Replace(WORD_SEPARATOR_URL, WORD_SEPARATOR_NEW);
        //    }
        //}

        //private DateTime ExtractDateTime()
        //{
        //    // Find start and end of date then extract value and store as string
        //    int start = source.IndexOf(DATE_IDENTIFIER_START) + DATE_IDENTIFIER_START.Length;
        //    int end = source.IndexOf(DATE_IDENTIFIER_END, start);
        //    int dateLength = end - start;
        //    dateString = source.Substring(start, dateLength);

        //    // First part of date is the day, not required as we have day of the month
        //    dateString = dateString.Substring(dateString.IndexOf(" ") + 1);

        //    // Now first part of date string is day of month, store this value
        //    string dayOfMonth = dateString.Substring(0, dateString.IndexOf(" "));

        //    string temp = dayOfMonth;
        //    // Remove any letters at the end of the day of the month
        //    for (int i = 0; i < dayOfMonth.Length; i++)
        //    {
        //        try
        //        {
        //            string s = temp.Substring(i, 1);
        //            Convert.ToDouble(s);
        //        }
        //        catch (FormatException) // Cannot be converted to double, delete all characters from this point onwards
        //        {
        //            dayOfMonth = dayOfMonth.Remove(i);
        //        }
        //    }

        //    // Remove day of month from date string
        //    dateString = dateString.Substring(dateString.IndexOf(" ") + 1);

        //    // Next part of date is month
        //    string month = dateString.Substring(0, dateString.IndexOf(" "));

        //    // Remove month and slash from date string, leaving time
        //    dateString = dateString.Substring(dateString.IndexOf(" ") + 1);
        //    dateString = dateString.Substring(dateString.IndexOf(" ") + 1);

        //    // Now hour
        //    string hour = dateString.Substring(0, dateString.IndexOf(":"));

        //    // Remove hour from date string
        //    dateString = dateString.Substring(dateString.IndexOf(":") + 1);

        //    // Only minute is left, store in string
        //    string minute = dateString;

        //    // Create new datetime object and store each value

        //    DateTime dateTime = DateTime.MinValue;

        //    dateTime = dateTime.AddMinutes(Convert.ToDouble(minute));
        //    dateTime = dateTime.AddHours(Convert.ToDouble(hour));
        //    dateTime = dateTime.AddDays(Convert.ToDouble(dayOfMonth) - 1);
        //    dateTime = dateTime.AddMonths(DateTime.ParseExact(month, "MMMM", CultureInfo.CurrentCulture).Month - 1);
        //    dateTime = dateTime.AddYears(DateTime.Now.Year - 1);

        //    if (dateTime < DateTime.Now) inPlay = true;

        //    return dateTime;
        //}

        //private List<string> EventsSplitList()
        //{
        //    List<string> eventsSplitList = new List<string>();

        //    string sourceEdited = source;

        //    // Split the source for each event into separate strings
        //    while (sourceEdited.Contains((EVENT_IDENTIFIER_START)))
        //    {
        //        // Work out how long the event is
        //        int eventStart = sourceEdited.IndexOf(EVENT_IDENTIFIER_START);
        //        int eventEnd = sourceEdited.IndexOf(EVENT_IDENTIFIER_END, eventStart);
        //        int eventLength = (eventEnd + EVENT_IDENTIFIER_END.Length) - eventStart;

        //        // Store substring of event
        //        string eventSubstring = sourceEdited.Substring(eventStart, eventLength);

        //        // Add substring to list
        //        eventsSplitList.Add(eventSubstring);

        //        // Remove extracted event from sourceEdited string, ready for next iteration
        //        sourceEdited = sourceEdited.Substring(eventEnd);

        //        //MessageBox.Show(eventSubstring);

        //    }

        //    return eventsSplitList;
        //}

        #endregion

        #region Core Logic

        private bool CheckIfProfitable()
        {
            int numberOfItems = results.Count;

            foreach (Result result in results)
            {
                result.chance = 1 / result.odds;
                totalChance += result.chance;
            }

            if (totalChance < 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void CalculateReturn(double stake)
        {
            // Scale chance as adjusted chances and store in values list

            double chanceAdjustment = 1 / totalChance;

            double difference = 0d;
            double minReturn = 0, maxReturn = 0;

            int i = 0;
            foreach (Result result in results)
            {
                result.chanceAdjusted = result.chance * chanceAdjustment;

                // Work out each bet amount based on stake and adjusted chance
                result.bet = result.chanceAdjusted * stake;
                result.bet -= difference; // Take on previous bet difference

                // Calculate difference between what must be bet and actual value, so difference may be carried over
                difference = Math.Round(result.bet, 2) - result.bet;

                result.bet = Math.Round(result.bet, 2);

                result.betReturn = result.odds * result.bet;
                result.betReturn = Math.Round(result.betReturn, 2);

                if (i == 0)
                {
                    minReturn = result.betReturn;
                    maxReturn = result.betReturn;
                }
                else
                {
                    if (result.betReturn < minReturn) minReturn = result.betReturn;
                    if (result.betReturn > maxReturn) maxReturn = result.betReturn;
                }

                i++;
            }

            profit = minReturn - stake;
            profitPercentage = (profit / stake) * 100;
        }
        #endregion

        #region Public Methods

        public override string ToString()
        {
            string output = "";
            string date = string.Format("{0:yyyy MM dd HHmm}", eventTime);
            output = "\nEvent name: " + eventName;
            output += "\nDate: " + date;
            output += "\nType: " + betType;
            output += "\nMinimum profit: " + profit;
            output += "\nBets:\n";

            foreach (Result res in results)
            {
                string line = string.Format("\nPut {0} on {1} at {4} to return {2} with {3}.", res.bet, res.name, res.betReturn, res.bookmaker.Name, res.odds);
                output += line;
            }

            output += "\nURL: " + URL;

            return output;
        }
        #endregion

    }
}
