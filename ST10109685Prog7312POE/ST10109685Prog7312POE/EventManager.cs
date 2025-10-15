using System;
using System.Collections.Generic;
using System.Linq;

namespace ST10109685Prog7312POE
{
    /// <summary>
    /// Manages local events using advanced data structures
    /// </summary>
    public class EventManager
    {
        // Sorted Dictionary to organize events by date (key: date, value: list of events)
        private static SortedDictionary<DateTime, List<LocalEvent>> eventsByDate = new SortedDictionary<DateTime, List<LocalEvent>>();

        // Dictionary to organize events by category
        private static Dictionary<string, List<LocalEvent>> eventsByCategory = new Dictionary<string, List<LocalEvent>>();

        // HashSet to store unique categories
        private static HashSet<string> categories = new HashSet<string>();

        // Queue to track recent searches for recommendation system
        private static Queue<string> searchHistory = new Queue<string>(10);

        // Dictionary to track category preferences (category -> search count)
        private static Dictionary<string, int> categoryPreferences = new Dictionary<string, int>();

        // Stack for undo functionality (optional but useful)
        private static Stack<LocalEvent> recentlyAddedEvents = new Stack<LocalEvent>();

        private static int nextEventId = 1;

        /// <summary>
        /// Initializes the event manager with Cape Town–based sample data.
        /// Locations include real Cape Town venues with Google Maps links for reference.
        /// </summary>
        /// <remarks>
        /// Data generation and localization assisted by ChatGPT (OpenAI, 2025).
        /// </remarks>
        public static void InitializeSampleData()
        {
            ClearAllEvents();

            // Sample categories
            string[] sampleCategories =
            {
        "Community Meeting", "Festival", "Public Notice", "Workshop",
        "Sports Event", "Cultural Event", "Health & Safety", "Maintenance Notice"
    };

            // Add sample events (Fake Events)
            AddEvent("City Council Town Hall",
                "Residents are invited to the quarterly City Council meeting to discuss community upgrades and safety initiatives.",
                "Community Meeting", DateTime.Now.AddDays(7),
                "Cape Town Civic Centre, Hertzog Boulevard", 1,
                new string[] { "governance", "community", "civic" });

            AddEvent("Cape Town Jazz Festival",
                "Experience Africa’s grandest gathering! Local and international artists performing at the CTICC.",
                "Festival", DateTime.Now.AddDays(14),
                "Cape Town International Convention Centre (CTICC)", 2,
                new string[] { "music", "festival", "entertainment" });

            AddEvent("Water Supply Maintenance Notice",
                "Maintenance will affect the Southern Suburbs from 9 AM to 3 PM. Residents are advised to store water in advance.",
                "Maintenance Notice", DateTime.Now.AddDays(3),
                "Southern Suburbs", 1,
                new string[] { "maintenance", "utilities", "water" });

            AddEvent("Community Beach Cleanup",
                "Join the Sea Point community for a beach cleanup. All equipment provided — let’s keep our shores clean!",
                "Community Meeting", DateTime.Now.AddDays(10),
                "Sea Point Pavilion", 2,
                new string[] { "volunteer", "environment", "cleanup" });

            AddEvent("Digital Skills Workshop for Youth",
                "A free computer and coding basics workshop aimed at empowering Cape Town’s youth for the digital future.",
                "Workshop", DateTime.Now.AddDays(5),
                "Khayelitsha Community Hall", 2,
                new string[] { "education", "technology", "youth" });

            AddEvent("Road Closure Notice: N1 Inbound",
                "N1 inbound towards Cape Town will be closed for resurfacing between 9 PM and 4 AM for the next 5 days.",
                "Public Notice", DateTime.Now.AddDays(17),
                "N1 Highway – Goodwood to Woodstock", 1,
                new string[] { "traffic", "roadworks", "notice" });

            AddEvent("Cape Town Marathon",
                "Annual marathon through the city’s scenic routes. Entries now open for runners of all experience levels.",
                "Sports Event", DateTime.Now.AddDays(21),
                "Green Point Stadium", 2,
                new string[] { "sports", "marathon", "fitness" });

            AddEvent("Neighbourgoods Market Reopening",
                "Enjoy fresh produce, gourmet food, and local crafts at the Biscuit Mill this Saturday.",
                "Festival", DateTime.Now.AddDays(2),
                "Old Biscuit Mill, Woodstock", 3,
                new string[] { "market", "local", "food" });

            AddEvent("Fire Safety Awareness Week",
                "Join Cape Town Fire and Rescue for safety demos and learn how to protect your home and family.",
                "Health & Safety", DateTime.Now.AddDays(12),
                "Cape Town Fire & Rescue HQ, Goodwood", 2,
                new string[] { "safety", "education", "emergency" });

            AddEvent("Cape Malay Cultural Celebration",
                "Celebrate heritage with traditional food, music, and dance from Cape Town’s Cape Malay community.",
                "Cultural Event", DateTime.Now.AddDays(25),
                "Bo-Kaap Community Centre", 2,
                new string[] { "culture", "heritage", "community" });

            AddEvent("City Budget Planning Session",
                "An open session for citizens to discuss the upcoming municipal budget and community priorities.",
                "Community Meeting", DateTime.Now.AddDays(8),
                "Cape Town Civic Centre, Council Chamber", 1,
                new string[] { "governance", "budget", "public" });

            AddEvent("Park Renovation Notice",
                "Green Point Urban Park will undergo landscaping upgrades. Portions will be closed temporarily.",
                "Maintenance Notice", DateTime.Now.AddDays(1),
                "Green Point Urban Park", 2,
                new string[] { "park", "maintenance", "renovation" });

            AddEvent("Small Business Growth Workshop",
                "Learn how to register, market, and grow your small business in Cape Town’s economy.",
                "Workshop", DateTime.Now.AddDays(15),
                "Cape Chamber of Commerce, Foreshore", 3,
                new string[] { "business", "entrepreneurship", "training" });

            AddEvent("Children’s Reading Program Launch",
                "Cape Town Library Service launches its summer reading challenge for kids — prizes to be won!",
                "Cultural Event", DateTime.Now.AddDays(30),
                "Central Library, Parade Street", 3,
                new string[] { "education", "children", "library" });

            AddEvent("Emergency Preparedness Fair",
                "Learn about emergency readiness and meet disaster management teams. Free emergency kits for the first 100 families.",
                "Health & Safety", DateTime.Now.AddDays(18),
                "Cape Town Stadium, Green Point", 1,
                new string[] { "safety", "preparedness", "emergency" });
        }


