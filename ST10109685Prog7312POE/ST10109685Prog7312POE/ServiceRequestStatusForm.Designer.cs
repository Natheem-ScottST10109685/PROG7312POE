using System;
using System.Drawing;
using System.Windows.Forms;

namespace ST10109685Prog7312POE
{
    partial class ServiceRequestStatusForm
    {
        private System.ComponentModel.IContainer components = null;

        // ALL control declarations in one place
        private ListView lvIssues;
        private TextBox txtSearchId;
        private Button btnFind;
        private Button btnRefresh;
        private Button btnClose;
        private Button btnShowNetwork;
        private Button btnOptimalOrder;
        private Button btnComparePerformance;
        private Button btnLoadSample;
        private Label lblHeapTop;
        private Label lblStats;
        private Label lblTitle;
        private Label lblSort;
        private Label lblSearch;
        private ComboBox cmbSortOrder;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            // Form settings
            this.SuspendLayout();
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(950, 650);
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Service Request Status - Advanced Data Structures";
            this.BackColor = Color.FromArgb(250, 250, 250);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Title label
            lblTitle = new Label();
            lblTitle.Text = "Service Request Status & Tracking (Advanced DS)";
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.Location = new Point(20, 10);
            lblTitle.Size = new Size(550, 30);
            this.Controls.Add(lblTitle);

            // Statistics label
            lblStats = new Label();
            lblStats.Text = "Loading statistics...";
            lblStats.Font = new Font("Segoe UI", 9F);
            lblStats.ForeColor = Color.FromArgb(100, 100, 100);
            lblStats.Location = new Point(20, 45);
            lblStats.Size = new Size(900, 20);
            this.Controls.Add(lblStats);

            // Sort Order label
            lblSort = new Label();
            lblSort.Text = "Sort/Tree Type:";
            lblSort.Font = new Font("Segoe UI", 9F);
            lblSort.Location = new Point(20, 75);
            lblSort.Size = new Size(90, 25);
            this.Controls.Add(lblSort);

            // Sort ComboBox
            cmbSortOrder = new ComboBox();
            cmbSortOrder.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSortOrder.Font = new Font("Segoe UI", 9F);
            cmbSortOrder.Items.AddRange(new object[] {
                "BST (Binary Search Tree)",
                "AVL Tree (Balanced)",
                "Red-Black Tree (Balanced)",
                "Priority (Min Heap)",
                "Date (Newest First)",
                "Status (Alphabetical)"
            });
            cmbSortOrder.SelectedIndex = 0;
            cmbSortOrder.Location = new Point(115, 75);
            cmbSortOrder.Size = new Size(220, 25);
            cmbSortOrder.TabIndex = 0;
            cmbSortOrder.SelectedIndexChanged += CmbSortOrder_SelectedIndexChanged;
            this.Controls.Add(cmbSortOrder);

            // Compare Performance Button
            btnComparePerformance = new Button();
            btnComparePerformance.Text = "📊 Compare Trees";
            btnComparePerformance.Font = new Font("Segoe UI", 9F);
            btnComparePerformance.Location = new Point(350, 75);
            btnComparePerformance.Size = new Size(130, 25);
            btnComparePerformance.BackColor = Color.FromArgb(111, 66, 193);
            btnComparePerformance.ForeColor = Color.White;
            btnComparePerformance.FlatStyle = FlatStyle.Flat;
            btnComparePerformance.FlatAppearance.BorderSize = 0;
            btnComparePerformance.TabIndex = 1;
            btnComparePerformance.Cursor = Cursors.Hand;
            btnComparePerformance.Click += BtnComparePerformance_Click;
            this.Controls.Add(btnComparePerformance);

            // ListView for issues
            lvIssues = new ListView();
            lvIssues.View = View.Details;
            lvIssues.FullRowSelect = true;
            lvIssues.GridLines = true;
            lvIssues.Font = new Font("Segoe UI", 9F);
            lvIssues.Location = new Point(20, 110);
            lvIssues.Size = new Size(900, 380);
            lvIssues.Columns.Add("ID", 60);
            lvIssues.Columns.Add("Location", 180);
            lvIssues.Columns.Add("Category", 140);
            lvIssues.Columns.Add("Status", 100);
            lvIssues.Columns.Add("Reported", 160);
            lvIssues.Columns.Add("Priority", 80);
            lvIssues.HideSelection = false;
            lvIssues.MultiSelect = false;
            lvIssues.SelectedIndexChanged += LvIssues_SelectedIndexChanged;
            this.Controls.Add(lvIssues);

