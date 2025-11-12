using System;
using System.Collections.Generic;
using System.Linq;

namespace ST10109685Prog7312POE
{
    /// <summary>
    /// Centralized manager for all reported issues using multi-dimensional arrays
    /// and supporting advanced data structures
    /// </summary>
    public static class IssueManager
    {
        // Multi-dimensional array to store issues (max 1000 issues, 7 properties each)
        // Columns: [0]=IssueId, [1]=Location, [2]=Category, [3]=Description, 
        //          [4]=ReportedDate, [5]=AttachedFiles, [6]=Status
        private static string[,] issueStorage = new string[1000, 7];
        private static int issueCount = 0;
        private static int nextIssueId = 1;
        private static object lockObject = new object();

        /// <summary>
        /// Add a new issue to the storage using multi-dimensional array
        /// </summary>
        /// <param name="location">Location of the issue</param>
        /// <param name="category">Category of the issue</param>
        /// <param name="description">Description of the issue</param>
        /// <param name="attachedFiles">List of attached file paths</param>
        /// <returns>The ID of the newly created issue</returns>
        public static int AddIssue(string location, string category, string description, List<string> attachedFiles)
        {
            lock (lockObject)
            {
                if (issueCount >= 1000)
                    throw new InvalidOperationException("Maximum issue capacity (1000) reached");

                ReportedIssue newIssue = new ReportedIssue
                {
                    IssueId = nextIssueId++,
                    Location = location,
                    Category = category,
                    Description = description,
                    AttachedFiles = new List<string>(attachedFiles ?? new List<string>()),
                    ReportedDate = DateTime.Now,
                    Status = "Submitted"
                };

                // Convert to array and store in multi-dimensional array
                string[] issueArray = newIssue.ToArray();
                for (int col = 0; col < issueArray.Length; col++)
                {
                    issueStorage[issueCount, col] = issueArray[col];
                }

                issueCount++;
                return newIssue.IssueId;
            }
        }

        /// <summary>
        /// Gets an issue by its ID from the multi-dimensional array
        /// </summary>
        /// <param name="issueId">The ID of the issue to retrieve</param>
        /// <returns>The ReportedIssue object or null if not found</returns>
        public static ReportedIssue GetIssueById(int issueId)
        {
            lock (lockObject)
            {
                for (int row = 0; row < issueCount; row++)
                {
                    if (issueStorage[row, 0] == issueId.ToString())
                    {
                        return ExtractIssueFromRow(row);
                    }
                }
                return null;
            }
        }

        /// <summary>
        /// Get total count of issues
        /// </summary>
        /// <returns>Number of issues reported</returns>
        public static int GetIssueCount()
        {
            return issueCount;
        }

        /// <summary>
        /// Get all issues as a list from multi-dimensional array
        /// </summary>
        /// <returns>List of all reported issues</returns>
        public static List<ReportedIssue> GetAllIssues()
        {
            lock (lockObject)
            {
                List<ReportedIssue> issues = new List<ReportedIssue>();

                for (int row = 0; row < issueCount; row++)
                {
                    ReportedIssue issue = ExtractIssueFromRow(row);
                    if (issue != null)
                    {
                        issues.Add(issue);
                    }
                }

                return issues;
            }
        }

        /// <summary>
        /// Gets statistics about issues by category
        /// </summary>
        /// <returns>Dictionary with category names and their counts</returns>
        public static Dictionary<string, int> GetCategoryStatistics()
        {
            lock (lockObject)
            {
                Dictionary<string, int> statistics = new Dictionary<string, int>();

                for (int row = 0; row < issueCount; row++)
                {
                    string category = issueStorage[row, 2]; // Column 2 = Category

                    if (statistics.ContainsKey(category))
                    {
                        statistics[category]++;
                    }
                    else
                    {
                        statistics[category] = 1;
                    }
                }

                // Sort by count descending
                return statistics.OrderByDescending(x => x.Value)
                               .ToDictionary(x => x.Key, x => x.Value);
            }
        }

        /// <summary>
        /// Updates the status of an issue
        /// </summary>
        /// <param name="issueId">The ID of the issue to update</param>
        /// <param name="newStatus">The new status</param>
        /// <returns>True if successful, false if issue not found</returns>
        public static bool UpdateIssueStatus(int issueId, string newStatus)
        {
            lock (lockObject)
            {
                for (int row = 0; row < issueCount; row++)
                {
                    if (issueStorage[row, 0] == issueId.ToString())
                    {
                        issueStorage[row, 6] = newStatus; // Column 6 = Status
                        return true;
                    }
                }
                return false;
            }
        }

