using System;
using System.Collections.Generic;
using MunicipalServicesApp.Models;

namespace MunicipalServicesApp.Data
{
    /// <summary>
    /// In-memory catalogue for Local Events and Announcements. Demonstrates the Part 2 data
    /// structures: Dictionary (hash table), SortedDictionary, HashSet, Queue, Stack, and a
    /// custom priority queue.
    /// </summary>
    public static class EventCatalogue
    {
        // Hash table for O(1) lookup by event id.
        private static readonly Dictionary<string, LocalEvent> EventsById = new Dictionary<string, LocalEvent>();

        // Sorted dictionary keeps events ordered by date for chronological browsing.
        private static readonly SortedDictionary<DateTime, List<LocalEvent>> EventsByDate = new SortedDictionary<DateTime, List<LocalEvent>>();

        // Sets store unique categories and unique dates used to populate the filter controls.
        private static readonly HashSet<string> Categories = new HashSet<string>();
        private static readonly HashSet<DateTime> UniqueDates = new HashSet<DateTime>();

        // Queue of latest municipal announcements. The user can rotate through them.
        private static readonly Queue<LocalEvent> AnnouncementQueue = new Queue<LocalEvent>();

        // Stack records recently viewed event ids. Top of the stack is the most recent.
        private static readonly Stack<string> RecentlyViewed = new Stack<string>();

        // Priority queue for featured events (higher priority, earlier date first).
        private static readonly SimplePriorityQueue FeaturedQueue = new SimplePriorityQueue();

        // Search history queue for recommendation scoring.
        private static readonly Queue<SearchRecord> SearchHistory = new Queue<SearchRecord>();
        private static readonly Dictionary<string, int> CategorySearchCounts = new Dictionary<string, int>();

        private static int nextId = 1;
        private const int MaxSearchHistory = 20;

        public static bool IsLoaded { get; private set; }

        /// <summary>Seed data relative to today's date so the catalogue always looks current.</summary>
        public static void Load()
        {
            if (IsLoaded)
            {
                return;
            }

            SeedEvents();
            RebuildIndexes();
            IsLoaded = true;
        }

        public static void RecordSearch(string category, DateTime? date)
        {
            SearchHistory.Enqueue(new SearchRecord { Category = category, Date = date });

            if (SearchHistory.Count > MaxSearchHistory)
            {
                SearchHistory.Dequeue();
            }

            string key = string.IsNullOrEmpty(category) ? "All" : category;
            if (!CategorySearchCounts.ContainsKey(key))
            {
                CategorySearchCounts[key] = 0;
            }

            CategorySearchCounts[key]++;
        }

        public static IEnumerable<LocalEvent> GetAnnouncements()
        {
            return AnnouncementQueue;
        }

        public static void RotateAnnouncement()
        {
            if (AnnouncementQueue.Count == 0)
            {
                return;
            }

            LocalEvent front = AnnouncementQueue.Dequeue();
            AnnouncementQueue.Enqueue(front);
        }

        public static void PushRecentlyViewed(string eventId)
        {
            if (string.IsNullOrEmpty(eventId))
            {
                return;
            }

            // Keep the most recent at the top, avoid adjacent duplicates.
            if (RecentlyViewed.Count == 0 || RecentlyViewed.Peek() != eventId)
            {
                RecentlyViewed.Push(eventId);
            }
        }

        public static LocalEvent PopRecentlyViewed()
        {
            while (RecentlyViewed.Count > 0)
            {
                string id = RecentlyViewed.Pop();
                if (EventsById.ContainsKey(id))
                {
                    return EventsById[id];
                }
            }

            return null;
        }

        public static IEnumerable<LocalEvent> GetRecentlyViewed()
        {
            List<LocalEvent> result = new List<LocalEvent>();
            foreach (string id in RecentlyViewed)
            {
                if (EventsById.ContainsKey(id) && !result.Contains(EventsById[id]))
                {
                    result.Add(EventsById[id]);
                }
            }

            return result;
        }

        public static LocalEvent GetById(string id)
        {
            if (EventsById.ContainsKey(id))
            {
                return EventsById[id];
            }

            return null;
        }

        public static IEnumerable<string> GetCategories()
        {
            List<string> sorted = new List<string>(Categories);
            sorted.Sort();
            return sorted;
        }

