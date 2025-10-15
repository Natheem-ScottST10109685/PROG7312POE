using System;
using System.Collections.Generic;
using System.Linq;

namespace ST10109685Prog7312POE
{
    /// <summary>
    /// Manages reported issues using a multi-dimensional array structure
    /// </summary>
    internal class IssueManager
    {
        // Static list to store all reported issues
        private static List<ReportedIssue> reportedIssues = new List<ReportedIssue>();
        private static int nextIssueId = 1;

        /// <summary>
        /// Adds a new issue to the system
        /// </summary>
        /// <param name="location">Location of the issue</param>
        /// <param name="category">Category of the issue</param>
        /// <param name="description">Description of the issue</param>
        /// <param name="attachedFiles">List of attached file paths</param>
        /// <returns>The ID of the newly created issue</returns>
        public static int AddIssue(string location, string category, string description, List<string> attachedFiles)
        {
            ReportedIssue newIssue = new ReportedIssue
            {
                IssueId = nextIssueId++,
                Location = location,
                Category = category,
                Description = description,
                AttachedFiles = new List<string>(attachedFiles),
                ReportedDate = DateTime.Now
            };

            reportedIssues.Add(newIssue);
            return newIssue.IssueId;
        }

        /// <summary>
        /// Gets an issue by its ID
        /// </summary>
        /// <param name="issueId">The ID of the issue to retrieve</param>
        /// <returns>The ReportedIssue object or null if not found</returns>
        public static ReportedIssue GetIssueById(int issueId)
        {
            return reportedIssues.FirstOrDefault(issue => issue.IssueId == issueId);
        }

        /// <summary>
        /// Gets the total count of reported issues
        /// </summary>
        /// <returns>Number of issues reported</returns>
        public static int GetIssueCount()
        {
            return reportedIssues.Count;
        }

        /// <summary>
        /// Gets all reported issues
        /// </summary>
        /// <returns>List of all reported issues</returns>
        public static List<ReportedIssue> GetAllIssues()
        {
            return new List<ReportedIssue>(reportedIssues);
        }

        /// <summary>
        /// Gets statistics about issues by category
        /// </summary>
        /// <returns>Dictionary with category names and their counts</returns>
        public static Dictionary<string, int> GetCategoryStatistics()
        {
            Dictionary<string, int> statistics = new Dictionary<string, int>();

            foreach (var issue in reportedIssues)
            {
                if (statistics.ContainsKey(issue.Category))
                {
                    statistics[issue.Category]++;
                }
                else
                {
                    statistics[issue.Category] = 1;
                }
            }

            // Sort by count descending
            return statistics.OrderByDescending(x => x.Value)
                           .ToDictionary(x => x.Key, x => x.Value);
        }

        /// <summary>
        /// Clears all reported issues (useful for testing)
        /// </summary>
        public static void ClearAllIssues()
        {
            reportedIssues.Clear();
            nextIssueId = 1;
        }
    }

}