        /// <summary>
        /// Deletes an issue by ID
        /// </summary>
        /// <param name="issueId">The ID of the issue to delete</param>
        /// <returns>True if successful, false if issue not found</returns>
        public static bool DeleteIssue(int issueId)
        {
            lock (lockObject)
            {
                for (int row = 0; row < issueCount; row++)
                {
                    if (issueStorage[row, 0] == issueId.ToString())
                    {
                        // Shift all rows up to remove the gap
                        for (int i = row; i < issueCount - 1; i++)
                        {
                            for (int col = 0; col < 7; col++)
                            {
                                issueStorage[i, col] = issueStorage[i + 1, col];
                            }
                        }

                        // Clear the last row
                        for (int col = 0; col < 7; col++)
                        {
                            issueStorage[issueCount - 1, col] = null;
                        }

                        issueCount--;
                        return true;
                    }
                }
                return false;
            }
        }

        /// <summary>
        /// Search issues by location (partial match)
        /// </summary>
        /// <param name="locationQuery">Location search query</param>
        /// <returns>List of matching issues</returns>
        public static List<ReportedIssue> SearchByLocation(string locationQuery)
        {
            lock (lockObject)
            {
                List<ReportedIssue> results = new List<ReportedIssue>();

                for (int row = 0; row < issueCount; row++)
                {
                    string location = issueStorage[row, 1]; // Column 1 = Location
                    if (location != null && location.IndexOf(locationQuery, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        results.Add(ExtractIssueFromRow(row));
                    }
                }

                return results;
            }
        }

        /// <summary>
        /// Search issues by category
        /// </summary>
        /// <param name="category">Category to filter by</param>
        /// <returns>List of matching issues</returns>
        public static List<ReportedIssue> SearchByCategory(string category)
        {
            lock (lockObject)
            {
                List<ReportedIssue> results = new List<ReportedIssue>();

                for (int row = 0; row < issueCount; row++)
                {
                    if (issueStorage[row, 2] == category) // Column 2 = Category
                    {
                        results.Add(ExtractIssueFromRow(row));
                    }
                }

                return results;
            }
        }

        /// <summary>
        /// Search issues by status
        /// </summary>
        /// <param name="status">Status to filter by</param>
        /// <returns>List of matching issues</returns>
        public static List<ReportedIssue> SearchByStatus(string status)
        {
            lock (lockObject)
            {
                List<ReportedIssue> results = new List<ReportedIssue>();

                for (int row = 0; row < issueCount; row++)
                {
                    if (issueStorage[row, 6] == status) // Column 6 = Status
                    {
                        results.Add(ExtractIssueFromRow(row));
                    }
                }

                return results;
            }
        }

        /// <summary>
        /// Get issues within a date range
        /// </summary>
        /// <param name="startDate">Start date</param>
        /// <param name="endDate">End date</param>
        /// <returns>List of issues within date range</returns>
        public static List<ReportedIssue> GetIssuesByDateRange(DateTime startDate, DateTime endDate)
        {
            lock (lockObject)
            {
                List<ReportedIssue> results = new List<ReportedIssue>();

                for (int row = 0; row < issueCount; row++)
                {
                    string dateStr = issueStorage[row, 4]; // Column 4 = ReportedDate
                    if (DateTime.TryParse(dateStr, out DateTime reportedDate))
                    {
                        if (reportedDate >= startDate && reportedDate <= endDate)
                        {
                            results.Add(ExtractIssueFromRow(row));
                        }
                    }
                }

                return results.OrderByDescending(i => i.ReportedDate).ToList();
            }
        }

        /// <summary>
        /// Get a 2D array slice for a specific issue (useful for demonstrations)
        /// </summary>
        /// <param name="issueId">Issue ID to get</param>
        /// <returns>String array representing the issue</returns>
        public static string[] GetIssueArrayById(int issueId)
        {
            lock (lockObject)
            {
                for (int row = 0; row < issueCount; row++)
                {
                    if (issueStorage[row, 0] == issueId.ToString())
                    {
                        string[] issueArray = new string[7];
                        for (int col = 0; col < 7; col++)
                        {
                            issueArray[col] = issueStorage[row, col];
                        }
                        return issueArray;
                    }
                }
                return null;
            }
        }

        /// <summary>
        /// Export all issues to a 2D array (useful for data export/analysis)
        /// </summary>
        /// <returns>2D array containing all issues</returns>
        public static string[,] ExportToArray()
        {
            lock (lockObject)
            {
                string[,] export = new string[issueCount, 7];

                for (int row = 0; row < issueCount; row++)
                {
                    for (int col = 0; col < 7; col++)
                    {
                        export[row, col] = issueStorage[row, col];
                    }
                }

                return export;
            }
        }

        /// <summary>
        /// Clears all reported issues (useful for testing)
        /// </summary>
        public static void ClearAllIssues()
        {
            lock (lockObject)
            {
                // Clear the multi-dimensional array
                for (int row = 0; row < issueCount; row++)
                {
                    for (int col = 0; col < 7; col++)
                    {
                        issueStorage[row, col] = null;
                    }
                }

                issueCount = 0;
                nextIssueId = 1;
            }
        }

        /// <summary>
        /// Helper method to extract a ReportedIssue from a specific row
        /// </summary>
        /// <param name="row">Row index in the multi-dimensional array</param>
        /// <returns>ReportedIssue object</returns>
        private static ReportedIssue ExtractIssueFromRow(int row)
        {
            string[] issueData = new string[7];
            for (int col = 0; col < 7; col++)
            {
                issueData[col] = issueStorage[row, col];
            }

            return ReportedIssue.FromArray(issueData);
        }

        /// <summary>
        /// Get summary statistics about all issues
        /// </summary>
        /// <returns>Dictionary with various statistics</returns>
        public static Dictionary<string, object> GetSummaryStatistics()
        {
            lock (lockObject)
            {
                var stats = new Dictionary<string, object>();

                stats["TotalIssues"] = issueCount;
                stats["CapacityRemaining"] = 1000 - issueCount;
                stats["CategoryBreakdown"] = GetCategoryStatistics();

                // Count by status
                var statusCount = new Dictionary<string, int>();
                for (int row = 0; row < issueCount; row++)
                {
                    string status = issueStorage[row, 6];
                    if (statusCount.ContainsKey(status))
                        statusCount[status]++;
                    else
                        statusCount[status] = 1;
                }
                stats["StatusBreakdown"] = statusCount;

                // Find oldest and newest
                DateTime? oldest = null;
                DateTime? newest = null;

                for (int row = 0; row < issueCount; row++)
                {
                    if (DateTime.TryParse(issueStorage[row, 4], out DateTime date))
                    {
                        if (!oldest.HasValue || date < oldest.Value)
                            oldest = date;
                        if (!newest.HasValue || date > newest.Value)
                            newest = date;
                    }
                }

                stats["OldestIssueDate"] = oldest;
                stats["NewestIssueDate"] = newest;

                return stats;
            }
        }

        /// <summary>
        /// Validate data integrity of the multi-dimensional array
        /// </summary>
        /// <returns>True if data is valid, false otherwise</returns>
        public static bool ValidateDataIntegrity()
        {
            lock (lockObject)
            {
                for (int row = 0; row < issueCount; row++)
                {
                    // Check that all required columns have data
                    for (int col = 0; col < 7; col++)
                    {
                        if (col != 5 && string.IsNullOrEmpty(issueStorage[row, col])) // col 5 = attachments (can be empty)
                        {
                            return false;
                        }
                    }

                    // Validate ID is numeric
                    if (!int.TryParse(issueStorage[row, 0], out _))
                    {
                        return false;
                    }

                    // Validate date format
                    if (!DateTime.TryParse(issueStorage[row, 4], out _))
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        /// <summary>
        /// Gets the next available issue ID (useful for testing)
        /// </summary>
        /// <returns>Next issue ID that will be assigned</returns>
        public static int GetNextIssueId()
        {
            return nextIssueId;
        }

        /// <summary>
        /// Gets the current storage capacity usage as percentage
        /// </summary>
        /// <returns>Percentage (0-100) of storage used</returns>
        public static double GetStorageUsagePercentage()
        {
            return (issueCount / 1000.0) * 100.0;
        }
    }
}