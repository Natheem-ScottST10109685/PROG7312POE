using System;
using System.Collections.Generic;

namespace ST10109685Prog7312POE
{
    /// <summary>
    /// Represents a local event or announcement
    /// </summary>
    public class LocalEvent
    {
        public int EventId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public DateTime EventDate { get; set; }
        public string Location { get; set; }
        public DateTime CreatedDate { get; set; }
        public HashSet<string> Tags { get; set; }
        public int Priority { get; set; } // 1 = High, 2 = Medium, 3 = Low

        public LocalEvent()
        {
            Tags = new HashSet<string>();
            CreatedDate = DateTime.Now;
        }

        /// <summary>
        /// Gets a formatted date string for display
        /// </summary>
        public string FormattedDate
        {
            get { return EventDate.ToString("dddd, MMMM dd, yyyy"); }
        }

        /// <summary>
        /// Gets a formatted time string for display
        /// </summary>
        public string FormattedTime
        {
            get { return EventDate.ToString("hh:mm tt"); }
        }

        /// <summary>
        /// Gets the number of days until the event
        /// </summary>
        public int DaysUntilEvent
        {
            get { return (EventDate.Date - DateTime.Now.Date).Days; }
        }

        /// <summary>
        /// Determines if the event is upcoming (in the future)
        /// </summary>
        public bool IsUpcoming
        {
            get { return EventDate > DateTime.Now; }
        }

        public override string ToString()
        {
            return $"{Title} - {FormattedDate}";
        }
    }
}