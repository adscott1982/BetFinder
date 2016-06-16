using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Net;

namespace Bet_Finder
{
    public partial class BetFinder : Form
    {
        #region Class Variables

        // File paths

        string baseURLsPath = "BaseURLs.txt";
        string settingsPath = "Settings.ini";
        string oddsPath = "Odds.ods";

        // Lists
        List<Bookmaker> bookmakers = new List<Bookmaker>();
        List<Bookmaker> enabledBookmakers = new List<Bookmaker>();
        List<RetrievedOdds> oddsList = new List<RetrievedOdds>();
        List<RetrievedOdds> filteredOddsList;
        List<string> baseURLs = new List<string>();
        List<LinkLabel> resultLinks = new List<LinkLabel>();

        // Default settings
        double stake = 100d;
        int maxUniqueBets = 3;
        int maxLinkCheckThreads = 50;
        int maxHrs = 24;
        bool maxHrsEnabled = true;
        bool includeInPlay = false;
        double minProfit = 0d;
        string dateFormat = "dd MMMM yyyy - HH:mm";

        // Logic variables
        int linkVerticalSep = 15;
        bool initComplete = false;
        enum SortTypes { Profit, Bets, Time, Event, Type };
        SortTypes sortType = SortTypes.Profit;

        // Objects
        LinkChecker linkChecker;
        RetrievedOdds selectedOdds;
        int selectedOddsIndex;

        #endregion

        #region Constructor and Initialization

        public BetFinder()
        {
            InitializeComponent();
            CreateBookmakers(bookmakers);
            LoadFiles();
            initComplete = true;
        }

        public void LoadFiles()
        {
            LoadBaseURLs();
            SetDefaultFormValues();
        }

        public void LoadBaseURLs()
        {
            if (File.Exists(baseURLsPath))
            {
                baseURLs = new List<string>(File.ReadAllLines(baseURLsPath));
            }
            else
            {
                SetBaseUrls();
            }
        }

        public void SetDefaultFormValues()
        {
            stakeControl.Value = (decimal)stake;
            maxUniqueBetsControl.Value = maxUniqueBets;
            maxThreadsControl.Value = maxLinkCheckThreads;
            maxHoursControl.Value = maxHrs;
            maxHoursEnabledCheckBox.Checked = maxHrsEnabled;
            maxHoursControl.Enabled = maxHrsEnabled;
            inPlayControl.Checked = includeInPlay;
            minProfitControl.Value = (decimal)minProfit;
        }

