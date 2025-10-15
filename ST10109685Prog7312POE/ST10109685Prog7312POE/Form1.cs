using System;
using System.Windows.Forms;

namespace ST10109685Prog7312POE
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            // Add form event handlers
            this.Load += Form1_Load;
            this.FormClosing += Form1_FormClosing;

            // Add hover effects for buttons
            btnReportIssues.MouseEnter += BtnReportIssues_MouseEnter;
            btnReportIssues.MouseLeave += BtnReportIssues_MouseLeave;

            btnLocalEvents.MouseEnter += BtnLocalEvents_MouseEnter;
            btnLocalEvents.MouseLeave += BtnLocalEvents_MouseLeave;
        }

        /// <summary>
        /// Form load event handler
        /// </summary>
        private void Form1_Load(object sender, EventArgs e)
        {
            // Welcome message on startup
            lblSubtitle.Text = $"Serving Our Community Better - {DateTime.Now:MMMM yyyy}";
        }

        /// <summary>
        /// Form closing event handler
        /// </summary>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            int issueCount = IssueManager.GetIssueCount();
            if (issueCount > 0)
            {
                DialogResult result = MessageBox.Show(
                    $"You have {issueCount} issue(s) reported in this session.\n\n" +
                    "Note: Issues will be lost when the application closes.\n\n" +
                    "Do you want to exit?",
                    "Confirm Exit",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

        /// <summary>
        /// Event handler for Report Issues button click
        /// </summary>
        private void BtnReportIssues_Click(object sender, EventArgs e)
        {
            ReportIssuesForm reportForm = new ReportIssuesForm();
            this.Hide();
            reportForm.ShowDialog();
            this.Show();

            // Update subtitle with issue count after returning from report form
            int issueCount = IssueManager.GetIssueCount();
            if (issueCount > 0)
            {
                lblSubtitle.Text = $"Serving Our Community Better - {issueCount} issue(s) reported";
            }
        }

        /// <summary>
        /// Event handler for Local Events button click
        /// </summary>
        private void BtnLocalEvents_Click(object sender, EventArgs e)
        {
            LocalEventsForm eventsForm = new LocalEventsForm();
            this.Hide();
            eventsForm.ShowDialog();
            this.Show();
        }

        /// <summary>
        /// Mouse enter event for Report Issues button - hover effect
        /// </summary>
        private void BtnReportIssues_MouseEnter(object sender, EventArgs e)
        {
            btnReportIssues.BackColor = System.Drawing.Color.FromArgb(40, 167, 69);
        }

        /// <summary>
        /// Mouse leave event for Report Issues button - reset color
        /// </summary>
        private void BtnReportIssues_MouseLeave(object sender, EventArgs e)
        {
            btnReportIssues.BackColor = System.Drawing.Color.FromArgb(34, 139, 34);
        }

        /// <summary>
        /// Mouse enter event for Local Events button - hover effect
        /// </summary>
        private void BtnLocalEvents_MouseEnter(object sender, EventArgs e)
        {
            btnLocalEvents.BackColor = System.Drawing.Color.FromArgb(40, 167, 69);
        }

        /// <summary>
        /// Mouse leave event for Local Events button - reset color
        /// </summary>
        private void BtnLocalEvents_MouseLeave(object sender, EventArgs e)
        {
            btnLocalEvents.BackColor = System.Drawing.Color.FromArgb(34, 139, 34);
        }
    }
}