            // Search label
            lblSearch = new Label();
            lblSearch.Text = "Search by ID:";
            lblSearch.Font = new Font("Segoe UI", 9F);
            lblSearch.Location = new Point(20, 510);
            lblSearch.Size = new Size(90, 25);
            this.Controls.Add(lblSearch);

            // Search textbox
            txtSearchId = new TextBox();
            txtSearchId.Font = new Font("Segoe UI", 9F);
            txtSearchId.Location = new Point(115, 507);
            txtSearchId.Size = new Size(100, 28);
            txtSearchId.TabIndex = 2;
            this.Controls.Add(txtSearchId);

            // Find button
            btnFind = new Button();
            btnFind.Text = "🔍 Find";
            btnFind.Font = new Font("Segoe UI", 9F);
            btnFind.Location = new Point(225, 507);
            btnFind.Size = new Size(80, 28);
            btnFind.BackColor = Color.FromArgb(0, 123, 255);
            btnFind.ForeColor = Color.White;
            btnFind.FlatStyle = FlatStyle.Flat;
            btnFind.FlatAppearance.BorderSize = 0;
            btnFind.TabIndex = 3;
            btnFind.Cursor = Cursors.Hand;
            btnFind.Click += BtnFind_Click;
            this.Controls.Add(btnFind);

            // Refresh button
            btnRefresh = new Button();
            btnRefresh.Text = "🔄 Refresh";
            btnRefresh.Font = new Font("Segoe UI", 9F);
            btnRefresh.Location = new Point(315, 507);
            btnRefresh.Size = new Size(90, 28);
            btnRefresh.BackColor = Color.FromArgb(108, 117, 125);
            btnRefresh.ForeColor = Color.White;
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.FlatAppearance.BorderSize = 0;
            btnRefresh.TabIndex = 4;
            btnRefresh.Cursor = Cursors.Hand;
            btnRefresh.Click += BtnRefresh_Click;
            this.Controls.Add(btnRefresh);

            // Show Network Button (Graph Traversal)
            btnShowNetwork = new Button();
            btnShowNetwork.Text = "🌐 Show Network";
            btnShowNetwork.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnShowNetwork.Location = new Point(20, 550);
            btnShowNetwork.Size = new Size(130, 32);
            btnShowNetwork.BackColor = Color.FromArgb(23, 162, 184);
            btnShowNetwork.ForeColor = Color.White;
            btnShowNetwork.FlatStyle = FlatStyle.Flat;
            btnShowNetwork.FlatAppearance.BorderSize = 0;
            btnShowNetwork.TabIndex = 5;
            btnShowNetwork.Cursor = Cursors.Hand;
            btnShowNetwork.Click += BtnShowNetwork_Click;
            this.Controls.Add(btnShowNetwork);

            // Optimal Order Button (MST)
            btnOptimalOrder = new Button();
            btnOptimalOrder.Text = "🌳 Optimal Order";
            btnOptimalOrder.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnOptimalOrder.Location = new Point(160, 550);
            btnOptimalOrder.Size = new Size(130, 32);
            btnOptimalOrder.BackColor = Color.FromArgb(40, 167, 69);
            btnOptimalOrder.ForeColor = Color.White;
            btnOptimalOrder.FlatStyle = FlatStyle.Flat;
            btnOptimalOrder.FlatAppearance.BorderSize = 0;
            btnOptimalOrder.TabIndex = 6;
            btnOptimalOrder.Cursor = Cursors.Hand;
            btnOptimalOrder.Click += BtnOptimalOrder_Click;
            this.Controls.Add(btnOptimalOrder);

            // Heap top label
            lblHeapTop = new Label();
            lblHeapTop.Text = "🔥 Top Priority: -";
            lblHeapTop.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblHeapTop.ForeColor = Color.FromArgb(220, 53, 69);
            lblHeapTop.Location = new Point(20, 595);
            lblHeapTop.Size = new Size(700, 28);
            this.Controls.Add(lblHeapTop);

            // Close button (Back)
            btnClose = new Button();
            btnClose.Text = "← Back";
            btnClose.Font = new Font("Segoe UI", 9F);
            btnClose.Location = new Point(840, 595);
            btnClose.Size = new Size(80, 28);
            btnClose.BackColor = Color.FromArgb(108, 117, 125);
            btnClose.ForeColor = Color.White;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.TabIndex = 7;
            btnClose.Cursor = Cursors.Hand;
            btnClose.Click += BtnClose_Click;
            this.Controls.Add(btnClose);

            this.ResumeLayout(false);
        }

        #endregion
    }
}