        void SetBaseUrls()
        {
            baseURLs.Add("http://www.oddschecker.com");
            baseURLs.Add("http://www.oddschecker.com/american-football/nfl");
            baseURLs.Add("http://www.oddschecker.com/baseball/mlb");
            baseURLs.Add("http://www.oddschecker.com/darts/darts-coupon");
            baseURLs.Add("http://www.oddschecker.com/rugby-union/uk-ireland/aviva-premiership");
            baseURLs.Add("http://www.oddschecker.com/rugby-union/internationals");
            baseURLs.Add("http://www.oddschecker.com/boxing");
            baseURLs.Add("http://www.oddschecker.com/kickboxing");
            baseURLs.Add("http://www.oddschecker.com/ufc-mma");
            baseURLs.Add("http://www.oddschecker.com/snooker/snooker-coupon");
            baseURLs.Add("http://www.oddschecker.com/ice-hockey/nhl");
            baseURLs.Add("http://www.oddschecker.com/handball/handball-coupon");
            baseURLs.Add("http://www.oddschecker.com/hockey/all-matches");
            baseURLs.Add("http://www.oddschecker.com/australian-rules/afl");
            baseURLs.Add("http://www.oddschecker.com/squash/tournament-of-champions");
            baseURLs.Add("http://www.oddschecker.com/badminton");
            baseURLs.Add("http://www.oddschecker.com/cricket");
            baseURLs.Add("http://www.oddschecker.com/pool/us-open");
            baseURLs.Add("http://www.oddschecker.com/gaelic-games/gaelic-football");
            baseURLs.Add("http://www.oddschecker.com/volleyball/volleyball-coupon");
            baseURLs.Add("http://www.oddschecker.com/rugby-league/super-league");
            baseURLs.Add("http://www.oddschecker.com/darts/grand-slam-of-darts");
            baseURLs.Add("http://www.oddschecker.com/basketball/match-coupon");
            baseURLs.Add("http://www.oddschecker.com/football");
            baseURLs.Add("http://www.oddschecker.com/football/football-coupons/major-leagues-cups");
            baseURLs.Add("http://www.oddschecker.com/football/english/premier-league");
            baseURLs.Add("http://www.oddschecker.com/football/english/championship");
            baseURLs.Add("http://www.oddschecker.com/football/english/league-1");
            baseURLs.Add("http://www.oddschecker.com/football/english/league-2");
            baseURLs.Add("http://www.oddschecker.com/football/scottish/premiership");
            baseURLs.Add("http://www.oddschecker.com/football/scottish/championship");
            baseURLs.Add("http://www.oddschecker.com/football/scottish/league-1");
            baseURLs.Add("http://www.oddschecker.com/football/scottish/league-2");
            baseURLs.Add("http://www.oddschecker.com/football/elite-coupon");
            baseURLs.Add("http://www.oddschecker.com/football/france/ligue-1");
            baseURLs.Add("http://www.oddschecker.com/football/france/ligue-2");
            baseURLs.Add("http://www.oddschecker.com/football/germany/bundesliga");
            baseURLs.Add("http://www.oddschecker.com/football/germany/bundesliga-2");
            baseURLs.Add("http://www.oddschecker.com/football/italy/serie-a");
            baseURLs.Add("http://www.oddschecker.com/football/italy/serie-b");
            baseURLs.Add("http://www.oddschecker.com/football/spain/la-liga-primera");
            baseURLs.Add("http://www.oddschecker.com/football/spain/la-liga-segunda");
            baseURLs.Add("http://www.oddschecker.com/football/euro-2016");
            baseURLs.Add("http://www.oddschecker.com/football/international-friendlies");
            baseURLs.Add("http://www.oddschecker.com/tennis");
            baseURLs.Add("http://www.oddschecker.com/tennis/match-coupon");

            // SPECIALS

            baseURLs.Add("http://www.oddschecker.com/golf/the-rsm-classic");
            baseURLs.Add("http://www.oddschecker.com/golf/dp-world-tour-championship");
            baseURLs.Add("http://www.oddschecker.com/tennis/atp-world-tour-finals");
            baseURLs.Add("http://www.oddschecker.com/darts/world-series-of-darts");
        }

        #endregion

        #region Bookmaker Settings and List Logic

        private void CreateBookmakers(List<Bookmaker> bookmakers)
        {
            // Create bookmakers
            bookmakers.Add(new Bookmaker("B3", "Bet365", true, 10, ""));
            bookmakers.Add(new Bookmaker("SK", "skyBET", true, 10, ""));
            bookmakers.Add(new Bookmaker("BX", "totesport", true, 5, ""));
            bookmakers.Add(new Bookmaker("BY", "BoyleSports3D", true, 4, ""));
            bookmakers.Add(new Bookmaker("FR", "BETFRED", true, 9, ""));
            bookmakers.Add(new Bookmaker("SO", "sportingbet", true, 5, ""));
            bookmakers.Add(new Bookmaker("VC", "Bet Victor", true, 5, ""));
            bookmakers.Add(new Bookmaker("PP", "Paddy Power", true, 10, ""));
            bookmakers.Add(new Bookmaker("SJ", "Stan James", false, 10, ""));
            bookmakers.Add(new Bookmaker("EE", "888sport", true, 9, ""));
            bookmakers.Add(new Bookmaker("LD", "Ladbrokes", true, 10, ""));
            bookmakers.Add(new Bookmaker("CE", "Coral", true, 10, ""));
            bookmakers.Add(new Bookmaker("WH", "William Hill", true, 10, ""));
            bookmakers.Add(new Bookmaker("WN", "Winner4D", true, 4, ""));
            bookmakers.Add(new Bookmaker("SX", "SpreadEx", false, 5, ""));
            bookmakers.Add(new Bookmaker("FB", "betfair", false, 10, ""));
            bookmakers.Add(new Bookmaker("WA", "Betway", true, 8, ""));
            bookmakers.Add(new Bookmaker("BB", "Bet Bright", false, 5, ""));
            bookmakers.Add(new Bookmaker("TI", "Titan Bet", false, 5, ""));
            bookmakers.Add(new Bookmaker("UN", "Unibet", true, 5, ""));
            bookmakers.Add(new Bookmaker("BW", "bwin", true, 9, ""));
            bookmakers.Add(new Bookmaker("RD", "32Red", true, 5, ""));
            bookmakers.Add(new Bookmaker("RB", "RaceBets", false, 5, ""));
            bookmakers.Add(new Bookmaker("OE", "10Bet", false, 5, ""));
            bookmakers.Add(new Bookmaker("MR", "Marathon Bet", false, 5, ""));
            bookmakers.Add(new Bookmaker("BF", "Betfair EXCHANGE", false, 5, ""));
            bookmakers.Add(new Bookmaker("BD", "BetDAQ", false, 5, ""));
            bookmakers.Add(new Bookmaker("MA", "Matchbook", false, 5, ""));

            // Display bookmakers

            foreach (Bookmaker bookie in bookmakers)
            {
                bookMakersListBox.Items.Add(bookie.Name, bookie.Enabled);
            }

            // Set enabled bookmakers list
            UpdateEnabledBookMakersList();
        }