        /// <summary>
        /// Adds a new event to the system
        /// </summary>
        public static int AddEvent(string title, string description, string category, DateTime eventDate, string location, int priority, string[] tags)
        {
            LocalEvent newEvent = new LocalEvent
            {
                EventId = nextEventId++,
                Title = title,
                Description = description,
                Category = category,
                EventDate = eventDate,
                Location = location,
                Priority = priority,
                Tags = new HashSet<string>(tags)
            };

            // Add to sorted dictionary by date
            DateTime dateKey = eventDate.Date;
            if (!eventsByDate.ContainsKey(dateKey))
            {
                eventsByDate[dateKey] = new List<LocalEvent>();
            }
            eventsByDate[dateKey].Add(newEvent);

            // Add to category dictionary
            if (!eventsByCategory.ContainsKey(category))
            {
                eventsByCategory[category] = new List<LocalEvent>();
            }
            eventsByCategory[category].Add(newEvent);

            // Add category to set
            categories.Add(category);

            // Add to recent stack
            recentlyAddedEvents.Push(newEvent);

            return newEvent.EventId;
        }

        /// <summary>
        /// Gets all unique categories
        /// </summary>
        public static HashSet<string> GetCategories()
        {
            return new HashSet<string>(categories);
        }

        /// <summary>
        /// Searches events by category and optional date range
        /// </summary>
        public static List<LocalEvent> SearchEvents(string category, DateTime? startDate = null, DateTime? endDate = null)
        {
            List<LocalEvent> results = new List<LocalEvent>();

            // Track search for recommendations
            if (!string.IsNullOrEmpty(category) && category != "All Categories")
            {
                TrackSearch(category);
            }

            if (category == "All Categories" || string.IsNullOrEmpty(category))
            {
                // Get all events
                foreach (var eventList in eventsByDate.Values)
                {
                    results.AddRange(eventList);
                }
            }
            else if (eventsByCategory.ContainsKey(category))
            {
                results.AddRange(eventsByCategory[category]);
            }

            // Filter by date range if specified
            if (startDate.HasValue)
            {
                results = results.Where(e => e.EventDate >= startDate.Value).ToList();
            }
            if (endDate.HasValue)
            {
                results = results.Where(e => e.EventDate <= endDate.Value).ToList();
            }

            // Sort by date, then by priority
            return results.OrderBy(e => e.EventDate).ThenBy(e => e.Priority).ToList();
        }

