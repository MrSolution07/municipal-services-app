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
