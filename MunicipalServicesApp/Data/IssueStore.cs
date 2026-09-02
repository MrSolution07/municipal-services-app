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