        public static IEnumerable<DateTime> GetUniqueDates()
        {
            return UniqueDates;
        }

        /// <summary>
        /// Returns upcoming events in date order. Announcements are included at the end unless
        /// the caller filters them out.
        /// </summary>
        public static IEnumerable<LocalEvent> GetUpcomingEvents()
        {
            foreach (DateTime date in EventsByDate.Keys)
            {
                foreach (LocalEvent item in EventsByDate[date])
                {
                    yield return item;
                }
            }
        }

        /// <summary>
        /// Search by category and/or exact date. Empty category means any category; null date
        /// means any date.
        /// </summary>
        public static IEnumerable<LocalEvent> Search(string category, DateTime? date)
        {
            foreach (LocalEvent item in GetUpcomingEvents())
            {
                bool matchesCategory = string.IsNullOrEmpty(category) || item.Category == category;
                bool matchesDate = !date.HasValue || item.EventDate.Date == date.Value.Date;

                if (matchesCategory && matchesDate)
                {
                    yield return item;
                }
            }
        }

        /// <summary>
        /// Returns the top featured events using the priority queue. Does not alter the queue.
        /// </summary>
        public static IEnumerable<LocalEvent> GetFeatured(int count)
        {
            int returned = 0;
            foreach (LocalEvent item in FeaturedQueue.Snapshot())
            {
                if (returned >= count)
                {
                    break;
                }

                yield return item;
                returned++;
            }
        }

        /// <summary>
        /// Recommendation engine. Combines search history (queue + dictionary) with the priority
        /// queue to suggest related upcoming events.
        /// </summary>
        public static IEnumerable<RecommendedEvent> GetRecommendations(int count, IEnumerable<string> excludeIds)
        {
            HashSet<string> exclude = new HashSet<string>(excludeIds);

            // Determine the most searched category.
            string favouriteCategory = null;
            int bestCount = 0;
            foreach (KeyValuePair<string, int> pair in CategorySearchCounts)
            {
                if (pair.Value > bestCount)
                {
                    bestCount = pair.Value;
                    favouriteCategory = pair.Key;
                }
            }

            // Last search record is used for date proximity.
            SearchRecord last = null;
            foreach (SearchRecord record in SearchHistory)
            {
                last = record;
            }

            List<RecommendedEvent> scored = new List<RecommendedEvent>();
            foreach (LocalEvent item in GetUpcomingEvents())
            {
                if (exclude.Contains(item.Id))
                {
                    continue;
                }

                int score = 0;
                List<string> reasons = new List<string>();

                if (item.Priority >= 3)
                {
                    score += 2;
                    reasons.Add("Featured by the municipality");
                }

                if (!string.IsNullOrEmpty(favouriteCategory) && item.Category == favouriteCategory)
                {
                    score += 4;
                    reasons.Add("Matches your most searched category: " + item.Category);
                }
                else if (last != null && !string.IsNullOrEmpty(last.Category) && item.Category == last.Category)
                {
                    score += 2;
                    reasons.Add("In category you just searched: " + item.Category);
                }

                if (last != null && last.Date.HasValue)
                {
                    double days = Math.Abs((item.EventDate.Date - last.Date.Value.Date).TotalDays);
                    if (days <= 7)
                    {
                        score += 2;
                        reasons.Add("Close to the date you searched");
                    }
                }

                if (score > 0)
                {
                    scored.Add(new RecommendedEvent
                    {
                        Event = item,
                        Score = score,
                        Reason = string.Join("; ", reasons)
                    });
                }
            }

            scored.Sort((a, b) =>
            {
                int cmp = b.Score.CompareTo(a.Score);
                if (cmp != 0)
                {
                    return cmp;
                }

                return a.Event.EventDate.CompareTo(b.Event.EventDate);
            });

            for (int i = 0; i < scored.Count && i < count; i++)
            {
                yield return scored[i];
            }
        }

