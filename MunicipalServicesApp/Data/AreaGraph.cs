using System;
using System.Collections.Generic;

namespace MunicipalServicesApp.Data
{
    /// <summary>
    /// Complete undirected distance matrix, in kilometres, between the depot and the suburbs.
    /// Every pair has a weight, so any subset that contains the depot stays connected.
    /// </summary>
    public static class AreaGraph
    {
        public const string Depot = "Civic Centre";

        private static readonly Dictionary<string, int> Kilometres = new Dictionary<string, int>(StringComparer.Ordinal);

        static AreaGraph()
        {
            Add(Depot, "Bellville", 4);
            Add(Depot, "Durbanville", 12);
            Add(Depot, "CBD", 8);
            Add(Depot, "Parow", 6);
            Add(Depot, "Brackenfell", 15);
            Add("Bellville", "Durbanville", 10);
            Add("Bellville", "CBD", 6);
            Add("Bellville", "Parow", 5);
            Add("Bellville", "Brackenfell", 14);
            Add("Durbanville", "CBD", 14);
            Add("Durbanville", "Parow", 11);
            Add("Durbanville", "Brackenfell", 8);
            Add("CBD", "Parow", 9);
            Add("CBD", "Brackenfell", 18);
            Add("Parow", "Brackenfell", 16);
        }

        public static int Distance(string left, string right)
        {
            if (left == right)
            {
                return 0;
            }

            int kilometres;
            if (!Kilometres.TryGetValue(Key(left, right), out kilometres))
            {
                throw new InvalidOperationException("No distance is stored between " + left + " and " + right + ".");
            }

            return kilometres;
        }

        private static void Add(string left, string right, int kilometres)
        {
            Kilometres[Key(left, right)] = kilometres;
        }

        private static string Key(string left, string right)
        {
            if (string.Compare(left, right, StringComparison.Ordinal) > 0)
            {
                string swap = left;
                left = right;
                right = swap;
            }

            return left + "|" + right;
        }
    }
}
