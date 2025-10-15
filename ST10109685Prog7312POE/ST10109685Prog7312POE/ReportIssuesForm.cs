using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

namespace ST10109685Prog7312POE
{
    public partial class ReportIssuesForm : Form
    {
        private List<string> attachedFiles;

        public ReportIssuesForm()
        {
            attachedFiles = new List<string>();
            InitializeComponent();

            // Add additional event handlers for better interactivity
            this.Load += ReportIssuesForm_Load;
        }

        /// <summary>
        /// Form load event handler
        /// </summary>
        private void ReportIssuesForm_Load(object sender, EventArgs e)
        {
            // Initialize form state
            UpdateEngagement(sender, e);
        }

        /// <summary>
        /// Updates the engagement progress bar and motivational message
        /// </summary>
        private void UpdateEngagement(object sender, EventArgs e)
        {
            int progress = 0;
            string[] motivationalMessages = {
                "Your voice matters! Help us serve you better.",
                "Great start! Keep filling out the details.",
                "Excellent! You're helping improve our community.",
                "Almost there! Your report will make a difference.",
                "Perfect! Ready to submit your important feedback."
            };

            // Calculate progress based on form completion
            if (!string.IsNullOrWhiteSpace(txtLocation.Text)) progress += 25;
            if (cmbCategory.SelectedIndex != -1) progress += 25;
            if (!string.IsNullOrWhiteSpace(rtxtDescription.Text)) progress += 25;
            if (attachedFiles.Count > 0) progress += 25;

            progressEngagement.Value = progress;

            int messageIndex = Math.Min(progress / 25, motivationalMessages.Length - 1);
            lblEngagement.Text = motivationalMessages[messageIndex];

            // Change color based on progress
            if (progress >= 75)
                lblEngagement.ForeColor = Color.FromArgb(34, 139, 34); // Green
            else if (progress >= 50)
                lblEngagement.ForeColor = Color.FromArgb(255, 140, 0); // Orange
            else
                lblEngagement.ForeColor = Color.FromArgb(70, 130, 180); // Blue
        }

        /// <summary>
        /// Event handler for the Attach Files button
        /// </summary>
        private void BtnAttachFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png, *.bmp)|*.jpg;*.jpeg;*.png;*.bmp|Document files (*.pdf, *.doc, *.docx, *.txt)|*.pdf;*.doc;*.docx;*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.Multiselect = true;
                openFileDialog.Title = "Select files to attach";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    foreach (string fileName in openFileDialog.FileNames)
                    {
                        if (!attachedFiles.Contains(fileName))
                        {
                            // Validate file size (max 10MB per file)
                            FileInfo fileInfo = new FileInfo(fileName);
                            if (fileInfo.Length > 10 * 1024 * 1024)
                            {
                                MessageBox.Show($"File '{Path.GetFileName(fileName)}' is too large. Maximum file size is 10MB.",
                                    "File Too Large", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                continue;
                            }

                            attachedFiles.Add(fileName);
                            lstAttachedFiles.Items.Add(Path.GetFileName(fileName));
                        }
                    }
                    UpdateEngagement(sender, e);
                }
            }
        }

        /// <summary>
        /// Event handler for the Submit button - validates and stores the issue
        /// </summary>
        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            // Validate required fields
            if (string.IsNullOrWhiteSpace(txtLocation.Text))
            {
                MessageBox.Show("Please enter the location of the issue.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLocation.Focus();
                return;
            }

            if (cmbCategory.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a category for the issue.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbCategory.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(rtxtDescription.Text))
            {
                MessageBox.Show("Please provide a description of the issue.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                rtxtDescription.Focus();
                return;
            }

            // Validate description length
            if (rtxtDescription.Text.Length < 10)
            {
                MessageBox.Show("Please provide a more detailed description (at least 10 characters).",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                rtxtDescription.Focus();
                return;
            }

            try
            {
                // Store the issue using the IssueManager (with multi-dimensional array)
                int issueId = IssueManager.AddIssue(
                    txtLocation.Text.Trim(),
                    cmbCategory.SelectedItem.ToString(),
                    rtxtDescription.Text.Trim(),
                    new List<string>(attachedFiles)
                );

                // Get the stored issue to display confirmation
                ReportedIssue storedIssue = IssueManager.GetIssueById(issueId);

                // Success feedback
                string filesInfo = attachedFiles.Count > 0
                    ? $"\nAttached Files: {attachedFiles.Count}"
                    : "\nNo files attached";

                MessageBox.Show($"Thank you! Your issue has been reported successfully.\n\n" +
                              $"Issue ID: {storedIssue.IssueId}\n" +
                              $"Location: {storedIssue.Location}\n" +
                              $"Category: {storedIssue.Category}\n" +
                              $"Submitted: {storedIssue.ReportedDate:yyyy-MM-dd HH:mm}" +
                              filesInfo + "\n\n" +
                              "Our team will review your report and take appropriate action.",
                              "Report Submitted Successfully",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Show statistics
                ShowStatistics();

                // Clear form for next report
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while submitting your report:\n{ex.Message}",
                    "Submission Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Displays statistics about reported issues
        /// </summary>
        private void ShowStatistics()
        {
            int totalIssues = IssueManager.GetIssueCount();
            if (totalIssues > 1)
            {
                DialogResult result = MessageBox.Show(
                    $"Total issues reported: {totalIssues}\n\n" +
                    "Would you like to view category statistics?",
                    "Issue Statistics",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information);

                if (result == DialogResult.Yes)
                {
                    var stats = IssueManager.GetCategoryStatistics();
                    string statsMessage = "Issues by Category:\n\n";
                    foreach (var stat in stats)
                    {
                        statsMessage += $"• {stat.Key}: {stat.Value}\n";
                    }

                    MessageBox.Show(statsMessage, "Category Statistics",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        /// <summary>
        /// Clears all form fields and resets state
        /// </summary>
        private void ClearForm()
        {
            txtLocation.Clear();
            cmbCategory.SelectedIndex = -1;
            rtxtDescription.Clear();
            attachedFiles.Clear();
            lstAttachedFiles.Items.Clear();
            progressEngagement.Value = 0;
            lblEngagement.Text = "Your voice matters! Help us serve you better.";
            lblEngagement.ForeColor = Color.FromArgb(70, 130, 180);
            txtLocation.Focus();
        }

        /// <summary>
        /// Event handler for the Back to Menu button
        /// </summary>
        private void BtnBackToMenu_Click(object sender, EventArgs e)
        {
            // Confirm if user has unsaved data
            if (!string.IsNullOrWhiteSpace(txtLocation.Text) ||
                cmbCategory.SelectedIndex != -1 ||
                !string.IsNullOrWhiteSpace(rtxtDescription.Text) ||
                attachedFiles.Count > 0)
            {
                DialogResult result = MessageBox.Show(
                    "You have unsaved data. Are you sure you want to go back?",
                    "Confirm Navigation",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.No)
                {
                    return;
                }
            }

            this.Close();
        }

        /// <summary>
        /// Property to access reported issues (for potential future use)
        /// </summary>
        public static List<ReportedIssue> GetReportedIssues()
        {
            return IssueManager.GetAllIssues();
        }
    }
}