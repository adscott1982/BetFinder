namespace Bet_Finder
{
    partial class BetFinder
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BetFinder));
            this.maxUniqueBetsControl = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.stakeControl = new System.Windows.Forms.NumericUpDown();
            this.bookMakersListBox = new System.Windows.Forms.CheckedListBox();
            this.resultsTable = new System.Windows.Forms.DataGridView();
            this.findOddsButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.maxThreadsControl = new System.Windows.Forms.NumericUpDown();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.statusLabel = new System.Windows.Forms.Label();
            this.stopButton = new System.Windows.Forms.Button();
            this.linkCheckStatusLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.maxHoursControl = new System.Windows.Forms.NumericUpDown();
            this.inPlayControl = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.minProfitControl = new System.Windows.Forms.NumericUpDown();
            this.maxHoursEnabledCheckBox = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.eventNameTypeLabel = new System.Windows.Forms.Label();
            this.eventTimeLabel = new System.Windows.Forms.Label();
            this.eventMinProfitLabel = new System.Windows.Forms.Label();
            this.oddsCheckerLinkLabel = new System.Windows.Forms.LinkLabel();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.recalcOriginalBet = new System.Windows.Forms.NumericUpDown();
            this.recalcMaximumBet = new System.Windows.Forms.NumericUpDown();
            this.recalcButton = new System.Windows.Forms.Button();
            this.recalcLabel = new System.Windows.Forms.Label();
            this.eventSportLabel = new System.Windows.Forms.Label();
            this.eventRefreshButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.maxUniqueBetsControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stakeControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.resultsTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxThreadsControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxHoursControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minProfitControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.recalcOriginalBet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.recalcMaximumBet)).BeginInit();
            this.SuspendLayout();
            // 
            // maxUniqueBetsControl
            // 
            this.maxUniqueBetsControl.Location = new System.Drawing.Point(62, 7);
            this.maxUniqueBetsControl.Name = "maxUniqueBetsControl";
            this.maxUniqueBetsControl.Size = new System.Drawing.Size(38, 20);
            this.maxUniqueBetsControl.TabIndex = 0;
            this.maxUniqueBetsControl.ValueChanged += new System.EventHandler(this.FilterValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Max Bets";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(106, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Stake";
            // 
            // stakeControl
            // 
            this.stakeControl.DecimalPlaces = 2;
            this.stakeControl.Location = new System.Drawing.Point(147, 7);
            this.stakeControl.Maximum = new decimal(new int[] {
            50000,
            0,
            0,
            0});
            this.stakeControl.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.stakeControl.Name = "stakeControl";
            this.stakeControl.Size = new System.Drawing.Size(63, 20);
            this.stakeControl.TabIndex = 2;
            this.stakeControl.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.stakeControl.ValueChanged += new System.EventHandler(this.FilterValueChanged);
            // 
            // bookMakersListBox
            // 
            this.bookMakersListBox.CheckOnClick = true;
            this.bookMakersListBox.FormattingEnabled = true;
            this.bookMakersListBox.Location = new System.Drawing.Point(8, 33);
            this.bookMakersListBox.Margin = new System.Windows.Forms.Padding(2);
            this.bookMakersListBox.Name = "bookMakersListBox";
            this.bookMakersListBox.Size = new System.Drawing.Size(130, 619);
            this.bookMakersListBox.TabIndex = 55;
            this.bookMakersListBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.bookMakersListBox_ItemCheck);
            // 
            // resultsTable
            // 
            this.resultsTable.AllowUserToAddRows = false;
            this.resultsTable.AllowUserToDeleteRows = false;
            this.resultsTable.AllowUserToResizeColumns = false;
            this.resultsTable.AllowUserToResizeRows = false;
            this.resultsTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.resultsTable.Location = new System.Drawing.Point(140, 33);
            this.resultsTable.MultiSelect = false;
            this.resultsTable.Name = "resultsTable";
            this.resultsTable.ReadOnly = true;
            this.resultsTable.RowHeadersVisible = false;
            this.resultsTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.resultsTable.Size = new System.Drawing.Size(731, 619);
            this.resultsTable.TabIndex = 56;
            this.resultsTable.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.URLTable_CellClick);
            this.resultsTable.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.resultsTable_ColumnHeaderMouseClick);
            // 
            // findOddsButton
            // 
            this.findOddsButton.Location = new System.Drawing.Point(579, 4);
            this.findOddsButton.Name = "findOddsButton";
            this.findOddsButton.Size = new System.Drawing.Size(83, 23);
            this.findOddsButton.TabIndex = 57;
            this.findOddsButton.Text = "Find Odds";
            this.findOddsButton.UseVisualStyleBackColor = true;
            this.findOddsButton.Click += new System.EventHandler(this.findOddsButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(757, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 13);
            this.label3.TabIndex = 60;
            this.label3.Text = "Max Threads";
            // 
            // maxThreadsControl
            // 
            this.maxThreadsControl.Location = new System.Drawing.Point(832, 7);
            this.maxThreadsControl.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.maxThreadsControl.Name = "maxThreadsControl";
            this.maxThreadsControl.Size = new System.Drawing.Size(39, 20);
            this.maxThreadsControl.TabIndex = 59;
            this.maxThreadsControl.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.WorkerSupportsCancellation = true;
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
            // 
            // statusLabel
            // 
            this.statusLabel.Location = new System.Drawing.Point(137, 655);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(575, 13);
            this.statusLabel.TabIndex = 61;
            this.statusLabel.Text = "Status";
            this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // stopButton
            // 
            this.stopButton.Enabled = false;
            this.stopButton.Location = new System.Drawing.Point(668, 4);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(83, 23);
            this.stopButton.TabIndex = 62;
            this.stopButton.Text = "Stop";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // linkCheckStatusLabel
            // 
            this.linkCheckStatusLabel.Location = new System.Drawing.Point(767, 655);
            this.linkCheckStatusLabel.Name = "linkCheckStatusLabel";
            this.linkCheckStatusLabel.Size = new System.Drawing.Size(104, 13);
            this.linkCheckStatusLabel.TabIndex = 63;
            this.linkCheckStatusLabel.Text = "Checking 0 / 0";
            this.linkCheckStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(216, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 13);
            this.label4.TabIndex = 65;
            this.label4.Text = "Max Time (hrs)";
            // 
            // maxHoursControl
            // 
            this.maxHoursControl.Location = new System.Drawing.Point(319, 7);
            this.maxHoursControl.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.maxHoursControl.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.maxHoursControl.Name = "maxHoursControl";
            this.maxHoursControl.Size = new System.Drawing.Size(47, 20);
            this.maxHoursControl.TabIndex = 64;
            this.maxHoursControl.Value = new decimal(new int[] {
            48,
            0,
            0,
            0});
            this.maxHoursControl.ValueChanged += new System.EventHandler(this.FilterValueChanged);
            // 
            // inPlayControl
            // 
            this.inPlayControl.AutoSize = true;
            this.inPlayControl.Location = new System.Drawing.Point(372, 8);
            this.inPlayControl.Name = "inPlayControl";
            this.inPlayControl.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.inPlayControl.Size = new System.Drawing.Size(58, 17);
            this.inPlayControl.TabIndex = 66;
            this.inPlayControl.Text = "In Play";
            this.inPlayControl.UseVisualStyleBackColor = true;
            this.inPlayControl.CheckedChanged += new System.EventHandler(this.FilterValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(436, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 13);
            this.label5.TabIndex = 68;
            this.label5.Text = "Min Profit (%)";
            // 
            // minProfitControl
            // 
            this.minProfitControl.DecimalPlaces = 2;
            this.minProfitControl.Location = new System.Drawing.Point(510, 7);
            this.minProfitControl.Maximum = new decimal(new int[] {
            50000,
            0,
            0,
            0});
            this.minProfitControl.Name = "minProfitControl";
            this.minProfitControl.Size = new System.Drawing.Size(63, 20);
            this.minProfitControl.TabIndex = 67;
            this.minProfitControl.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.minProfitControl.ValueChanged += new System.EventHandler(this.FilterValueChanged);
            // 
            // maxHoursEnabledCheckBox
            // 
            this.maxHoursEnabledCheckBox.AutoSize = true;
            this.maxHoursEnabledCheckBox.Location = new System.Drawing.Point(298, 9);
            this.maxHoursEnabledCheckBox.Name = "maxHoursEnabledCheckBox";
            this.maxHoursEnabledCheckBox.Size = new System.Drawing.Size(15, 14);
            this.maxHoursEnabledCheckBox.TabIndex = 69;
            this.maxHoursEnabledCheckBox.UseVisualStyleBackColor = true;
            this.maxHoursEnabledCheckBox.CheckedChanged += new System.EventHandler(this.maxHoursEnabledCheckBox_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(877, 70);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 13);
            this.label6.TabIndex = 70;
            this.label6.Text = "Event Details";
            // 
            // eventNameTypeLabel
            // 
            this.eventNameTypeLabel.AutoSize = true;
            this.eventNameTypeLabel.Location = new System.Drawing.Point(877, 107);
            this.eventNameTypeLabel.Name = "eventNameTypeLabel";
            this.eventNameTypeLabel.Size = new System.Drawing.Size(114, 13);
            this.eventNameTypeLabel.TabIndex = 71;
            this.eventNameTypeLabel.Text = "Event Name and Type";
            // 
            // eventTimeLabel
            // 
            this.eventTimeLabel.AutoSize = true;
            this.eventTimeLabel.Location = new System.Drawing.Point(877, 122);
            this.eventTimeLabel.Name = "eventTimeLabel";
            this.eventTimeLabel.Size = new System.Drawing.Size(108, 13);
            this.eventTimeLabel.TabIndex = 72;
            this.eventTimeLabel.Text = "Event Date and Time";
            // 
            // eventMinProfitLabel
            // 
            this.eventMinProfitLabel.AutoSize = true;
            this.eventMinProfitLabel.Location = new System.Drawing.Point(877, 137);
            this.eventMinProfitLabel.Name = "eventMinProfitLabel";
            this.eventMinProfitLabel.Size = new System.Drawing.Size(87, 13);
            this.eventMinProfitLabel.TabIndex = 73;
            this.eventMinProfitLabel.Text = "Minimum Profit: 0";
            // 
            // oddsCheckerLinkLabel
            // 
            this.oddsCheckerLinkLabel.AutoSize = true;
            this.oddsCheckerLinkLabel.Enabled = false;
            this.oddsCheckerLinkLabel.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.oddsCheckerLinkLabel.Location = new System.Drawing.Point(877, 179);
            this.oddsCheckerLinkLabel.Name = "oddsCheckerLinkLabel";
            this.oddsCheckerLinkLabel.Size = new System.Drawing.Size(100, 13);
            this.oddsCheckerLinkLabel.TabIndex = 74;
            this.oddsCheckerLinkLabel.TabStop = true;
            this.oddsCheckerLinkLabel.Text = "OddsChecker Page";
            this.oddsCheckerLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkClicked);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(877, 157);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(37, 13);
            this.label7.TabIndex = 75;
            this.label7.Text = "Links";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(877, 626);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 13);
            this.label8.TabIndex = 78;
            this.label8.Text = "Maximum Bet";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(877, 603);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(61, 13);
            this.label9.TabIndex = 77;
            this.label9.Text = "Original Bet";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(877, 577);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(154, 13);
            this.label10.TabIndex = 76;
            this.label10.Text = "Minimum Bet Recalculator";
            // 
            // recalcOriginalBet
            // 
            this.recalcOriginalBet.DecimalPlaces = 2;
            this.recalcOriginalBet.Location = new System.Drawing.Point(968, 601);
            this.recalcOriginalBet.Maximum = new decimal(new int[] {
            50000,
            0,
            0,
            0});
            this.recalcOriginalBet.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.recalcOriginalBet.Name = "recalcOriginalBet";
            this.recalcOriginalBet.Size = new System.Drawing.Size(63, 20);
            this.recalcOriginalBet.TabIndex = 79;
            this.recalcOriginalBet.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // recalcMaximumBet
            // 
            this.recalcMaximumBet.DecimalPlaces = 2;
            this.recalcMaximumBet.Location = new System.Drawing.Point(968, 624);
            this.recalcMaximumBet.Maximum = new decimal(new int[] {
            50000,
            0,
            0,
            0});
            this.recalcMaximumBet.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.recalcMaximumBet.Name = "recalcMaximumBet";
            this.recalcMaximumBet.Size = new System.Drawing.Size(63, 20);
            this.recalcMaximumBet.TabIndex = 80;
            this.recalcMaximumBet.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // recalcButton
            // 
            this.recalcButton.Location = new System.Drawing.Point(1037, 598);
            this.recalcButton.Name = "recalcButton";
            this.recalcButton.Size = new System.Drawing.Size(83, 23);
            this.recalcButton.TabIndex = 81;
            this.recalcButton.Text = "Calculate";
            this.recalcButton.UseVisualStyleBackColor = true;
            this.recalcButton.Click += new System.EventHandler(this.recalcButton_Click);
            // 
            // recalcLabel
            // 
            this.recalcLabel.AutoSize = true;
            this.recalcLabel.Location = new System.Drawing.Point(1037, 626);
            this.recalcLabel.Name = "recalcLabel";
            this.recalcLabel.Size = new System.Drawing.Size(70, 13);
            this.recalcLabel.TabIndex = 82;
            this.recalcLabel.Text = "New stake = ";
            // 
            // eventSportLabel
            // 
            this.eventSportLabel.AutoSize = true;
            this.eventSportLabel.Location = new System.Drawing.Point(877, 92);
            this.eventSportLabel.Name = "eventSportLabel";
            this.eventSportLabel.Size = new System.Drawing.Size(63, 13);
            this.eventSportLabel.TabIndex = 83;
            this.eventSportLabel.Text = "Event Sport";
            // 
            // eventRefreshButton
            // 
            this.eventRefreshButton.Enabled = false;
            this.eventRefreshButton.Location = new System.Drawing.Point(877, 33);
            this.eventRefreshButton.Name = "eventRefreshButton";
            this.eventRefreshButton.Size = new System.Drawing.Size(83, 23);
            this.eventRefreshButton.TabIndex = 84;
            this.eventRefreshButton.Text = "Refresh";
            this.eventRefreshButton.UseVisualStyleBackColor = true;
            this.eventRefreshButton.Click += new System.EventHandler(this.eventRefreshButton_Click);
            // 
            // BetFinder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 672);
            this.Controls.Add(this.eventRefreshButton);
            this.Controls.Add(this.eventSportLabel);
            this.Controls.Add(this.recalcLabel);
            this.Controls.Add(this.recalcButton);
            this.Controls.Add(this.recalcMaximumBet);
            this.Controls.Add(this.recalcOriginalBet);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.oddsCheckerLinkLabel);
            this.Controls.Add(this.eventMinProfitLabel);
            this.Controls.Add(this.eventTimeLabel);
            this.Controls.Add(this.eventNameTypeLabel);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.maxHoursEnabledCheckBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.minProfitControl);
            this.Controls.Add(this.inPlayControl);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.maxHoursControl);
            this.Controls.Add(this.linkCheckStatusLabel);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.maxThreadsControl);
            this.Controls.Add(this.findOddsButton);
            this.Controls.Add(this.resultsTable);
            this.Controls.Add(this.bookMakersListBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.stakeControl);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.maxUniqueBetsControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "BetFinder";
            this.Text = "Bet Finder";
            ((System.ComponentModel.ISupportInitialize)(this.maxUniqueBetsControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stakeControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.resultsTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxThreadsControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxHoursControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minProfitControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.recalcOriginalBet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.recalcMaximumBet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown maxUniqueBetsControl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown stakeControl;
        private System.Windows.Forms.CheckedListBox bookMakersListBox;
        private System.Windows.Forms.DataGridView resultsTable;
        private System.Windows.Forms.Button findOddsButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown maxThreadsControl;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.Label linkCheckStatusLabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown maxHoursControl;
        private System.Windows.Forms.CheckBox inPlayControl;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown minProfitControl;
        private System.Windows.Forms.CheckBox maxHoursEnabledCheckBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label eventNameTypeLabel;
        private System.Windows.Forms.Label eventTimeLabel;
        private System.Windows.Forms.Label eventMinProfitLabel;
        private System.Windows.Forms.LinkLabel oddsCheckerLinkLabel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown recalcOriginalBet;
        private System.Windows.Forms.NumericUpDown recalcMaximumBet;
        private System.Windows.Forms.Button recalcButton;
        private System.Windows.Forms.Label recalcLabel;
        private System.Windows.Forms.Label eventSportLabel;
        private System.Windows.Forms.Button eventRefreshButton;
    }
}

