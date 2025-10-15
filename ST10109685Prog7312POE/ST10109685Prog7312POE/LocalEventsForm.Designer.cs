using System;
using System.Drawing;
using System.Windows.Forms;

namespace ST10109685Prog7312POE
{
    partial class LocalEventsForm
    {
        private System.ComponentModel.IContainer components = null;

        // Header
        private Panel headerPanel;
        private Label lblTitle;
        private Label lblSubtitle;
        private Button btnBack;

        // Search Panel
        private Panel searchPanel;
        private Label lblSearchBy;
        private ComboBox cmbCategory;
        private Label lblDateRange;
        private DateTimePicker dtpStartDate;
        private Label lblTo;
        private DateTimePicker dtpEndDate;
        private Button btnSearch;
        private Button btnClearFilter;
        private CheckBox chkUpcomingOnly;

        // Events Display
        private Panel eventsPanel;
        private ListBox lstEvents;
        private Panel eventDetailsPanel;
        private RichTextBox rtbEventDetails;

        // Recommendations Panel
        private Panel recommendationsPanel;
        private Label lblRecommendations;
        private ListBox lstRecommendations;

        // Statistics Panel
        private Panel statsPanel;
        private Label lblStats;
        private Label lblTotalEvents;
        private Label lblUpcomingEvents;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // Form properties
            this.Text = "Local Events and Announcements";
            this.Size = new Size(1200, 800);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(240, 248, 255);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Header Panel
            headerPanel = new Panel();
            headerPanel.Location = new Point(0, 0);
            headerPanel.Size = new Size(1200, 100);
            headerPanel.BackColor = Color.FromArgb(70, 130, 180);
            this.Controls.Add(headerPanel);

            // Back Button
            btnBack = new Button();
            btnBack.Text = "← Back";
            btnBack.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnBack.Size = new Size(100, 35);
            btnBack.Location = new Point(20, 30);
            btnBack.BackColor = Color.FromArgb(52, 98, 135);
            btnBack.ForeColor = Color.White;
            btnBack.FlatStyle = FlatStyle.Flat;
            btnBack.FlatAppearance.BorderSize = 0;
            btnBack.Cursor = Cursors.Hand;
            btnBack.Click += BtnBack_Click;
            headerPanel.Controls.Add(btnBack);

            // Title Label
            lblTitle = new Label();
            lblTitle.Text = "Local Events & Announcements";
            lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(400, 25);
            lblTitle.Size = new Size(400, 35);
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            headerPanel.Controls.Add(lblTitle);

            // Subtitle Label
            lblSubtitle = new Label();
            lblSubtitle.Text = "Stay informed about community events";
            lblSubtitle.Font = new Font("Segoe UI", 11F);
            lblSubtitle.ForeColor = Color.White;
            lblSubtitle.Location = new Point(400, 60);
            lblSubtitle.Size = new Size(400, 25);
            lblSubtitle.TextAlign = ContentAlignment.MiddleCenter;
            headerPanel.Controls.Add(lblSubtitle);

            // Search Panel
            searchPanel = new Panel();
            searchPanel.Location = new Point(20, 120);
            searchPanel.Size = new Size(1160, 120);
            searchPanel.BackColor = Color.White;
            searchPanel.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(searchPanel);

            // Search By Label
            lblSearchBy = new Label();
            lblSearchBy.Text = "Filter by Category:";
            lblSearchBy.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblSearchBy.Location = new Point(20, 20);
            lblSearchBy.Size = new Size(150, 25);
            searchPanel.Controls.Add(lblSearchBy);

            // Category ComboBox
            cmbCategory = new ComboBox();
            cmbCategory.Font = new Font("Segoe UI", 10F);
            cmbCategory.Location = new Point(180, 18);
            cmbCategory.Size = new Size(250, 30);
            cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            searchPanel.Controls.Add(cmbCategory);

