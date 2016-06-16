using System;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace Bet_Finder
{
    class LinkChecker
    {
        // Received settings
        BetFinder form;
        List<Bookmaker> bookMakers;
        int linkCheckThreads;
        double stake = 1000d;

        // Internal variables
        string linksPath = "" + string.Format("{0:yyMMdd HHmm}", DateTime.Now) + " Links.txt";
        string oddsPath = "" + string.Format("{0:yyMMdd HHmm}", DateTime.Now) + " Odds.txt";
        string baseURL;

        public int i = 0;

        // Lists

        public List<string> linksList = new List<string>();
        List<string> exclusions = new List<string>();
        List<string> inclusions = new List<string>();
        List<RetrievedOdds> oddsList = new List<RetrievedOdds>();

        // Objects
        Stopwatch sw = new Stopwatch();
        HtmlWeb hw = new HtmlWeb { UseCookies = true };

        #region Constructor

        // Constructor
        public LinkChecker(BetFinder form, List<Bookmaker> bookMakers, int linkCheckThreads, List<string> baseURLs)
        {
            this.form = form;
            this.bookMakers = bookMakers;
            this.linkCheckThreads = linkCheckThreads;
            linksList = baseURLs.ToList();
            baseURL = linksList[0];
            sw.Start();

            SetInclusions();
            SetExclusions();
        }

        #endregion

        #region Exclusions, Inclusions

        void SetExclusions()
        {
            exclusions.Add("bet-history");
            exclusions.Add("bookie_offer");
            exclusions.Add("?selectionName");
            exclusions.Add("bet-activity");
            exclusions.Add("clickout.htm?");
            exclusions.Add("market-reports");
            exclusions.Add("horse-racing");
            exclusions.Add("greyhounds");

            // Test

            //exclusions.Add("/football/");

            // bet types to exclude due to naming overlap:

            foreach(string inclusion in inclusions)
            {
                exclusions.Add(inclusion + "-");
            }

            //exclusions.Add("/betting-markets-");

            //exclusions.Add("/winner-");
            //exclusions.Add("/half-time-");
            //exclusions.Add("/both-teams-to-score-");
            //exclusions.Add("/team-to-score-first-");
            //exclusions.Add("/to-qualify-");
            //exclusions.Add("/set-1-winner-");

            //exclusions.Add("/second-half-result-");
            //exclusions.Add("/highest-scoring-half-");
            //exclusions.Add("/both-teams-to-score-in-1st-half-");
            //exclusions.Add("/both-teams-to-score-in-2nd-half-");
            //exclusions.Add("/team-to-score-last-");
            //exclusions.Add("/total-goals-odd-even-");
            //exclusions.Add("/total-goals-odd-even-1st-half-");
            //exclusions.Add("/total-points-odd-even-");
            //exclusions.Add("/1st-half-points-odd-even-");
            //exclusions.Add("/2nd-half-points-odd-even-");
            //exclusions.Add("/set-2-winner-");
        }

        void SetInclusions()
        {
            inclusions.Add("/winner");
            inclusions.Add("/half-time");
            inclusions.Add("/second-half-result");
            inclusions.Add("/to-qualify");

            inclusions.Add("/team-to-score-first");
            inclusions.Add("/team-to-score-last");
            
            inclusions.Add("/highest-scoring-half");
            inclusions.Add("/set-1-winner");
            inclusions.Add("/set-2-winner");
            inclusions.Add("/set-1-tie-break");
            inclusions.Add("/set-2-tie-break");
            //inclusions.Add("");

            inclusions.Add("/betting-markets");

            inclusions.Add("/both-teams-to-score");
            inclusions.Add("/both-teams-to-score-in-1st-half");
            inclusions.Add("/both-teams-to-score-in-2nd-half");

            inclusions.Add("/total-goals-odd-even");
            inclusions.Add("/total-points-odd-even");
            inclusions.Add("/total-goals-odd-even-1st-half");
            inclusions.Add("/1st-half-points-odd-even");
            inclusions.Add("/2nd-half-points-odd-even");
            inclusions.Add("/1st-period-both-teams-to-score");
            inclusions.Add("/2nd-period-both-teams-to-score");
            inclusions.Add("/3rd-period-both-teams-to-score");

            // new

            inclusions.Add("/home-team-to-win-both-halves");
            inclusions.Add("/home-team-to-win-to-nil");
            inclusions.Add("/away-team-to-win-both-halves");
            inclusions.Add("/away-team-to-win-to-nil");
            inclusions.Add("/goal-in-both-halves");
            inclusions.Add("/away-team-to-score-in-both-halves");
            inclusions.Add("/home-team-to-score-in-both-halves");
            inclusions.Add("/away-team-to-score");
            inclusions.Add("/home-team-to-score");
            inclusions.Add("/overtime");
        }

        #endregion

        public string GetPage(string url)
        {

            WebClient webClient = new WebClient();
            webClient.Headers.Add("user-agent", "MOZILLA/5.0 (WINDOWS NT 6.1; WOW64) APPLEWEBKIT/537.1 (KHTML, LIKE GECKO) CHROME/21.0.1180.75 SAFARI/537.1");
            webClient.Headers.Add(HttpRequestHeader.Cookie, "odds_type=decimal");

            try
            {
                return webClient.DownloadString(url);
            }
            catch
            {
                return "";
            }
        }

        public void CheckLinks()
        {
            WebRequest.DefaultWebProxy = null;
            ServicePointManager.DefaultConnectionLimit = linkCheckThreads;

            int linksRemainingCount = linksList.Count - i;
            int linksCheckingCount = linksRemainingCount;
            if (linksCheckingCount > linkCheckThreads) linksCheckingCount = linkCheckThreads;
            int linksChecked = 0;

            Parallel.For(0, linksCheckingCount, j =>
            {
                try
                {
                    GetUniqueLinks(linksList[i + j]);
                }
                catch
                {

                }
                
                linksChecked++;
                form.LinkChecked(linksChecked, linksCheckingCount);
            });

            RemoveDuplicates(linksList);

            i += linksCheckingCount;

            File.WriteAllLines(linksPath, linksList);

            form.UpdateOddsList(oddsList);
            ShowStatus(i);
        }

        private void ShowStatus(int linksChecked)
        {
            int totalLinks = linksList.Count;
            int profitableBets = 0;
            int hadSource = 0;

            foreach (RetrievedOdds odds in oddsList)
            {
                if (odds.isProfitable) profitableBets++;
                if (odds.hadSource) hadSource++;
            }

            float elapsedTime = (int)Math.Round(sw.ElapsedMilliseconds / 1000f);
            string output = string.Format("Total links found: {0}   Checked: {1}   Time(s): {2}   Loaded source: {3} / {4}   Profitable bets: {5}", totalLinks, linksChecked, elapsedTime, hadSource, oddsList.Count, profitableBets);
            form.UpdateStatus(output);
        }

        private void RemoveDuplicates(List<string> list)
        {
            list = list.Distinct().ToList();
        }

        public HtmlDocument LoadSource(string source)
        {
            if (source == null) return null;

            HtmlDocument doc = new HtmlDocument();

            try
            {
                doc.LoadHtml(source);
            }
            catch
            {
                Console.WriteLine("Bad URL");
                return null;
            }

            return doc;
        }

        private void GetUniqueLinks(string url)
        {
            int maxRetries = 3;
            int attempts = 0;

            string source = "";
            while (source == "" && attempts < maxRetries)
            {
                source = GetPage(url);
                attempts++;
            }

            HtmlDocument doc = LoadSource(source);
            //if (doc == null) return;

            if (doc.DocumentNode.SelectNodes("//a[@href]") != null)
            {
                foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//a[@href]"))
                {
                    HtmlAttribute att = link.Attributes["href"];
                    string linkString = att.Value;

                    if (linkString.StartsWith("/"))
                    {
                        linkString = baseURL + linkString;
                    }

                    if (!linksList.Contains(linkString))
                    {
                        bool excluded = false;
                        bool included = false;

                        foreach (string exclusion in exclusions)
                        {
                            if (linkString.Contains(exclusion)) excluded = true;
                        }

                        foreach (string inclusion in inclusions)
                        {
                            if (linkString.Contains(inclusion)) included = true;
                        }

                        if (linkString.StartsWith(baseURL) && !excluded && included)
                        {
                            linksList.Add(linkString);
                            int index = linksList.Count;
                        }
                    }
                }
            }

            // TODO: Work out exception

            try
            {
                RetrievedOdds odds = new RetrievedOdds(url, source, bookMakers, stake);

                List<string> output = new List<string>();
                oddsList.Add(odds);
            }
            catch (Exception e)
            {
                string message = string.Format("Exception retriving odds for URL: {0}\nException details: {1}", url, e.ToString());
                System.Windows.Forms.MessageBox.Show(message);
            }
        }
    }
}
