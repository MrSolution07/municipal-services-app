using System;
using System.Collections.Generic;
using MunicipalServicesApp.Models;

namespace MunicipalServicesApp.Data
{
    /// <summary>
    /// In-memory storage for reported issues. A List is used throughout the application:
    /// reports are appended in submission order and read back in that same order.
    /// </summary>
    public static class IssueStore
    {
        private static readonly List<IssueReport> Reports = new List<IssueReport>();

        public static int Count
        {
            get { return Reports.Count; }
        }

        /// <summary>Adds a report to the list and returns its generated reference number.</summary>
        public static string Add(IssueReport report)
        {
            if (report == null)
            {
                throw new ArgumentNullException("report");
            }

            report.ReferenceNumber = BuildReferenceNumber(Reports.Count + 1);
            report.SubmittedAt = DateTime.Now;
            Reports.Add(report);
            return report.ReferenceNumber;
        }

        /// <summary>Read-only view of every report captured during this session.</summary>
        public static IReadOnlyList<IssueReport> GetAll()
        {
            return Reports;
        }

        private static string BuildReferenceNumber(int sequence)
        {
            return "MSA-" + DateTime.Now.ToString("yyyyMMdd") + "-" + sequence.ToString("D3");
        }
    }
}
