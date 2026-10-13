using System;

namespace MunicipalServicesApp.Models
{
    /// <summary>
    /// One municipal event or announcement. Used by the Local Events and Announcements
    /// feature in Part 2.
    /// </summary>
    public class LocalEvent
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Category { get; set; }

        public string Summary { get; set; }

        public string Venue { get; set; }

        public DateTime EventDate { get; set; }

        public DateTime? EndTime { get; set; }

        /// <summary>
        /// Higher numbers are more important. Used by the priority queue to select featured items.
        /// </summary>
        public int Priority { get; set; }

        public bool IsAnnouncement { get; set; }

        public string DisplayDate
        {
            get
            {
                string start = EventDate.ToString("ddd d MMMM yyyy");
                if (EndTime.HasValue)
                {
                    return start + " from " + EventDate.ToString("HH:mm") + " to " + EndTime.Value.ToString("HH:mm");
                }

                if (IsAnnouncement)
                {
                    return "Announcement - " + start;
                }

                return start + " at " + EventDate.ToString("HH:mm");
            }
        }

        public override string ToString()
        {
            return Title + " (" + Category + ")";
        }
    }
}
