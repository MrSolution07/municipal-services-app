using System;

namespace MunicipalServicesApp.Models
{
    /// <summary>
    /// One issue reported by a resident. Instances are stored in the application's List of reports.
    /// </summary>
    public class IssueReport
    {
        public string ReferenceNumber { get; set; }

        public string Location { get; set; }

        public string Category { get; set; }

        public string Description { get; set; }

        /// <summary>Full path of the attached image or document. Empty when nothing was attached.</summary>
        public string AttachmentPath { get; set; }

        public DateTime SubmittedAt { get; set; }

        public bool HasAttachment
        {
            get { return !string.IsNullOrEmpty(AttachmentPath); }
        }

        public override string ToString()
        {
            return ReferenceNumber + " - " + Category + " - " + Location;
        }
    }
}
