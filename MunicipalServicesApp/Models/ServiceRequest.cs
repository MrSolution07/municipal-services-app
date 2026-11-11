using System;

namespace MunicipalServicesApp.Models
{
    /// <summary>How far a municipal service request has moved.</summary>
    public enum RequestStatus
    {
        Submitted = 0,
        InReview = 1,
        Assigned = 2,
        InProgress = 3,
        Resolved = 4
    }

    /// <summary>Whether the request was loaded with the app or created from a resident report.</summary>
    public enum RequestSource
    {
        Seed = 0,
        Resident = 1
    }

    /// <summary>
    /// One service request tracked on the Part 3 status page.
    /// </summary>
    public class ServiceRequest
    {
        public string Reference { get; set; }

        public string Department { get; set; }

        public string Category { get; set; }

        public string Location { get; set; }

        public string Area { get; set; }

        public string Description { get; set; }

        public RequestStatus Status { get; set; }

        /// <summary>1 to 5. 5 is the most urgent.</summary>
        public int Priority { get; set; }

        public DateTime SubmittedAt { get; set; }

        public RequestSource Source { get; set; }

        public bool IsOpen
        {
            get { return Status != RequestStatus.Resolved; }
        }

        public bool IsWaitingToAssign
        {
            get { return Status == RequestStatus.Submitted || Status == RequestStatus.InReview; }
        }

        public static string StatusText(RequestStatus status)
        {
            switch (status)
            {
                case RequestStatus.InReview:
                    return "In review";
                case RequestStatus.InProgress:
                    return "In progress";
                default:
                    return status.ToString();
            }
        }

        public string StatusName
        {
            get { return StatusText(Status); }
        }
    }
}