            // Upcoming Only Checkbox
            chkUpcomingOnly = new CheckBox();
            chkUpcomingOnly.Text = "Upcoming Events Only";
            chkUpcomingOnly.Font = new Font("Segoe UI", 10F);
            chkUpcomingOnly.Location = new Point(450, 18);
            chkUpcomingOnly.Size = new Size(200, 30);
            chkUpcomingOnly.Checked = true;
            searchPanel.Controls.Add(chkUpcomingOnly);

            // Date Range Label
            lblDateRange = new Label();
            lblDateRange.Text = "Date Range:";
            lblDateRange.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblDateRange.Location = new Point(20, 60);
            lblDateRange.Size = new Size(150, 25);
            searchPanel.Controls.Add(lblDateRange);

            // Start Date Picker
            dtpStartDate = new DateTimePicker();
            dtpStartDate.Font = new Font("Segoe UI", 10F);
            dtpStartDate.Location = new Point(180, 58);
            dtpStartDate.Size = new Size(200, 30);
            dtpStartDate.Format = DateTimePickerFormat.Short;
            searchPanel.Controls.Add(dtpStartDate);

            // To Label
            lblTo = new Label();
            lblTo.Text = "to";
            lblTo.Font = new Font("Segoe UI", 10F);
            lblTo.Location = new Point(390, 60);
            lblTo.Size = new Size(30, 25);
            lblTo.TextAlign = ContentAlignment.MiddleCenter;
            searchPanel.Controls.Add(lblTo);

            // End Date Picker
            dtpEndDate = new DateTimePicker();
            dtpEndDate.Font = new Font("Segoe UI", 10F);
            dtpEndDate.Location = new Point(430, 58);
            dtpEndDate.Size = new Size(200, 30);
            dtpEndDate.Format = DateTimePickerFormat.Short;
            searchPanel.Controls.Add(dtpEndDate);

            // Search Button
            btnSearch = new Button();
            btnSearch.Text = "Search";
            btnSearch.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnSearch.Size = new Size(120, 40);
            btnSearch.Location = new Point(680, 48);
            btnSearch.BackColor = Color.FromArgb(34, 139, 34);
            btnSearch.ForeColor = Color.White;
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.FlatAppearance.BorderSize = 0;
            btnSearch.Cursor = Cursors.Hand;
            btnSearch.Click += BtnSearch_Click;
            searchPanel.Controls.Add(btnSearch);

            // Clear Filter Button
            btnClearFilter = new Button();
            btnClearFilter.Text = "Clear Filters";
            btnClearFilter.Font = new Font("Segoe UI", 10F);
            btnClearFilter.Size = new Size(120, 40);
            btnClearFilter.Location = new Point(820, 48);
            btnClearFilter.BackColor = Color.FromArgb(70, 130, 180);
            btnClearFilter.ForeColor = Color.White;
            btnClearFilter.FlatStyle = FlatStyle.Flat;
            btnClearFilter.FlatAppearance.BorderSize = 0;
            btnClearFilter.Cursor = Cursors.Hand;
            btnClearFilter.Click += BtnClearFilter_Click;
            searchPanel.Controls.Add(btnClearFilter);

            // Events Panel
            eventsPanel = new Panel();
            eventsPanel.Location = new Point(20, 260);
            eventsPanel.Size = new Size(750, 480);
            eventsPanel.BackColor = Color.White;
            eventsPanel.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(eventsPanel);

            // Events ListBox
            lstEvents = new ListBox();
            lstEvents.Font = new Font("Segoe UI", 10F);
            lstEvents.Location = new Point(10, 40);
            lstEvents.Size = new Size(730, 430);
            lstEvents.BorderStyle = BorderStyle.None;
            lstEvents.SelectedIndexChanged += LstEvents_SelectedIndexChanged;
            eventsPanel.Controls.Add(lstEvents);

            Label lblEventsTitle = new Label();
            lblEventsTitle.Text = "Events List";
            lblEventsTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblEventsTitle.Location = new Point(10, 10);
            lblEventsTitle.Size = new Size(200, 25);
            eventsPanel.Controls.Add(lblEventsTitle);