        private static void SeedEvents()
        {
            DateTime today = DateTime.Today;

            AddEvent("Community hall repairs", "Community", "The Bellville community hall will be closed for maintenance.", "Bellville Community Hall", today.AddDays(2).AddHours(10), null, 2, true);
            AddEvent("Road resurfacing - Voortrekker Road", "Roads", "Single-lane closures between 08:00 and 16:00.", "Voortrekker Road", today.AddDays(3).AddHours(8), today.AddDays(3).AddHours(16), 4, false);
            AddEvent("Water supply update", "Utilities", "Planned pressure reduction in the southern areas.", "Municipal notice", today.AddDays(1), null, 3, true);
            AddEvent("Youth sports day", "Recreation", "Athletics, soccer and netball registration open to all residents.", "Bellville Sports Grounds", today.AddDays(5).AddHours(9), today.AddDays(5).AddHours(15), 3, false);
            AddEvent("Refuse collection delay", "Sanitation", "Monday collection moved to Tuesday due to the public holiday.", "All wards", today.AddDays(4), null, 2, true);
            AddEvent("Public participation meeting", "Governance", "Review of the draft integrated development plan.", "Bellville Civic Centre", today.AddDays(7).AddHours(18), today.AddDays(7).AddHours(20), 5, false);
            AddEvent("Electricity outage notice", "Utilities", "Scheduled maintenance in the Durbanville area.", "Durbanville", today.AddDays(2), null, 4, true);
            AddEvent("Park clean-up", "Environment", "Volunteers welcome. Gloves and bags provided.", "Willowbridge Park", today.AddDays(6).AddHours(8), today.AddDays(6).AddHours(12), 2, false);
            AddEvent("Library story hour", "Recreation", "Free reading session for children aged 4 to 8.", "Bellville Public Library", today.AddDays(4).AddHours(10), today.AddDays(4).AddHours(11), 2, false);
            AddEvent("Street light inspection", "Utilities", "Teams will inspect street lights in the central business district.", "CBD", today.AddDays(8), null, 2, true);
            AddEvent("Indigenous plant sale", "Environment", "Local nursery plants at subsidised prices.", "Durbanville Nature Reserve", today.AddDays(9).AddHours(9), today.AddDays(9).AddHours(13), 3, false);
            AddEvent("Ratepayers information session", "Governance", "How to query your municipal account and apply for rebates.", "Bellville Civic Centre", today.AddDays(10).AddHours(17), today.AddDays(10).AddHours(19), 3, false);
            AddEvent("Sanitation health awareness", "Sanitation", "Information on refuse separation and illegal dumping.", "Bellville Taxi Rank", today.AddDays(11).AddHours(10), today.AddDays(11).AddHours(14), 2, false);
            AddEvent("City band concert", "Recreation", "Free outdoor concert in the city hall gardens.", "City Hall Gardens", today.AddDays(12).AddHours(18), today.AddDays(12).AddHours(20), 4, false);
        }

        private static void AddEvent(string title, string category, string summary, string venue,
            DateTime eventDate, DateTime? endTime, int priority, bool isAnnouncement)
        {
            string id = "EVT-" + nextId.ToString("D3");
            nextId++;

            LocalEvent item = new LocalEvent
            {
                Id = id,
                Title = title,
                Category = category,
                Summary = summary,
                Venue = venue,
                EventDate = eventDate,
                EndTime = endTime,
                Priority = priority,
                IsAnnouncement = isAnnouncement
            };

            EventsById[id] = item;
        }

        private static void RebuildIndexes()
        {
            EventsByDate.Clear();
            Categories.Clear();
            UniqueDates.Clear();
            AnnouncementQueue.Clear();
            FeaturedQueue.Clear();

            foreach (LocalEvent item in EventsById.Values)
            {
                DateTime dateKey = item.EventDate.Date;
                if (!EventsByDate.ContainsKey(dateKey))
                {
                    EventsByDate[dateKey] = new List<LocalEvent>();
                }

                EventsByDate[dateKey].Add(item);

                Categories.Add(item.Category);
                UniqueDates.Add(dateKey);

                if (item.IsAnnouncement)
                {
                    AnnouncementQueue.Enqueue(item);
                }
                else
                {
                    FeaturedQueue.Enqueue(item);
                }
            }

            // Keep each date list in time order for stable output.
            foreach (List<LocalEvent> list in EventsByDate.Values)
            {
                list.Sort((a, b) => a.EventDate.CompareTo(b.EventDate));
            }
        }

        private class SearchRecord
        {
            public string Category { get; set; }
            public DateTime? Date { get; set; }
        }

        public class RecommendedEvent
        {
            public LocalEvent Event { get; set; }
            public int Score { get; set; }
            public string Reason { get; set; }
        }
    }
}
