using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ST10109685Prog7312POE
{
    public partial class LocalEventsForm : Form
    {
        private List<LocalEvent> currentEvents;

        public LocalEventsForm()
        {
            InitializeComponent();
            this.Load += LocalEventsForm_Load;

            // Add hover effects
            btnBack.MouseEnter += (s, e) => btnBack.BackColor = Color.FromArgb(62, 118, 155);
            btnBack.MouseLeave += (s, e) => btnBack.BackColor = Color.FromArgb(52, 98, 135);

            btnSearch.MouseEnter += (s, e) => btnSearch.BackColor = Color.FromArgb(40, 167, 69);
            btnSearch.MouseLeave += (s, e) => btnSearch.BackColor = Color.FromArgb(34, 139, 34);

            btnClearFilter.MouseEnter += (s, e) => btnClearFilter.BackColor = Color.FromArgb(85, 150, 200);
            btnClearFilter.MouseLeave += (s, e) => btnClearFilter.BackColor = Color.FromArgb(70, 130, 180);
        }

        /// <summary>
        /// Form load event - initialize data
        /// </summary>
        private void LocalEventsForm_Load(object sender, EventArgs e)
        {
            // Initialize sample data if needed
            if (EventManager.GetEventCount() == 0)
            {
                EventManager.InitializeSampleData();
            }

            // Populate category dropdown
            LoadCategories();

            // Set default date range
            dtpStartDate.Value = DateTime.Now;
            dtpEndDate.Value = DateTime.Now.AddMonths(3);

            // Load all upcoming events initially
            LoadEvents();

            // Update statistics
            UpdateStatistics();

            // Load recommendations
            LoadRecommendations();
        }

        /// <summary>
        /// Loads categories into the combo box
        /// </summary>
        private void LoadCategories()
        {
            cmbCategory.Items.Clear();
            cmbCategory.Items.Add("All Categories");

            var categories = EventManager.GetCategories().OrderBy(c => c);
            foreach (var category in categories)
            {
                cmbCategory.Items.Add(category);
            }

            cmbCategory.SelectedIndex = 0;
        }

        /// <summary>
        /// Loads events based on current filters
        /// </summary>
        private void LoadEvents()
        {
            lstEvents.Items.Clear();
            currentEvents = new List<LocalEvent>();

            string selectedCategory = cmbCategory.SelectedItem?.ToString() ?? "All Categories";
            DateTime? startDate = dtpStartDate.Value.Date;
            DateTime? endDate = dtpEndDate.Value.Date.AddDays(1).AddSeconds(-1);

            // Get filtered events
            if (chkUpcomingOnly.Checked)
            {
                if (selectedCategory == "All Categories")
                {
                    currentEvents = EventManager.GetUpcomingEvents();
                }
                else
                {
                    currentEvents = EventManager.SearchEvents(selectedCategory, DateTime.Now, endDate);
                }
            }
            else
            {
                currentEvents = EventManager.SearchEvents(selectedCategory, startDate, endDate);
            }

            // Populate list box
            foreach (var evt in currentEvents)
            {
                string displayText = FormatEventListItem(evt);
                lstEvents.Items.Add(displayText);
            }

            // Update subtitle with count
            lblSubtitle.Text = $"Showing {currentEvents.Count} event(s)";

            // Clear details if no events
            if (currentEvents.Count == 0)
            {
                rtbEventDetails.Clear();
                rtbEventDetails.Text = "No events found matching your criteria.";
            }
            else if (lstEvents.Items.Count > 0)
            {
                lstEvents.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Formats an event for display in the list
        /// </summary>
        private string FormatEventListItem(LocalEvent evt)
        {
            string priorityIndicator = evt.Priority == 1 ? "🔴 " : evt.Priority == 2 ? "🟡 " : "🟢 ";
            int daysUntil = evt.DaysUntilEvent;
            string daysText = daysUntil == 0 ? "Today" :
                             daysUntil == 1 ? "Tomorrow" :
                             daysUntil < 0 ? $"{Math.Abs(daysUntil)} days ago" :
                             $"In {daysUntil} days";

            return $"{priorityIndicator}{evt.Title} - {evt.FormattedDate} ({daysText})";
        }

        /// <summary>
        /// Updates the statistics panel
        /// </summary>
        private void UpdateStatistics()
        {
            int totalEvents = EventManager.GetEventCount();
            int upcomingEvents = EventManager.GetUpcomingEvents().Count;

            lblTotalEvents.Text = $"Total Events: {totalEvents}";
            lblUpcomingEvents.Text = $"Upcoming: {upcomingEvents}";
        }

        /// <summary>
        /// Loads recommended events based on search patterns
        /// </summary>
        private void LoadRecommendations()
        {
            lstRecommendations.Items.Clear();

            var recommendations = EventManager.GetRecommendedEvents(5);

            if (recommendations.Count == 0)
            {
                lstRecommendations.Items.Add("No recommendations yet. Search to get personalized suggestions!");
                return;
            }

            foreach (var evt in recommendations)
            {
                string displayText = $"⭐ {evt.Title} - {evt.EventDate:MMM dd}";
                lstRecommendations.Items.Add(displayText);
            }
        }

        /// <summary>
        /// Displays detailed information about the selected event
        /// </summary>
        private void DisplayEventDetails(LocalEvent evt)
        {
            if (evt == null)
            {
                rtbEventDetails.Clear();
                return;
            }

            rtbEventDetails.Clear();

            // Priority indicator
            string priorityText = evt.Priority == 1 ? "HIGH PRIORITY" :
                                 evt.Priority == 2 ? "MEDIUM PRIORITY" :
                                 "LOW PRIORITY";
            Color priorityColor = evt.Priority == 1 ? Color.FromArgb(220, 20, 60) :
                                 evt.Priority == 2 ? Color.FromArgb(255, 165, 0) :
                                 Color.FromArgb(34, 139, 34);

            // Build details text
            rtbEventDetails.SelectionFont = new Font("Segoe UI", 12F, FontStyle.Bold);
            rtbEventDetails.SelectionColor = Color.FromArgb(70, 130, 180);
            rtbEventDetails.AppendText(evt.Title + "\n\n");

            rtbEventDetails.SelectionFont = new Font("Segoe UI", 9F, FontStyle.Bold);
            rtbEventDetails.SelectionColor = priorityColor;
            rtbEventDetails.AppendText(priorityText + "\n\n");

            rtbEventDetails.SelectionFont = new Font("Segoe UI", 10F, FontStyle.Bold);
            rtbEventDetails.SelectionColor = Color.Black;
            rtbEventDetails.AppendText("Category: ");
            rtbEventDetails.SelectionFont = new Font("Segoe UI", 10F);
            rtbEventDetails.AppendText(evt.Category + "\n\n");

            rtbEventDetails.SelectionFont = new Font("Segoe UI", 10F, FontStyle.Bold);
            rtbEventDetails.AppendText("Date & Time:\n");
            rtbEventDetails.SelectionFont = new Font("Segoe UI", 10F);
            rtbEventDetails.AppendText($"{evt.FormattedDate}\n");
            rtbEventDetails.AppendText($"{evt.FormattedTime}\n\n");

            // Days until event
            int daysUntil = evt.DaysUntilEvent;
            if (daysUntil >= 0)
            {
                rtbEventDetails.SelectionFont = new Font("Segoe UI", 9F, FontStyle.Italic);
                rtbEventDetails.SelectionColor = Color.FromArgb(70, 130, 180);
                string daysText = daysUntil == 0 ? "Happening Today!" :
                                 daysUntil == 1 ? "Tomorrow" :
                                 $"In {daysUntil} days";
                rtbEventDetails.AppendText(daysText + "\n\n");
            }

            rtbEventDetails.SelectionFont = new Font("Segoe UI", 10F, FontStyle.Bold);
            rtbEventDetails.SelectionColor = Color.Black;
            rtbEventDetails.AppendText("Location: ");
            rtbEventDetails.SelectionFont = new Font("Segoe UI", 10F);
            rtbEventDetails.AppendText(evt.Location + "\n\n");

            rtbEventDetails.SelectionFont = new Font("Segoe UI", 10F, FontStyle.Bold);
            rtbEventDetails.AppendText("Description:\n");
            rtbEventDetails.SelectionFont = new Font("Segoe UI", 10F);
            rtbEventDetails.AppendText(evt.Description + "\n");

            // Tags if available
            if (evt.Tags != null && evt.Tags.Count > 0)
            {
                rtbEventDetails.SelectionFont = new Font("Segoe UI", 9F, FontStyle.Italic);
                rtbEventDetails.SelectionColor = Color.Gray;
                rtbEventDetails.AppendText("\nTags: " + string.Join(", ", evt.Tags));
            }
        }

        /// <summary>
        /// Event handler for search button
        /// </summary>
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            LoadEvents();
            LoadRecommendations();
        }

        /// <summary>
        /// Event handler for clear filter button
        /// </summary>
        private void BtnClearFilter_Click(object sender, EventArgs e)
        {
            cmbCategory.SelectedIndex = 0;
            chkUpcomingOnly.Checked = true;
            dtpStartDate.Value = DateTime.Now;
            dtpEndDate.Value = DateTime.Now.AddMonths(3);
            LoadEvents();
        }

        /// <summary>
        /// Event handler for event list selection
        /// </summary>
        private void LstEvents_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstEvents.SelectedIndex >= 0 && lstEvents.SelectedIndex < currentEvents.Count)
            {
                LocalEvent selectedEvent = currentEvents[lstEvents.SelectedIndex];
                DisplayEventDetails(selectedEvent);
            }
        }

        /// <summary>
        /// Event handler for recommendations list selection
        /// </summary>
        private void LstRecommendations_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstRecommendations.SelectedIndex >= 0)
            {
                var recommendations = EventManager.GetRecommendedEvents(5);
                if (lstRecommendations.SelectedIndex < recommendations.Count)
                {
                    LocalEvent selectedEvent = recommendations[lstRecommendations.SelectedIndex];
                    DisplayEventDetails(selectedEvent);

                    // Find and select in main list if visible
                    for (int i = 0; i < currentEvents.Count; i++)
                    {
                        if (currentEvents[i].EventId == selectedEvent.EventId)
                        {
                            lstEvents.SelectedIndex = i;
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Event handler for back button
        /// </summary>
        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}