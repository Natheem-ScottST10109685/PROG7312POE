using System;
using System.Collections.Generic;

namespace ST10109685Prog7312POE
{
    /// <summary>
    /// Utility class to generate sample service request data for testing
    /// </summary>
    public static class SampleDataLoader
    {
        /// <summary>
        /// Load sample service requests into IssueManager for Cape Town
        /// </summary>
        public static void LoadSampleData()
        {
            // Clear existing issues
            IssueManager.ClearAllIssues();

            // Cape Town locations 
            string[] locations = new string[]
            {
                "Long Street, Cape Town CBD",
                "Sea Point Promenade",
                "Khayelitsha Site C",
                "Mitchells Plain Town Centre",
                "Claremont Main Road",
                "Bellville Transport Hub",
                "Observatory Village Green",
                "Woodstock Albert Road",
                "Muizenberg Beachfront",
                "Table View Bayside Mall",
                "Rondebosch Common",
                "Durbanville Town Square",
                "Langa Taxi Rank",
                "Green Point Urban Park",
                "Camps Bay Drive"
            };

            // Cape Town municipal service categories
            string[] categories = new string[]
            {
                "Water & Sanitation",
                "Roads & Transport",
                "Electricity",
                "Public Safety",
                "Waste Management",
                "Street Lighting",
                "Parks & Recreation",
                "Health Services"
            };

            // Cape Town-based issue descriptions
            string[] descriptions = new string[]
            {
                "Burst water pipe along Main Road in Claremont causing flooding near shops.",
                "Major pothole on Spine Road, Mitchells Plain. Vehicles swerving to avoid damage.",
                "Street light out near Green Point Park entrance. Area very dark at night.",
                "Illegal dumping along the N2 near Langa. Piles of rubbish attracting rodents.",
                "Power outage in Woodstock affecting several residential blocks.",
                "Playground in Sea Point needs urgent maintenance — damaged swing equipment.",
                "Graffiti on public toilets at Muizenberg Beach — requires cleaning.",
                "Stray dogs near Bellville taxi rank causing concern for commuters.",
                "Traffic light malfunction at Koeberg Road intersection, creating congestion.",
                "Benches at Rondebosch Common vandalized. Replacement required.",
                "Blocked sewer in Khayelitsha causing strong odors in the community.",
                "Overgrown vegetation obscuring road signs near Camps Bay Drive.",
                "Abandoned car near Observatory Park for over two weeks.",
                "Multiple complaints of low water pressure in Durbanville suburb.",
                "Road markings faded along Long Street, increasing risk for pedestrians."
            };

            // Generate 15 realistic sample issues
            Random rand = new Random(42); // Fixed seed for reproducibility

            for (int i = 0; i < 15; i++)
            {
                string location = locations[rand.Next(locations.Length)];
                string category = categories[rand.Next(categories.Length)];
                string description = descriptions[rand.Next(descriptions.Length)];

                // Random report date (0–30 days ago)
                DateTime reportDate = DateTime.Now.AddDays(-rand.Next(0, 31));

                // Random attachments (photos)
                List<string> attachments = new List<string>();
                if (rand.Next(100) > 60) // ~40% of reports have photos
                {
                    int attachmentCount = rand.Next(1, 4);
                    for (int j = 0; j < attachmentCount; j++)
                    {
                        attachments.Add($"cape_town_issue_{i}_{j}.jpg");
                    }
                }

                // Add the issue
                int issueId = IssueManager.AddIssue(location, category, description, attachments);

                // Adjust the reported date (for realistic data variety)
                var issue = IssueManager.GetIssueById(issueId);
                if (issue != null)
                {
                    issue.ReportedDate = reportDate;
                }
            }
        }

        /// <summary>
        /// Display a summary of the loaded Cape Town test data
        /// </summary>
        public static string GetSampleDataSummary()
        {
            int count = IssueManager.GetIssueCount();
            var stats = IssueManager.GetCategoryStatistics();

            string summary = $"📍 Cape Town Sample Data Loaded Successfully!\n\n";
            summary += $"Total Issues: {count}\n\n";
            summary += "Category Breakdown:\n";

            foreach (var stat in stats)
            {
                summary += $"  • {stat.Key}: {stat.Value} issue(s)\n";
            }

            summary += "\n✅ Ready for testing all data structures:\n";
            summary += "  • Binary Search Tree (BST)\n";
            summary += "  • AVL Tree (Balanced)\n";
            summary += "  • Red-Black Tree (Balanced)\n";
            summary += "  • Min Heap (Priority Queue)\n";
            summary += "  • Graph with BFS/DFS\n";
            summary += "  • Minimum Spanning Tree (MST)";

            return summary;
        }

        /// <summary>
        /// Quick load with confirmation dialog
        /// </summary>
        public static void LoadWithConfirmation()
        {
            LoadSampleData();
            System.Windows.Forms.MessageBox.Show(
                GetSampleDataSummary(),
                "Cape Town Sample Data Loaded",
                System.Windows.Forms.MessageBoxButtons.OK,
                System.Windows.Forms.MessageBoxIcon.Information);
        }
    }
}
