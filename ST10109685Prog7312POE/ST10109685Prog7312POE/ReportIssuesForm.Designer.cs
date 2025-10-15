using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ST10109685Prog7312POE
{
    partial class ReportIssuesForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // Form controls
        private TextBox txtLocation;
        private ComboBox cmbCategory;
        private RichTextBox rtxtDescription;
        private Button btnAttachFile;
        private Button btnSubmit;
        private Button btnBackToMenu;
        private Label lblEngagement;
        private ProgressBar progressEngagement;
        private ListBox lstAttachedFiles;
        private Label lblAttachedFiles;
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
            this.Text = "Report Issues - Municipal Services";
            this.Size = new Size(700, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(240, 248, 255);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Header Panel
            headerPanel = new Panel();
            headerPanel.Location = new Point(0, 0);
            headerPanel.Size = new Size(700, 80);
            headerPanel.BackColor = Color.FromArgb(70, 130, 180);
            this.Controls.Add(headerPanel);

            Label lblHeader = new Label();
            lblHeader.Text = "Report Municipal Issues";
            lblHeader.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblHeader.ForeColor = Color.White;
            lblHeader.Location = new Point(250, 25);
            lblHeader.Size = new Size(200, 30);
            lblHeader.TextAlign = ContentAlignment.MiddleCenter;
            headerPanel.Controls.Add(lblHeader);

            // Location Input
            Label lblLocation = new Label();
            lblLocation.Text = "Location:";
            lblLocation.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblLocation.Location = new Point(30, 100);
            lblLocation.Size = new Size(100, 23);
            this.Controls.Add(lblLocation);

            txtLocation = new TextBox();
            txtLocation.Font = new Font("Segoe UI", 10F);
            txtLocation.Location = new Point(30, 125);
            txtLocation.Size = new Size(300, 25);
            this.Controls.Add(txtLocation);

            // Category Selection
            Label lblCategory = new Label();
            lblCategory.Text = "Issue Category:";
            lblCategory.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblCategory.Location = new Point(350, 100);
            lblCategory.Size = new Size(120, 23);
            this.Controls.Add(lblCategory);

            cmbCategory = new ComboBox();
            cmbCategory.Font = new Font("Segoe UI", 10F);
            cmbCategory.Location = new Point(350, 125);
            cmbCategory.Size = new Size(300, 25);
            cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategory.Items.AddRange(new string[] {
                "Roads and Infrastructure",
                "Water and Sanitation",
                "Electricity and Power",
                "Waste Management",
                "Parks and Recreation",
                "Public Safety",
                "Housing and Property",
                "Other"
            });
            this.Controls.Add(cmbCategory);

            // Description Box
            Label lblDescription = new Label();
            lblDescription.Text = "Issue Description:";
            lblDescription.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblDescription.Location = new Point(30, 170);
            lblDescription.Size = new Size(150, 23);
            this.Controls.Add(lblDescription);

            rtxtDescription = new RichTextBox();
            rtxtDescription.Font = new Font("Segoe UI", 10F);
            rtxtDescription.Location = new Point(30, 195);
            rtxtDescription.Size = new Size(620, 120);
            this.Controls.Add(rtxtDescription);

            // File Attachment
            btnAttachFile = new Button();
            btnAttachFile.Text = "📎 Attach Files";
            btnAttachFile.Font = new Font("Segoe UI", 10F);
            btnAttachFile.Location = new Point(30, 335);
            btnAttachFile.Size = new Size(150, 35);
            btnAttachFile.BackColor = Color.FromArgb(100, 149, 237);
            btnAttachFile.ForeColor = Color.White;
            btnAttachFile.FlatStyle = FlatStyle.Flat;
            btnAttachFile.FlatAppearance.BorderSize = 0;
            btnAttachFile.Cursor = Cursors.Hand;
            btnAttachFile.Click += BtnAttachFile_Click;
            this.Controls.Add(btnAttachFile);

            // Attached Files List
            lblAttachedFiles = new Label();
            lblAttachedFiles.Text = "Attached Files:";
            lblAttachedFiles.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblAttachedFiles.Location = new Point(200, 335);
            lblAttachedFiles.Size = new Size(100, 20);
            this.Controls.Add(lblAttachedFiles);

            lstAttachedFiles = new ListBox();
            lstAttachedFiles.Font = new Font("Segoe UI", 9F);
            lstAttachedFiles.Location = new Point(200, 360);
            lstAttachedFiles.Size = new Size(450, 60);
            lstAttachedFiles.BackColor = Color.FromArgb(250, 250, 250);
            this.Controls.Add(lstAttachedFiles);

            // Engagement Feature - Progress and Motivation
            lblEngagement = new Label();
            lblEngagement.Text = "Your voice matters! Help us serve you better.";
            lblEngagement.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblEngagement.ForeColor = Color.FromArgb(34, 139, 34);
            lblEngagement.Location = new Point(30, 440);
            lblEngagement.Size = new Size(400, 25);
            this.Controls.Add(lblEngagement);

            progressEngagement = new ProgressBar();
            progressEngagement.Location = new Point(30, 470);
            progressEngagement.Size = new Size(400, 20);
            progressEngagement.Style = ProgressBarStyle.Continuous;
            progressEngagement.ForeColor = Color.FromArgb(34, 139, 34);
            this.Controls.Add(progressEngagement);

            // Submit Button
            btnSubmit = new Button();
            btnSubmit.Text = "✓ Submit Report";
            btnSubmit.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnSubmit.Location = new Point(450, 450);
            btnSubmit.Size = new Size(200, 40);
            btnSubmit.BackColor = Color.FromArgb(34, 139, 34);
            btnSubmit.ForeColor = Color.White;
            btnSubmit.FlatStyle = FlatStyle.Flat;
            btnSubmit.FlatAppearance.BorderSize = 0;
            btnSubmit.Cursor = Cursors.Hand;
            btnSubmit.Click += BtnSubmit_Click;
            this.Controls.Add(btnSubmit);

            // Back to Main Menu Button
            btnBackToMenu = new Button();
            btnBackToMenu.Text = "← Back to Main Menu";
            btnBackToMenu.Font = new Font("Segoe UI", 10F);
            btnBackToMenu.Location = new Point(30, 560);
            btnBackToMenu.Size = new Size(180, 35);
            btnBackToMenu.BackColor = Color.FromArgb(169, 169, 169);
            btnBackToMenu.ForeColor = Color.White;
            btnBackToMenu.FlatStyle = FlatStyle.Flat;
            btnBackToMenu.FlatAppearance.BorderSize = 0;
            btnBackToMenu.Cursor = Cursors.Hand;
            btnBackToMenu.Click += BtnBackToMenu_Click;
            this.Controls.Add(btnBackToMenu);

            // Event handlers for engagement
            txtLocation.TextChanged += UpdateEngagement;
            cmbCategory.SelectedIndexChanged += UpdateEngagement;
            rtxtDescription.TextChanged += UpdateEngagement;

            this.ResumeLayout(false);
        }

        #endregion
    }
}