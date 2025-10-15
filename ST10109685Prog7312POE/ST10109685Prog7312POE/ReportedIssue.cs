using System;
using System.Collections.Generic;

namespace ST10109685Prog7312POE
{
    /// <summary>
    /// Data structure to store reported issues with multi-dimensional array support
    /// </summary>
    public class ReportedIssue
    {
        public int IssueId { get; set; }
        public string Location { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public DateTime ReportedDate { get; set; }
        public List<string> AttachedFiles { get; set; }
        public string Status { get; set; }

        public ReportedIssue()
        {
            AttachedFiles = new List<string>();
            ReportedDate = DateTime.Now;
            Status = "Submitted";
        }

        /// <summary>
        /// Converts the issue to a string array for multi-dimensional storage
        /// </summary>
        public string[] ToArray()
        {
            return new string[]
            {
                IssueId.ToString(),
                Location,
                Category,
                Description,
                ReportedDate.ToString("yyyy-MM-dd HH:mm:ss"),
                string.Join(";", AttachedFiles),
                Status
            };
        }

        /// <summary>
        /// Creates a ReportedIssue from a string array
        /// </summary>
        public static ReportedIssue FromArray(string[] data)
        {
            if (data == null || data.Length < 7)
                return null;

            var issue = new ReportedIssue
            {
                IssueId = int.Parse(data[0]),
                Location = data[1],
                Category = data[2],
                Description = data[3],
                ReportedDate = DateTime.Parse(data[4]),
                Status = data[6]
            };

            if (!string.IsNullOrEmpty(data[5]))
            {
                issue.AttachedFiles = new List<string>(data[5].Split(';'));
            }

            return issue;
        }
    }
}