            // Event Details Panel
            eventDetailsPanel = new Panel();
            eventDetailsPanel.Location = new Point(790, 260);
            eventDetailsPanel.Size = new Size(390, 280);
            eventDetailsPanel.BackColor = Color.White;
            eventDetailsPanel.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(eventDetailsPanel);

            Label lblDetailsTitle = new Label();
            lblDetailsTitle.Text = "Event Details";
            lblDetailsTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblDetailsTitle.Location = new Point(10, 10);
            lblDetailsTitle.Size = new Size(200, 25);
            eventDetailsPanel.Controls.Add(lblDetailsTitle);

            // Event Details RichTextBox
            rtbEventDetails = new RichTextBox();
            rtbEventDetails.Font = new Font("Segoe UI", 10F);
            rtbEventDetails.Location = new Point(10, 40);
            rtbEventDetails.Size = new Size(370, 230);
            rtbEventDetails.BorderStyle = BorderStyle.None;
            rtbEventDetails.ReadOnly = true;
            rtbEventDetails.BackColor = Color.FromArgb(250, 250, 250);
            eventDetailsPanel.Controls.Add(rtbEventDetails);

            // Recommendations Panel
            recommendationsPanel = new Panel();
            recommendationsPanel.Location = new Point(790, 560);
            recommendationsPanel.Size = new Size(390, 180);
            recommendationsPanel.BackColor = Color.White;
            recommendationsPanel.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(recommendationsPanel);

            lblRecommendations = new Label();
            lblRecommendations.Text = "Recommended Events";
            lblRecommendations.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblRecommendations.Location = new Point(10, 10);
            lblRecommendations.Size = new Size(300, 25);
            recommendationsPanel.Controls.Add(lblRecommendations);

            lstRecommendations = new ListBox();
            lstRecommendations.Font = new Font("Segoe UI", 9F);
            lstRecommendations.Location = new Point(10, 40);
            lstRecommendations.Size = new Size(370, 130);
            lstRecommendations.BorderStyle = BorderStyle.None;
            lstRecommendations.SelectedIndexChanged += LstRecommendations_SelectedIndexChanged;
            recommendationsPanel.Controls.Add(lstRecommendations);

            // Statistics Panel
            statsPanel = new Panel();
            statsPanel.Location = new Point(970, 120);
            statsPanel.Size = new Size(210, 120);
            statsPanel.BackColor = Color.FromArgb(34, 139, 34);
            statsPanel.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(statsPanel);

            lblStats = new Label();
            lblStats.Text = "Statistics";
            lblStats.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblStats.ForeColor = Color.White;
            lblStats.Location = new Point(10, 10);
            lblStats.Size = new Size(190, 25);
            lblStats.TextAlign = ContentAlignment.MiddleCenter;
            statsPanel.Controls.Add(lblStats);

            lblTotalEvents = new Label();
            lblTotalEvents.Text = "Total Events: 0";
            lblTotalEvents.Font = new Font("Segoe UI", 10F);
            lblTotalEvents.ForeColor = Color.White;
            lblTotalEvents.Location = new Point(10, 45);
            lblTotalEvents.Size = new Size(190, 25);
            lblTotalEvents.TextAlign = ContentAlignment.MiddleCenter;
            statsPanel.Controls.Add(lblTotalEvents);

            lblUpcomingEvents = new Label();
            lblUpcomingEvents.Text = "Upcoming: 0";
            lblUpcomingEvents.Font = new Font("Segoe UI", 10F);
            lblUpcomingEvents.ForeColor = Color.White;
            lblUpcomingEvents.Location = new Point(10, 75);
            lblUpcomingEvents.Size = new Size(190, 25);
            lblUpcomingEvents.TextAlign = ContentAlignment.MiddleCenter;
            statsPanel.Controls.Add(lblUpcomingEvents);

            this.ResumeLayout(false);
        }
    }
}