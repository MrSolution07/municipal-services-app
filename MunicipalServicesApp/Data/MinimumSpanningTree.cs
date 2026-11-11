using System;
using System.Collections.Generic;

namespace MunicipalServicesApp.Data
{
    /// <summary>One road in the crew route, with its length in kilometres.</summary>
    public class RouteEdge
    {
        public string From { get; set; }

        public string To { get; set; }

        public int Kilometres { get; set; }
    }

    /// <summary>
    /// Prim's algorithm. The depot is the start vertex. The result has vertices - 1 edges
    /// when the distance matrix contains every pair.
    /// </summary>
    public static class MinimumSpanningTree
    {
        public static List<RouteEdge> Prim(IList<string> vertices, out int totalKilometres)
        {
            totalKilometres = 0;
            List<RouteEdge> edges = new List<RouteEdge>();
            if (vertices == null || vertices.Count <= 1)
            {
                return edges;
            }

            HashSet<string> included = new HashSet<string>(StringComparer.Ordinal);
            if (!Contains(vertices, AreaGraph.Depot))
            {
                throw new InvalidOperationException("The crew route must include the depot.");
            }

            included.Add(AreaGraph.Depot);
            while (included.Count < vertices.Count)
            {
                string bestFrom = null;
                string bestTo = null;
                int best = int.MaxValue;

                foreach (string from in included)
                {
                    for (int i = 0; i < vertices.Count; i++)
                    {
                        string to = vertices[i];
                        if (included.Contains(to))
                        {
                            continue;
                        }

                        int kilometres = AreaGraph.Distance(from, to);
                        if (kilometres < best)
                        {
                            best = kilometres;
                            bestFrom = from;
                            bestTo = to;
                        }
                    }
                }

                if (bestTo == null)
                {
                    throw new InvalidOperationException("Prim could not reach every area. The distance matrix is incomplete.");
                }

                included.Add(bestTo);
                edges.Add(new RouteEdge { From = bestFrom, To = bestTo, Kilometres = best });
                totalKilometres += best;
            }

            if (edges.Count != vertices.Count - 1)
            {
                throw new InvalidOperationException("Prim returned " + edges.Count + " edges for " + vertices.Count + " places.");
            }

            return edges;
        }

        private static bool Contains(IList<string> vertices, string name)
        {
            for (int i = 0; i < vertices.Count; i++)
            {
                if (vertices[i] == name)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