        private void UpdateEnabledBookMakersList()
        {
            // Go through list of bookmakers and add only those that are enabled to list of enabled bookmakers

            enabledBookmakers.Clear();

            foreach (Bookmaker bookie in bookmakers)
            {
                if (bookie.Enabled)
                {
                    enabledBookmakers.Add(bookie);
                }
            }
        }

        #endregion

        #region Form Events

        private void maxHoursEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            bool checkBoxState = maxHoursEnabledCheckBox.Checked;
            maxHrsEnabled = checkBoxState;

            if(maxHrsEnabled)
            {
                maxHrs = (int)maxHoursControl.Value;
            }
            else
            {
                maxHrs = (int)(DateTime.MaxValue - DateTime.Now).TotalHours;
            }

            maxHoursControl.Enabled = maxHrsEnabled;

            FilterChanged();
        }

        private void FilterChanged()
        {
            UpdateOddsList(oddsList);
        }

        private void bookMakersListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked)
            {
                bookmakers[e.Index].Enabled = true;
            }
            else
            {
                bookmakers[e.Index].Enabled = false;
            }

            // Set enabled bookmakers list
            UpdateEnabledBookMakersList();
            FilterChanged();
        }

        private void findOddsButton_Click(object sender, EventArgs e)
        {
            findOddsButton.Enabled = false;
            stopButton.Enabled = true;

            statusLabel.Text = "Started, awaiting update...";
            linkChecker = new LinkChecker(this, bookmakers, (int)maxThreadsControl.Value, baseURLs);

            backgroundWorker.RunWorkerAsync(linkChecker);
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            if (backgroundWorker.IsBusy)
            {
                backgroundWorker.CancelAsync();
            }

            stopButton.Enabled = false;
            statusLabel.Text = "Stopping...";
        }

        private void URLTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;

            if (index < 0 || index > filteredOddsList.Count) return;

            selectedOddsIndex = index;
            PopulateEventLabels(filteredOddsList[index]);
        }

        private void eventRefreshButton_Click(object sender, EventArgs e)
        {
            eventRefreshButton.Enabled = false;

            //HtmlAgilityPack.HtmlDocument doc;
            //doc = LoadSource(GetPage(selectedOdds.URL));

            string source = linkChecker.GetPage(selectedOdds.URL);

            if (source == null)
            {
                source = selectedOdds.source;
                PopulateEventLabels(selectedOdds);
            }
            else
            {
                RetrievedOdds tempOdds = new RetrievedOdds(selectedOdds.URL, source, enabledBookmakers, stake);
                filteredOddsList[selectedOddsIndex] = tempOdds;
                PopulateEventLabels(tempOdds);
                UpdateOddsList(oddsList);
            }

            eventRefreshButton.Enabled = true;
        }

        private void PopulateEventLabels(RetrievedOdds odds)
        {
            selectedOdds = odds;
            eventRefreshButton.Enabled = true;

            string sport = odds.sport;
            string sportBracket = odds.sportBracket;
            string name = odds.eventName;
            string type = odds.betType;
            string time = odds.eventTime.ToString(dateFormat);
            string profit = string.Format("{0:0.00}", odds.profit);
            string oddsCheckerlink = odds.URL;

            eventSportLabel.Text = sport + ", " + sportBracket;
            eventNameTypeLabel.Text = name + ", " + type;
            eventTimeLabel.Text = time;
            eventMinProfitLabel.Text = "Minimum Profit: " + profit;

            oddsCheckerLinkLabel.Enabled = true;
            oddsCheckerLinkLabel.Links[0].LinkData = oddsCheckerlink;

            ClearResultLinks();

            int i = 1;
            foreach(Result res in odds.results)
            {
                string text = string.Format("\nPut {0:0.00} on {1} at {4} to return {2:0.00} with {3}.", res.bet, res.name, res.betReturn, res.bookmaker.Name, res.odds);
                string URL = "https://www.google.com/search?q=";
                URL += res.bookmaker.Name + "+" + odds.eventName + "+" + odds.betType;

                LinkLabel linkLabel = new LinkLabel();

                int horizontalPosition = oddsCheckerLinkLabel.Location.X;
                int verticalPosition = oddsCheckerLinkLabel.Location.Y + (linkVerticalSep * i);
                i++;

                linkLabel.Location = new System.Drawing.Point(horizontalPosition, verticalPosition);
                linkLabel.Size = new System.Drawing.Size(224, 16);
                linkLabel.AutoSize = true;
                linkLabel.LinkBehavior = LinkBehavior.NeverUnderline;
                linkLabel.Text = text;
                linkLabel.LinkArea = new LinkArea(0, text.Length);
                linkLabel.Links[0].LinkData = URL;

                linkLabel.LinkClicked += new LinkLabelLinkClickedEventHandler(LinkClicked);

                resultLinks.Add(linkLabel);
                Controls.Add(linkLabel);
            }
        }

        private void ClearResultLinks()
        {
            foreach (LinkLabel linkLabel in resultLinks)
            {
                Controls.Remove(linkLabel);
                linkLabel.Dispose();
            }

            resultLinks = new List<LinkLabel>();
        }

        private void LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string target = e.Link.LinkData as string;
            Process.Start(target);
        }

        private void resultsTable_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int columnNumber = e.ColumnIndex;
            sortType = (SortTypes)columnNumber;
            UpdateOddsList(oddsList);
        }

        private void FilterValueChanged(object sender, EventArgs e)
        {
            if (initComplete)
            {
                stake = (double)stakeControl.Value;
                maxUniqueBets = (int)maxUniqueBetsControl.Value;
                includeInPlay = inPlayControl.Checked;
                if (maxHrsEnabled) maxHrs = (int)maxHoursControl.Value;
                minProfit = (double)minProfitControl.Value;
            }

            FilterChanged();

        }

        private void recalcButton_Click(object sender, EventArgs e)
        {
            double ratio = (double)recalcMaximumBet.Value / (double)recalcOriginalBet.Value;
            double newStake = ratio * (double)stakeControl.Value;

            newStake = RoundDown(newStake, 2);

            recalcLabel.Text = string.Format("New stake = {0:0.00}", newStake);
        }

        #endregion

        #region Table Management

        private void RefreshTable()
        {
            UpdateOddsList(oddsList);
        }

        private void UpdateTable(DataGridView grid)
        {
            DataTable table = new DataTable();

            table.Columns.Add("Profit", typeof(string));
            table.Columns.Add("No. of Bets", typeof(string));
            table.Columns.Add("Time and Date", typeof(string));
            table.Columns.Add("Event Name", typeof(string));
            table.Columns.Add("Bet Type", typeof(string));

            foreach (RetrievedOdds odds in filteredOddsList)
            {
                DataRow row = table.NewRow();

                string time;

                if (odds.eventTime < DateTime.Now) time = "In Play";
                else if (odds.eventTime == DateTime.MaxValue) time = "No date set";
                else time = odds.eventTime.ToString(dateFormat);

                string profit = string.Format("{0:0.00}", odds.profit);

                row.SetField<string>(table.Columns[0], profit);
                row.SetField<string>(table.Columns[1], odds.results.Count.ToString());
                row.SetField<string>(table.Columns[2], time);
                row.SetField<string>(table.Columns[3], odds.eventName);
                row.SetField<string>(table.Columns[4], odds.betType);

                table.Rows.Add(row);
            }

            SafeInvokeFormControl(grid, () => {
                grid.DataSource = table;
                grid.AutoResizeColumns();

                foreach (DataGridViewColumn column in grid.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                }

                grid.Refresh();
            });
        }

        #endregion

        #region Oddslist Handling

        internal void UpdateOddsList(List<RetrievedOdds> oddsList)
        {
            this.oddsList = oddsList.ToList();
            filteredOddsList = FilterOddsList(oddsList);
            SortOddsList(sortType);
            UpdateTable(resultsTable);
        }

        private List<RetrievedOdds> FilterOddsList(List<RetrievedOdds> oddsList)
        {
            List<RetrievedOdds> tempOddsList = new List<RetrievedOdds>();
            foreach (RetrievedOdds odds in oddsList)
            {
                if (odds.isProfitable)
                {
                    string source = odds.source;

                    string URL = odds.URL;

                    double tempStake = stake;
                    SafeInvokeFormControl(stakeControl, () => { tempStake = (double)stakeControl.Value; });

                    double tempProfitPC = minProfit;
                    SafeInvokeFormControl(minProfitControl, () => { tempProfitPC = (double)minProfitControl.Value; });

                    int tempHours = maxHrs;
                    SafeInvokeFormControl(maxHoursControl, () => { tempHours = (int)maxHoursControl.Value; });

                    int tempBets = maxUniqueBets;
                    SafeInvokeFormControl(maxUniqueBetsControl, () => { tempBets = (int)stakeControl.Value; });

                    bool tempInPlay = includeInPlay;
                    SafeInvokeFormControl(inPlayControl, () => { tempInPlay = inPlayControl.Checked; });

                    RetrievedOdds tempOdds = new RetrievedOdds(URL, source, enabledBookmakers.ToList(), tempStake);

                    bool include = true;

                    if (!includeInPlay && tempOdds.inPlay) include = false;
                    if (tempOdds.timeToEvent.TotalHours > maxHrs) include = false;
                    if (tempOdds.results.Count > maxUniqueBets) include = false;
                    if (tempOdds.profitPercentage < minProfit) include = false;

                    if (include)
                    {
                        tempOddsList.Add(tempOdds);
                    }
                }
            }

            return tempOddsList;
        }

        private void SortOddsList(SortTypes type)
        {
            switch (type)
            {
                case SortTypes.Profit:
                    filteredOddsList.Sort((a, b) => b.profit.CompareTo(a.profit));
                    break;
                case SortTypes.Bets:
                    filteredOddsList.Sort((b, a) => b.results.Count.CompareTo(a.results.Count));
                    break;
                case SortTypes.Time:
                    filteredOddsList.Sort((b, a) => b.eventTime.CompareTo(a.eventTime));
                    break;
                case SortTypes.Event:
                    filteredOddsList.Sort((b, a) => b.eventName.CompareTo(a.eventName));
                    break;
                case SortTypes.Type:
                    filteredOddsList.Sort((b, a) => b.betType.CompareTo(a.betType));
                    break;
            }
        }

        #endregion

        #region Background Worker

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            LinkChecker lChkr = e.Argument as LinkChecker;

            while (lChkr.linksList.Count > lChkr.i && !backgroundWorker.CancellationPending)
            {
                lChkr.CheckLinks();
            }

            MessageBox.Show("Stopped checking links");
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            findOddsButton.Enabled = true;
            stopButton.Enabled = false;
        }

        #endregion

        #region Status Updates

        public void UpdateStatus(string status)
        {
            SafeInvokeFormControl(statusLabel, () => { statusLabel.Text = status; });
        }

        public void LinkChecked(int i, int j)
        {
            string label = "Checked " + i.ToString() + " / " + j.ToString();

            SafeInvokeFormControl(linkCheckStatusLabel, () => { linkCheckStatusLabel.Text = label; });
        }

        #endregion



        #region Utility

        public void SafeInvokeFormControl(Control control, Action action)
        {
            if (control.InvokeRequired)
                control.Invoke(new MethodInvoker(() => { action(); }));
            else
                action();
        }

        public double RoundDown(double i, double decimalPlaces)
        {
            var power = Math.Pow(10, decimalPlaces);
            return Math.Floor(i * power) / power;
        }

        public string GetPage(string text)
        {
            WebClient webClient = new WebClient();
            webClient.Headers.Add("user-agent", "MOZILLA/5.0 (WINDOWS NT 6.1; WOW64) APPLEWEBKIT/537.1 (KHTML, LIKE GECKO) CHROME/21.0.1180.75 SAFARI/537.1");
            webClient.Headers.Add(HttpRequestHeader.Cookie, "odds_type=decimal");

            try
            {
                return webClient.DownloadString(text);
            }
            catch
            {
                return null;
            }
        }

        public HtmlAgilityPack.HtmlDocument LoadSource(string source)
        {
            if (source == null) return null;

            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();

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

        #endregion


    }
}
