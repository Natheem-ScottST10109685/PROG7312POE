using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ST10109685Prog7312POE
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // Form controls
        private Button btnReportIssues;
        private Button btnLocalEvents;
        private Button btnServiceStatus;
        private Label lblWelcome;
        private Label lblSubtitle;
        private Panel headerPanel;

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
            this.SuspendLayout();

            // Form properties
            this.Text = "Municipal Services Application";
            this.Size = new Size(600, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(240, 248, 255);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Header Panel
            headerPanel = new Panel();
            headerPanel.Location = new Point(0, 0);
            headerPanel.Size = new Size(600, 120);
            headerPanel.BackColor = Color.FromArgb(70, 130, 180);
            this.Controls.Add(headerPanel);

            // Welcome Label
            lblWelcome = new Label();
            lblWelcome.Text = "Municipal Services Portal";
            lblWelcome.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblWelcome.ForeColor = Color.White;
            lblWelcome.Location = new Point(150, 30);
            lblWelcome.Size = new Size(300, 35);
            lblWelcome.TextAlign = ContentAlignment.MiddleCenter;
            headerPanel.Controls.Add(lblWelcome);

            // Subtitle Label
            lblSubtitle = new Label();
            lblSubtitle.Text = "Serving Our Community Better";
            lblSubtitle.Font = new Font("Segoe UI", 12F);
            lblSubtitle.ForeColor = Color.White;
            lblSubtitle.Location = new Point(150, 70);
            lblSubtitle.Size = new Size(300, 25);
            lblSubtitle.TextAlign = ContentAlignment.MiddleCenter;
            headerPanel.Controls.Add(lblSubtitle);

            // Report Issues Button (Active)
            btnReportIssues = new Button();
            btnReportIssues.Text = "Report Issues";
            btnReportIssues.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnReportIssues.Size = new Size(350, 60);
            btnReportIssues.Location = new Point(125, 160);
            btnReportIssues.BackColor = Color.FromArgb(34, 139, 34);
            btnReportIssues.ForeColor = Color.White;
            btnReportIssues.FlatStyle = FlatStyle.Flat;
            btnReportIssues.FlatAppearance.BorderSize = 0;
            btnReportIssues.Cursor = Cursors.Hand;
            btnReportIssues.Click += BtnReportIssues_Click;
            this.Controls.Add(btnReportIssues);

            // Local Events Button (NOW ACTIVE)
            btnLocalEvents = new Button();
            btnLocalEvents.Text = "Local Events and Announcements";
            btnLocalEvents.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnLocalEvents.Size = new Size(350, 60);
            btnLocalEvents.Location = new Point(125, 240);
            btnLocalEvents.BackColor = Color.FromArgb(34, 139, 34);
            btnLocalEvents.ForeColor = Color.White;
            btnLocalEvents.FlatStyle = FlatStyle.Flat;
            btnLocalEvents.FlatAppearance.BorderSize = 0;
            btnLocalEvents.Cursor = Cursors.Hand;
            btnLocalEvents.Enabled = true;
            btnLocalEvents.Click += BtnLocalEvents_Click;
            this.Controls.Add(btnLocalEvents);

            // Service Status Button (Disabled)
            btnServiceStatus = new Button();
            btnServiceStatus.Text = "Service Request Status";
            btnServiceStatus.Font = new Font("Segoe UI", 12F);
            btnServiceStatus.Size = new Size(350, 60);
            btnServiceStatus.Location = new Point(125, 320);
            btnServiceStatus.BackColor = Color.Gray;
            btnServiceStatus.ForeColor = Color.LightGray;
            btnServiceStatus.FlatStyle = FlatStyle.Flat;
            btnServiceStatus.FlatAppearance.BorderSize = 0;
            btnServiceStatus.Enabled = false;
            this.Controls.Add(btnServiceStatus);

            // Footer Label
            Label lblFooter = new Label();
            lblFooter.Text = "Coming Soon: Service Status Tracking";
            lblFooter.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            lblFooter.ForeColor = Color.Gray;
            lblFooter.Location = new Point(125, 400);
            lblFooter.Size = new Size(350, 20);
            lblFooter.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(lblFooter);

            this.ResumeLayout(false);
        }

        #endregion
    }
}