        /// <summary>
        /// Gets upcoming events (future events only)
        /// </summary>
        public static List<LocalEvent> GetUpcomingEvents(int limit = 0)
        {
            List<LocalEvent> upcoming = new List<LocalEvent>();

            foreach (var kvp in eventsByDate)
            {
                if (kvp.Key >= DateTime.Now.Date)
                {
                    foreach (var evt in kvp.Value)
                    {
                        if (evt.IsUpcoming)
                        {
                            upcoming.Add(evt);
                        }
                    }
                }
            }

            var sorted = upcoming.OrderBy(e => e.EventDate).ThenBy(e => e.Priority).ToList();

            if (limit > 0 && sorted.Count > limit)
            {
                return sorted.Take(limit).ToList();
            }

            return sorted;
        }

        /// <summary>
        /// Tracks search patterns for recommendation system
        /// </summary>
        private static void TrackSearch(string category)
        {
            // Add to search history queue
            if (searchHistory.Count >= 10)
            {
                searchHistory.Dequeue();
            }
            searchHistory.Enqueue(category);

            // Update category preferences
            if (categoryPreferences.ContainsKey(category))
            {
                categoryPreferences[category]++;
            }
            else
            {
                categoryPreferences[category] = 1;
            }
        }

        /// <summary>
        /// Gets recommended events based on user search patterns
        /// </summary>
        public static List<LocalEvent> GetRecommendedEvents(int limit = 5)
        {
            List<LocalEvent> recommended = new List<LocalEvent>();

            if (categoryPreferences.Count == 0)
            {
                // No search history, return most recent upcoming events
                return GetUpcomingEvents(limit);
            }

            // Get top preferred categories
            var topCategories = categoryPreferences
                .OrderByDescending(kvp => kvp.Value)
                .Take(3)
                .Select(kvp => kvp.Key)
                .ToList();

            // Get events from preferred categories
            foreach (var category in topCategories)
            {
                if (eventsByCategory.ContainsKey(category))
                {
                    var categoryEvents = eventsByCategory[category]
                        .Where(e => e.IsUpcoming)
                        .OrderBy(e => e.EventDate)
                        .ToList();

                    recommended.AddRange(categoryEvents);
                }
            }

            // Remove duplicates and sort
            recommended = recommended
                .Distinct()
                .OrderBy(e => e.EventDate)
                .ThenBy(e => e.Priority)
                .ToList();

            if (limit > 0 && recommended.Count > limit)
            {
                return recommended.Take(limit).ToList();
            }

            return recommended;
        }

        /// <summary>
        /// Gets events by specific date
        /// </summary>
        public static List<LocalEvent> GetEventsByDate(DateTime date)
        {
            DateTime dateKey = date.Date;
            if (eventsByDate.ContainsKey(dateKey))
            {
                return new List<LocalEvent>(eventsByDate[dateKey]);
            }
            return new List<LocalEvent>();
        }

        /// <summary>
        /// Gets total event count
        /// </summary>
        public static int GetEventCount()
        {
            return eventsByDate.Values.Sum(list => list.Count);
        }

        /// <summary>
        /// Gets category statistics
        /// </summary>
        public static Dictionary<string, int> GetCategoryStatistics()
        {
            return eventsByCategory.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Count);
        }

        /// <summary>
        /// Clears all events
        /// </summary>
        public static void ClearAllEvents()
        {
            eventsByDate.Clear();
            eventsByCategory.Clear();
            categories.Clear();
            searchHistory.Clear();
            categoryPreferences.Clear();
            recentlyAddedEvents.Clear();
            nextEventId = 1;
        }

        /// <summary>
        /// Gets search history for analysis
        /// </summary>
        public static List<string> GetSearchHistory()
        {
            return searchHistory.ToList();
        }
    }
}